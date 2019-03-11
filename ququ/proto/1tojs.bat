xcopy ququPb.proto ..\client\ququ\node_modules\.bin\
cd ..\client\ququ\node_modules\.bin\
pbjs -t static-module -w commonjs -o pb.js ququPb.proto
pause