dotnet publish src/Server/GladMMO.Service.Authentication/GladMMO.Service.Authentication.csproj -c DEBUG_LOCAL
dotnet publish src/Server/GladMMO.Service.ServiceDiscovery/GladMMO.Service.ServiceDiscovery.csproj -c DEBUG_LOCAL
::dotnet publish src/Server/GladMMO.Service.ServerSelection/GladMMO.Service.ServerSelection.csproj -c DEBUG_LOCAL
dotnet publish src/Server/GladMMO.Service.GameServer/GladMMO.Service.GameServer.csproj -c DEBUG_LOCAL
::dotnet publish src/Server/GladMMO.Service.ContentServer/GladMMO.Service.ContentServer.csproj -c DEBUG_LOCAL
::dotnet publish src/Server/GladMMO.Service.Vivox/GladMMO.Service.Vivox.csproj -c DEBUG_LOCAL
dotnet publish src/Server/GladMMO.Service.NameQuery/GladMMO.Service.NameQuery.csproj -c DEBUG_LOCAL
::dotnet publish src/Server/GladMMO.Service.Social/GladMMO.Service.Social.csproj -c DEBUG_LOCAL
::dotnet publish src/Server/GladMMO.Service.ZoneAuthentication/GladMMO.Service.ZoneAuthentication.csproj -c DEBUG_LOCAL
::dotnet publish src/Server/GladMMO.Service.ZoneManager/GladMMO.Service.ZoneManager.csproj -c DEBUG_LOCAL


if not exist "build\auth" mkdir build\auth
if not exist "build\servdisc" mkdir build\servdisc
if not exist "build\servsel" mkdir build\servsel
if not exist "build\gameserv" mkdir build\gameserv
if not exist "build\contentserv" mkdir build\contentserv
if not exist "build\vivox" mkdir build\vivox
if not exist "build\namequery" mkdir build\namequery
if not exist "build\social" mkdir build\social
if not exist "build\zoneauth" mkdir build\zoneauth
if not exist "build\zonemanager" mkdir build\zonemanager

xcopy src\Server\GladMMO.Service.Authentication\bin\Debug_Local\netcoreapp3.1\publish build\auth /s /y
xcopy src\Server\GladMMO.Service.ServiceDiscovery\bin\Debug_Local\netcoreapp2.0\publish build\servdisc /s /y
::xcopy src\Server\GladMMO.Service.ServerSelection\bin\Debug_Local\netcoreapp2.0\publish build\servsel /s /y
xcopy src\Server\GladMMO.Service.GameServer\bin\Debug_Local\netcoreapp3.1\publish build\gameserv /s /y
::xcopy src\Server\GladMMO.Service.ContentServer\bin\Debug_Local\netcoreapp2.1\publish build\contentserv /s /y
::xcopy src\Server\GladMMO.Service.Vivox\bin\Debug_Local\netcoreapp2.1\publish build\vivox /s /y
xcopy src\Server\GladMMO.Service.NameQuery\bin\Debug_Local\netcoreapp3.1\publish build\namequery /s /y
::xcopy src\Server\GladMMO.Service.Social\bin\Debug_Local\netcoreapp2.1\publish build\social /s /y
::xcopy src\Server\GladMMO.Service.ZoneAuthentication\bin\Debug_Local\netcoreapp2.1\publish build\zoneauth /s /y
::xcopy src\Server\GladMMO.Service.ZoneManager\bin\Debug_Local\netcoreapp2.1\publish build\zonemanager /s /y

EXIT 0