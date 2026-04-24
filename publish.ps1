[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$projectPath = Join-Path $PSScriptRoot 'Quanta.Core.Windows\Quanta.Core.Windows.csproj'
$publishPath = 'C:\publish\quanta'

if (-not (Test-Path -LiteralPath $projectPath)) {
    throw "Project file not found: $projectPath"
}

if (Test-Path -LiteralPath $publishPath) {
    Remove-Item -LiteralPath $publishPath -Recurse -Force
}

New-Item -ItemType Directory -Path $publishPath -Force | Out-Null

dotnet publish $projectPath `
    --configuration Release `
    --runtime win-x64 `
    --self-contained false `
    --output $publishPath `
    -p:DebugType=None `
    -p:DebugSymbols=false `
    -p:PublishSingleFile=true

Write-Host "Published Quanta to $publishPath"
