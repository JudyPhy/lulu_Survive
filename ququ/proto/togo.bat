@echo 生成服务端消息文件
xcopy /R /Y ququPb.proto server\
cd server
protoc.exe  --plugin=protoc-gen-go=protoc-gen-go.exe --go_out . --proto_path .  ququPb.proto

@echo 删除中间文件
del ququPb.proto
cd ../