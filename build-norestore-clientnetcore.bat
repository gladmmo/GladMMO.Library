dotnet publish src/Client/GladMMO.Client.Game.NetCore/GladMMO.Client.Game.NetCore.csproj -c Debug

if not exist "build\clientnetcore" mkdir build\clientnetcore

xcopy src\Client\GladMMO.Client.Game.NetCore\bin\Debug\netstandard2.0\publish build\clientnetcore /Y /q /EXCLUDE:BuildExclude.txt
PAUSE