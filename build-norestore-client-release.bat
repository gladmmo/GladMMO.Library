dotnet publish src/GladMMO.Client.All/GladMMO.Client.All.csproj -c Release
dotnet publish src/GladMMO.Client.Common/GladMMO.Client.Common.csproj -c Release

if not exist "build\client" mkdir build\client"

if not exist "build\api" mkdir build\api"
if not exist "build\api\client" mkdir build\api\client"

xcopy src\GladMMO.Client.Common\bin\Release\netstandard2.0\publish build\api\client /Y /q /EXCLUDE:BuildExclude.txt
xcopy src\GladMMO.Client.All\bin\Release\netstandard2.0\publish build\client /Y /q /EXCLUDE:BuildExclude.txt