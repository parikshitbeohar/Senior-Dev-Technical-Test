# Run from the repo root:  .\scripts\start.ps1
$root = Split-Path -Parent $PSScriptRoot

Start-Process powershell -ArgumentList @(
  '-NoExit',
  '-Command',
  "Set-Location '$root\api'; dotnet run --project src/Ixp.Interview.Api"
)

Start-Process powershell -ArgumentList @(
  '-NoExit',
  '-Command',
  "Set-Location '$root\web'; if (-not (Test-Path node_modules)) { npm install }; npm run dev"
)

Write-Host 'Started API (http://localhost:5080/swagger) and web (http://localhost:3000).'
