@echo off
set /p OctopusNugetApiKey=Set Octopus Nuget api key
set /p OctopusNugetServerUrl=Set Octopus Nuget server url
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" .\buildDefinition.msbuild /nologo /m /v:m /t:D /p:OctopusNugetApiKey=%OctopusNugetApiKey% /p:OctopusNugetServerUrl=%OctopusNugetServerUrl%