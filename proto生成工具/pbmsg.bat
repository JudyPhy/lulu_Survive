@echo 生成客户端消息文件

xcopy /R /Y luluBattle.proto protobuf-net\ProtoGen\
cd protobuf-net\ProtoGen
protogen.exe -i:luluBattle.proto -o:pbmsg.cs
xcopy /R /Y pbmsg.cs ..\..\client\

@echo 删除中间文件
del luluBattle.proto
del pbmsg.cs
cd ../../

@echo 生成服务端消息文件
xcopy /R /Y luluBattle.proto server\
cd server
protoc.exe  --plugin=protoc-gen-go=protoc-gen-go.exe --go_out . --proto_path .  luluBattle.proto

@echo 删除中间文件
del luluBattle.proto
cd ../

pause