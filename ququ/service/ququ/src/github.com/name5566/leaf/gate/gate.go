package gate

import (
	"encoding/binary"
	"net"
	"reflect"
	"time"

	"github.com/name5566/leaf/chanrpc"
	"github.com/name5566/leaf/log"
	"github.com/name5566/leaf/network"
)

type Gate struct {
	MaxConnNum      int
	PendingWriteNum int
	MaxMsgLen       uint32
	Processor       network.Processor
	AgentChanRPC    *chanrpc.Server

	// websocket
	WSAddr      string
	HTTPTimeout time.Duration
	CertFile    string
	KeyFile     string

	// tcp
	TCPAddr      string
	LenMsgLen    int
	LittleEndian bool
}

func (gate *Gate) Run(closeSig chan bool) {
	var wsServer *network.WSServer
	if gate.WSAddr != "" {
		wsServer = new(network.WSServer)
		wsServer.Addr = gate.WSAddr
		wsServer.MaxConnNum = gate.MaxConnNum
		wsServer.PendingWriteNum = gate.PendingWriteNum
		wsServer.MaxMsgLen = gate.MaxMsgLen
		wsServer.HTTPTimeout = gate.HTTPTimeout
		wsServer.CertFile = gate.CertFile
		wsServer.KeyFile = gate.KeyFile
		wsServer.NewAgent = func(conn *network.WSConn) network.Agent {
			a := &agent{conn: conn, gate: gate}
			if gate.AgentChanRPC != nil {
				gate.AgentChanRPC.Go("NewAgent", a)
			}
			return a
		}
	}

	var tcpServer *network.TCPServer
	if gate.TCPAddr != "" {
		tcpServer = new(network.TCPServer)
		tcpServer.Addr = gate.TCPAddr
		tcpServer.MaxConnNum = gate.MaxConnNum
		tcpServer.PendingWriteNum = gate.PendingWriteNum
		tcpServer.LenMsgLen = gate.LenMsgLen
		tcpServer.MaxMsgLen = gate.MaxMsgLen
		tcpServer.LittleEndian = gate.LittleEndian
		tcpServer.NewAgent = func(conn *network.TCPConn) network.Agent {
			a := &agent{conn: conn, gate: gate}
			if gate.AgentChanRPC != nil {
				gate.AgentChanRPC.Go("NewAgent", a)
			}
			return a
		}
	}

	if wsServer != nil {
		wsServer.Start()
	}
	if tcpServer != nil {
		tcpServer.Start()
	}
	<-closeSig
	if wsServer != nil {
		wsServer.Close()
	}
	if tcpServer != nil {
		tcpServer.Close()
	}
}

func (gate *Gate) OnDestroy() {}

type agent struct {
	conn     network.Conn
	gate     *Gate
	userData interface{}
}

func (a *agent) Run() {
	for {
		data, err := a.conn.ReadMsg()
		if err != nil {
			log.Debug("read message: %v", err)
			break
		}
		a.splitMsg(data)
		// if a.gate.Processor != nil {
		// 	msg, err := a.gate.Processor.Unmarshal(data)
		// 	if err != nil {
		// 		log.Debug("unmarshal message error: %v", err)
		// 		break
		// 	}
		// 	err = a.gate.Processor.Route(msg, a)
		// 	if err != nil {
		// 		log.Debug("route message error: %v", err)
		// 		break
		// 	}
		// }
	}
}

func (a *agent) OnClose() {
	if a.gate.AgentChanRPC != nil {
		err := a.gate.AgentChanRPC.Call0("CloseAgent", a)
		if err != nil {
			log.Error("chanrpc error: %v", err)
		}
	}
}

func (a *agent) WriteMsg(msg interface{}) {
	if a.gate.Processor != nil {
		data, err := a.gate.Processor.Marshal(msg)
		if err != nil {
			log.Error("marshal message %v error: %v", reflect.TypeOf(msg), err)
			return
		}
		err = a.conn.WriteMsg(data...)
		if err != nil {
			log.Error("write message %v error: %v", reflect.TypeOf(msg), err)
		}
	}
}

func (a *agent) LocalAddr() net.Addr {
	return a.conn.LocalAddr()
}

func (a *agent) RemoteAddr() net.Addr {
	return a.conn.RemoteAddr()
}

func (a *agent) Close() {
	a.conn.Close()
}

func (a *agent) Destroy() {
	a.conn.Destroy()
}

func (a *agent) UserData() interface{} {
	return a.userData
}

func (a *agent) SetUserData(data interface{}) {
	a.userData = data
}

var mIncompleteBuffer []byte = nil
var headPackageSize int = 4

func (a *agent) parseLength(data []byte) int {
	length := binary.BigEndian.Uint16(data)
	return int(length)
}

func (a *agent) BuildMessage(data []byte) {
	if a.gate.Processor != nil {
		msg, err := a.gate.Processor.Unmarshal(data)
		if err != nil {
			log.Debug("unmarshal message error: %v", err)
		} else {
			err = a.gate.Processor.Route(msg, a)
			if err != nil {
				log.Debug("route message error: %v", err)
			}
		}
	}
}

func (a *agent) splitMsg(data []byte) {
	var msgBuffer []byte = nil
	if mIncompleteBuffer == nil {
		msgBuffer = data
	} else {
		completeBuffer := make([]byte, 0)
		completeBuffer = append(completeBuffer, mIncompleteBuffer...)
		completeBuffer = append(completeBuffer, data...)
		mIncompleteBuffer = nil
		msgBuffer = completeBuffer
	}

	curPos := 0 // split package pos
	for {
		if curPos == len(msgBuffer) {
			break
		}
		//包头长度不符，等待下一个包
		if len(msgBuffer)-curPos <= headPackageSize {
			incompleteBuffer := make([]byte, 0) //临时缓冲区
			incompleteBuffer = append(incompleteBuffer, msgBuffer[curPos:]...)
			mIncompleteBuffer = incompleteBuffer //存储没有处理的消息
			break
		}
		//消息长度不符，等待下一个包
		lenArray := msgBuffer[curPos+2 : curPos+4] //包头后2个字节是总长度（id+proto消息）
		sizePackage := a.parseLength(lenArray) + 2
		if sizePackage > len(msgBuffer)-curPos {
			incompleteBuffer := make([]byte, 0) //临时缓冲区
			incompleteBuffer = append(incompleteBuffer, msgBuffer[curPos:]...)
			mIncompleteBuffer = incompleteBuffer //存储没有处理的消息
			break
		}
		//消息完整，取出解析
		a.BuildMessage(msgBuffer[curPos : curPos+sizePackage])
		curPos += sizePackage
	}
}
