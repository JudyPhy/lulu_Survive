xcopy /R /Y ..\client\ququ\node_modules\.bin\pb.js client\
xcopy /R /Y ..\client\ququ\node_modules\.bin\pb.d.ts client\

cd ..\client\ququ\node_modules\.bin\
del pb.js 
del pb.d.ts
del ququPb.proto
pause