dotnet publish src/Server/Guardians.Service.Authentication/Guardians.Service.Authentication.csproj -c DEBUG_LOCAL
dotnet publish src/Server/Guardians.Service.ServiceDiscovery/Guardians.Service.ServiceDiscovery.csproj -c DEBUG_LOCAL

if not exist "build" mkdir build
if not exist "build\auth" mkdir build\auth
if not exist "build\servdisc" mkdir build\servdisc
xcopy src\Server\Guardians.Service.Authentication\bin\Debug_Local\netcoreapp2.0\publish build\auth /s /y
xcopy src\Server\Guardians.Service.ServiceDiscovery\bin\Debug_Local\netcoreapp2.0\publish build\servdisc /s /y

PAUSE