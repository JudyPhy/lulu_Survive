package main

import (
	"server/conf"
	"server/frameSync"
	"server/game"
	"server/gate"
	"server/login"

	"github.com/name5566/leaf"
	lconf "github.com/name5566/leaf/conf"
)

func main() {
	lconf.LogLevel = conf.Server.LogLevel
	lconf.LogPath = conf.Server.LogPath
	lconf.LogFlag = conf.LogFlag
	lconf.ConsolePort = conf.Server.ConsolePort
	lconf.ProfilePath = conf.Server.ProfilePath

	frameSync.Init()

	leaf.Run(
		game.Module,
		gate.Module,
		login.Module,
	)
}
