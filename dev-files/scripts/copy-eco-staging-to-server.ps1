# Copy Eco server files from staging (eco-server-download) into Server
# so the nidaren Docker can run. Preserves your Configs and Storage.
#
# Run from: dev-files\scripts
# Usage: .\copy-eco-staging-to-server.ps1

$ErrorActionPreference = "Stop"
$ScriptDir = $PSScriptRoot
$DevFiles  = Split-Path $ScriptDir -Parent
$Staging   = Join-Path $DevFiles "eco-server-download"
$ServerRoot = Split-Path $DevFiles -Parent

if (-not (Test-Path $Staging)) {
    Write-Host "Staging folder not found: $Staging. Run download-eco-server-steamcmd.ps1 first." -ForegroundColor Red
    exit 1
}

Write-Host "Copying from staging to Server (Configs and Storage are NOT overwritten)." -ForegroundColor Cyan
Write-Host "Staging: $Staging" -ForegroundColor Gray
Write-Host "Server:  $ServerRoot" -ForegroundColor Gray

# Binary and Steam libs
@("EcoServer", "linux64", "libsteam_api.so", "libsteamwebrtc.so", "steamclient.so", "steam_appid.txt") | ForEach-Object {
    $src = Join-Path $Staging $_
    if (Test-Path $src) {
        if (Test-Path $src -PathType Container) {
            Copy-Item -Path $src -Destination $ServerRoot -Recurse -Force
        } else {
            Copy-Item -Path $src -Destination $ServerRoot -Force
        }
        Write-Host "  Copied: $_" -ForegroundColor Green
    }
}

# Mods/__core__ (so server and __core__ match)
$coreSrc = Join-Path $Staging "Mods\__core__"
$coreDst = Join-Path $ServerRoot "Mods\__core__"
if (Test-Path $coreSrc) {
    if (Test-Path $coreDst) { Remove-Item -Path $coreDst -Recurse -Force }
    Copy-Item -Path $coreSrc -Destination (Join-Path $ServerRoot "Mods") -Recurse -Force
    Write-Host "  Copied: Mods\__core__" -ForegroundColor Green
}

# WebClient (optional; overwrites existing)
$wcSrc = Join-Path $Staging "WebClient"
$wcDst = Join-Path $ServerRoot "WebClient"
if (Test-Path $wcSrc) {
    if (Test-Path $wcDst) { Remove-Item -Path $wcDst -Recurse -Force }
    Copy-Item -Path $wcSrc -Destination $ServerRoot -Recurse -Force
    Write-Host "  Copied: WebClient" -ForegroundColor Green
}

Write-Host "Done. Your Configs and Storage were not touched." -ForegroundColor Green
Write-Host "Start Docker from dev-files: docker compose up -d" -ForegroundColor Cyan
