dotnet publish src/sdk/GladMMO.Client.SDK/GladMMO.Client.SDK.csproj -c Debug

if not exist "build\sdk" mkdir build\sdk

xcopy src\sdk\GladMMO.Client.SDK\bin\Debug\netstandard2.0\publish build\sdk /Y /q /EXCLUDE:BuildExclude.txt
EXIT 0