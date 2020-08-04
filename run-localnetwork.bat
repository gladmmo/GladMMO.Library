cd build\auth
start "auth" dotnet GladMMO.Service.Authentication.dll --usehttps Certs/TLSCert.pfx
cd ..

cd servdisc
start "servdisc" dotnet GladMMO.Service.ServiceDiscovery.dll --url=http://192.168.0.12:5000
cd ..

cd servsel
start "servsel" dotnet GladMMO.Service.ServerSelection.dll --url=http://192.168.0.12:5002
cd ..

cd gameserv
start "gameserv" dotnet GladMMO.Service.GameServer.dll --url=http://192.168.0.12:5004
cd ..

cd contentserv
start "contentserv" dotnet GladMMO.Service.ContentServer.dll --url=http://192.168.0.12:5005
cd ..

cd vivox
start "vivox" dotnet GladMMO.Service.Vivox.dll --url=http://192.168.0.12:5010
cd ..

cd namequery
start "namequery" dotnet GladMMO.Service.NameQuery.dll --url=http://192.168.0.12:5011
cd ..

cd social
start "social" dotnet GladMMO.Service.Social.dll --url=http://192.168.0.12:5008
cd ..

cd zoneauth
start "zoneauth" dotnet GladMMO.Service.ZoneAuthentication.dll --url=http://192.168.0.12:5009
cd ..

cd zonemanager
start "zonemanager" dotnet GladMMO.Service.ZoneManager.dll --url=http://192.168.0.12:5012
cd ..

cd discord
start "discordserv" dotnet GladMMO.Service.Discord.dll --url=http://192.168.0.12:5013 https://192.168.0.12:15013 --usehttps=Certs/ScapeVRMMOOrg.pfx
cd ..