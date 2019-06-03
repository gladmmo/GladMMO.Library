cd build\auth
start "auth" dotnet GladMMO.Service.Authentication.dll --url=http://0.0.0.0:443 --usehttps Certs/TLSCert.pfx
cd ..

cd servdisc
start "servdisc" dotnet GladMMO.Service.ServiceDiscovery.dll --url=http://0.0.0.0:5000
cd ..

cd servsel
start "servsel" dotnet GladMMO.Service.ServerSelection.dll --url=http://0.0.0.0:5002
cd ..

cd gameserv
start "gameserv" dotnet GladMMO.Service.GameServer.dll --url=http://0.0.0.0:5004
cd ..

cd gameservdisc
start "gameservdisc" dotnet GladMMO.Service.ServiceDiscovery.dll --url=http://0.0.0.0:5003
cd ..

cd contentserv
start "contentserv" dotnet GladMMO.Service.ContentServer.dll --url=http://0.0.0.0:5005
cd ..