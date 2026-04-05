$scriptDir = $PSScriptRoot
$scripts = @(
    "prepare-infrastructure-directory.ps1",
    "configure-keycloak.ps1",
    "configure-database.ps1",
    "configure-monitoring.ps1",
    "compose-up.ps1"
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

Write-Host "All scripts executed." -ForegroundColor Green
exit 0