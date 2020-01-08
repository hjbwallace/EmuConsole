$ErrorActionPreference = "Stop"
Set-StrictMode -Version "Latest"

$srcPath = Join-Path -Path $PSScriptRoot -ChildPath '..\src'
$artifactsPath = Join-Path -Path $srcPath -ChildPath '..\artifacts'

# Remove any previous artifacts 
if (Test-path $artifactsPath) { Remove-Item -Recurse -Force $artifactsPath }
New-Item -Path $artifactsPath -ItemType Directory | Out-Null

dotnet clean "$($srcPath)\EmuConsole.sln"
dotnet restore "$($srcPath)\EmuConsole.sln"

dotnet build "$($srcPath)\EmuConsole\EmuConsole.csproj"
dotnet build "$($srcPath)\EmuConsole.Tests\EmuConsole.Tests.csproj"

dotnet pack "$($srcPath)\EmuConsole\EmuConsole.csproj" --output $artifactsPath