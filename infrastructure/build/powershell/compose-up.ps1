$composeFilePath = Join-Path $PSScriptRoot "../../../src/compose.yaml"

Write-Host "Clean old docker compose..." -ForegroundColor Yellow
docker compose -p launchpad down -v

if (Test-Path $composeFilePath) {
    Write-Host "Starting Docker Compose..." -ForegroundColor Cyan

    docker compose -f $composeFilePath -p launchpad up -d --build --force-recreate

    if ($LASTEXITCODE -eq 0) {
        Write-Host "Success!" -ForegroundColor Green
    } else {
        Write-Host "Failed to start docker compose." -ForegroundColor Red
    }
} else {
    Write-Host "Error: $composeFilePath file not found!" -ForegroundColor Red
}
exit 0