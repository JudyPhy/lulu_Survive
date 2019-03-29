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

func GetAgent(playerId int64) gate.Agent {
	for a, id := range agentPlayerMap {
		if id == playerId {
			return a
		}
	}
	return nil
}
