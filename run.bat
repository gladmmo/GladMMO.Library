cd build\auth
start "auth" cmd /c dotnet GladMMO.Service.Authentication.dll --url=http://0.0.0.0:5001
cd ..

cd servdisc
start "servdisc" cmd /c dotnet GladMMO.Service.ServiceDiscovery.dll --url=http://0.0.0.0:5000
cd ..

cd servsel
start "servsel" cmd /c dotnet GladMMO.Service.ServerSelection.dll --url=http://0.0.0.0:5002
cd ..

cd gameserv
start "gameserv" cmd /c dotnet GladMMO.Service.GameServer.dll --url=http://0.0.0.0:5004
cd ..

cd gameservdisc
start "gameservdisc" cmd /c dotnet GladMMO.Service.ServiceDiscovery.dll --url=http://0.0.0.0:5003
cd ..

cd contentserv
start "contentserv" cmd /c dotnet GladMMO.Service.ContentServer.dll --url=http://0.0.0.0:5005
cd ..