@echo ���ɿͻ�����Ϣ�ļ�

xcopy /R /Y luluBattle.proto protobuf-net\ProtoGen\
cd protobuf-net\ProtoGen
protogen.exe -i:luluBattle.proto -o:pbmsg.cs
xcopy /R /Y pbmsg.cs ..\..\client\

@echo ɾ���м��ļ�
del luluBattle.proto
del pbmsg.cs
cd ../../

@echo ���ɷ������Ϣ�ļ�
xcopy /R /Y luluBattle.proto server\
cd server
protoc.exe  --plugin=protoc-gen-go=protoc-gen-go.exe --go_out . --proto_path .  luluBattle.proto

@echo ɾ���м��ļ�
del luluBattle.proto
cd ../

pause