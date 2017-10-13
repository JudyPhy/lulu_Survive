package room

import (
	"github.com/name5566/leaf/log"
)

type SceneMap struct {
	MapID   int32
	BornPos [][]uint32
	ExpPos  [][]uint32
	BuffPos [][]uint32
}

func (scene *SceneMap) createScene(mapId int32) {
	log.Debug("Create scene: mapId=%v", mapId)
	scene.MapID = mapId
	scene.BornPos = make([][]uint32, 0)
	for i := 0; i < ROOM_MEMBER_COUNT; i++ {
		pos := make([]uint32, 2)
		pos[0] = 0
		pos[1] = 0
		scene.BornPos = append(scene.BornPos, pos)
	}
	for i := 0; i < 100; i++ {
		pos := make([]uint32, 2)
		pos[0] = uint32(i / 10 * 20)
		pos[1] = uint32(i % 10 * 20)
		scene.ExpPos = append(scene.ExpPos, pos)
	}
	for i := 0; i < 20; i++ {
		pos := make([]uint32, 2)
		pos[0] = uint32(i / 10 * 33)
		pos[1] = uint32(i % 10 * 33)
		scene.BuffPos = append(scene.BuffPos, pos)
	}
}
