$scriptDir = $PSScriptRoot
$composeFilePath = Join-Path $PSScriptRoot "../../../src/compose.yaml"
$scripts = @(
    "prepare-infrastructure-directory.ps1",
    "configure-keycloak.ps1",
    "configure-database.ps1"
)

foreach ($scriptName in $scripts) {
    $scriptPath = Join-Path $scriptDir $scriptName

    if (Test-Path $scriptPath) {
        Write-Host "--- Executing: $scriptName ---" -ForegroundColor Cyan

        & $scriptPath

        if ($LASTEXITCODE -ne 0 -and $null -ne $LASTEXITCODE) {
            Write-Host "Failed to execute $scriptName. Code: $LASTEXITCODE" -ForegroundColor Red
            break
        }
    } else {
        Write-Host "Script not found: $scriptPath" -ForegroundColor Red
        break
    }
}

Write-Host "Clean old docker compose..." -ForegroundColor Yellow
docker compose -f $composeFilePath -p launchpad down -v

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

Write-Host "All scripts executed." -ForegroundColor Green