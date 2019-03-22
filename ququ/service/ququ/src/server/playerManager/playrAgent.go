package playerManager

import (
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

var agentPlayerMap map[gate.Agent]int64

func init() {
	agentPlayerMap = make(map[gate.Agent]int64)
}

func UpdatePlayerAgent(a gate.Agent, playerid int64) {
	id, ok := agentPlayerMap[a]
	if ok && id != playerid {
		log.Debug("player[%v] agent changed to[%v]", id, playerid)
	}
	agentPlayerMap[a] = playerid
}

func GetPlayerId(a gate.Agent) int64 {
	id, ok := agentPlayerMap[a]
	if ok {
		return id
	}
	return 0
}

func GetAllAgent() []gate.Agent {
	agentList := make([]gate.Agent, 0)
	for a, _ := range agentPlayerMap {
		agentList = append(agentList, a)
	}
	return agentList
}
