@echo ���ɿͻ�����Ϣ�ļ�

xcopy /R /Y lulu.proto protobuf-net\ProtoGen\
cd protobuf-net\ProtoGen
protogen.exe -i:lulu.proto -o:pbmsg.cs
xcopy /R /Y pbmsg.cs ..\..\client\

@echo ɾ���м��ļ�
del lulu.proto
del pbmsg.cs
cd ../../

@echo ���ɷ������Ϣ�ļ�
xcopy /R /Y lulu.proto server\
cd server
protoc.exe  --plugin=protoc-gen-go=protoc-gen-go.exe --go_out . --proto_path .  lulu.proto

@echo ɾ���м��ļ�
del lulu.proto
cd ../

pause