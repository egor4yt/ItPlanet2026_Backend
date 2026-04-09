$scriptDir = $PSScriptRoot
$scripts = @(
    "prepare-infrastructure-directory.ps1",
    "configure-keycloak.ps1",
    "configure-database.ps1",
    "configure-elastic.ps1",
    "configure-monitoring.ps1",
    "configure-environment.ps1",
    "compose-up.ps1"
)

$profile = ""
do {
    Write-Host "Select profile:" -ForegroundColor Yellow
    Write-Host "1. all"
    Write-Host "2. load-test"
    $choice = Read-Host "Enter number: "

    switch ($choice) {
        "1" { $profile = "all" }
        "2" { $profile = "load-test" }
        Default { Write-Host "Invalid selection, try again." -ForegroundColor Red }
    }
} while ($profile -eq "")

Write-Host "Selected profile: $profile" -ForegroundColor Green

foreach ($scriptName in $scripts) {
    $scriptPath = Join-Path $scriptDir $scriptName

    if (Test-Path $scriptPath) {
        Write-Host "--- Executing: $scriptName ---" -ForegroundColor Cyan

        if ($scriptName -eq "compose-up.ps1" -or $scriptName -eq "configure-environment.ps1") {
            & $scriptPath -Profile $profile
        } else {
            & $scriptPath
        }

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