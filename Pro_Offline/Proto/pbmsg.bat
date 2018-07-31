@echo 生成客户端消息文件

xcopy /R /Y lulu.proto protobuf-net\ProtoGen\
cd protobuf-net\ProtoGen
protogen.exe -i:lulu.proto -o:pbmsg.cs
xcopy /R /Y pbmsg.cs ..\..\client\

@echo 删除中间文件
del lulu.proto
del pbmsg.cs
cd ../../

@echo 生成服务端消息文件
xcopy /R /Y lulu.proto server\
cd server
protoc.exe  --plugin=protoc-gen-go=protoc-gen-go.exe --go_out . --proto_path .  lulu.proto

@echo 删除中间文件
del lulu.proto
cd ../

pause