cd build\auth
start "auth" cmd /c dotnet Guardians.Service.Authentication.dll --url=http://0.0.0.0:5001
cd ..

cd servdisc
start "servdisc" cmd /c dotnet Guardians.Service.ServiceDiscovery.dll --url=http://0.0.0.0:5000
cd ..