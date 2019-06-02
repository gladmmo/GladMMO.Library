dotnet publish src/server/GladMMO.Server.ZoneServer/GladMMO.Server.ZoneServer.csproj -c Debug

if not exist "build\zoneserv" mkdir build\zoneserv

xcopy src\server\GladMMO.Server.ZoneServer\bin\Debug\netstandard2.0\publish build\zoneserv /Y /q /EXCLUDE:BuildExclude.txt