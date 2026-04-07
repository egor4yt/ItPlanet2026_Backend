param (
    [Parameter(Mandatory=$true)]
    [string]$Profile
)

$targetDirectory = Join-Path $PSScriptRoot "../../../src"
$envFilesDir = Join-Path $PSScriptRoot "../../env"

$sourceFileName = "$Profile.env"
$sourceFilePath = Join-Path $envFilesDir $sourceFileName
$destinationPath = Join-Path $targetDirectory ".env"

if (Test-Path $destinationPath) {
    Remove-Item -Path $destinationPath -Force
    Write-Host "Old configuration removed." -ForegroundColor Gray
}

if (Test-Path $targetDirectory) {
    if (Test-Path $sourceFilePath) {
        Write-Host "Copying $sourceFileName to $destinationPath..." -ForegroundColor Green
        Copy-Item -Path $sourceFilePath -Destination $destinationPath -Force
    } else {
        Write-Host "Error: Source file $sourceFilePath not found!" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "Error: $targetDirectory directory not found!" -ForegroundColor Red
    exit 1
}

exit 0