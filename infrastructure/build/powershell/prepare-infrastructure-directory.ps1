$infrastructureDir = Join-Path $PSScriptRoot "../../../src/.infrastructure"

if (Test-Path $infrastructureDir) {
    Write-Host "Clean old configuration" -ForegroundColor Yellow
    Remove-Item -Path $infrastructureDir -Recurse -Force
}

Write-Host "Creating new configuration" -ForegroundColor Green
New-Item -Path $infrastructureDir -ItemType Directory | Out-Null