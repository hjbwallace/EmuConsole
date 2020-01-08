$ErrorActionPreference = "Stop"
Set-StrictMode -Version "Latest"

$srcPath = Join-Path -Path $PSScriptRoot -ChildPath '..\src'
dotnet test "$($srcPath)\EmuConsole.Tests\EmuConsole.Tests.csproj" --no-build --no-restore -v quiet