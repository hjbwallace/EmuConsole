Param(
    [ValidateNotNullOrEmpty()]
    [string] $nugetApiKey,

    [ValidateNotNullOrEmpty()]
    [string] $version
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version "Latest"

$artifactsPath = Join-Path -Path $PSScriptRoot -ChildPath '..\artifacts'

dotnet nuget push "$($artifactsPath)\EmuConsole.$($version).nupkg" -k $nugetApiKey -s https://api.nuget.org/v3/index.json
