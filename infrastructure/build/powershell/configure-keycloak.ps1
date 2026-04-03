$fromDir = Join-Path $PSScriptRoot "../../keycloak docker"
$sourceDir = Join-Path $fromDir "keycloak"
$infrastructureDir = Join-Path $PSScriptRoot "../../../src/.infrastructure"

if (Test-Path $sourceDir) {
    Write-Host "Copying files..." -ForegroundColor Cyan
    Copy-Item -Path $sourceDir -Destination $infrastructureDir -Recurse -Force
    Write-Host "Success!" -ForegroundColor Green
} else {
    Write-Host "Error: $sourceDir not found!" -ForegroundColor Red
}
exit 0