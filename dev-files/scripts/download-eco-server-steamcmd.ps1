# Download Eco dedicated server (Linux) via SteamCMD per wiki:
#   https://wiki.play.eco/en/Setting_Up_a_Server#Server_Through_SteamCMD
#
# Uses cm2network/steamcmd in Docker. App ID 739590 = Eco Server.
#
# Usage:
#   .\download-eco-server-steamcmd.ps1                    # Download to dev-files\eco-server-download (staging)
#   .\download-eco-server-steamcmd.ps1 -TargetServer       # Download directly into Server (fresh server, overwrites)
#   .\download-eco-server-steamcmd.ps1 -StagingPath "D:\eco"  # Custom staging path

param(
    [switch]$TargetServer,
    [string]$StagingPath = ""
)

$ErrorActionPreference = "Stop"
$ScriptDir = $PSScriptRoot
$DevFiles  = Split-Path $ScriptDir -Parent
$ServerRoot = Split-Path $DevFiles -Parent  # C:\workspace\Server

if ($TargetServer) {
    $InstallPath = $ServerRoot
    Write-Host "Downloading Eco Server (739590) directly into Server (fresh server). This will overwrite existing server files." -ForegroundColor Yellow
} else {
    $InstallPath = if ($StagingPath) { $StagingPath } else { Join-Path $DevFiles "eco-server-download" }
    if (-not (Test-Path $InstallPath)) { New-Item -ItemType Directory -Path $InstallPath -Force | Out-Null }
    Write-Host "Downloading Eco Server (739590) to staging: $InstallPath" -ForegroundColor Cyan
    Write-Host "After download, copy EcoServer, linux64, Mods, Configs (templates), WebClient into your Server folder as needed." -ForegroundColor Gray
}

# Resolve for Docker (Windows path must be passed correctly)
$InstallPath = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($InstallPath)

Write-Host "Install path: $InstallPath"
Write-Host "Running SteamCMD (this may take several minutes)..." -ForegroundColor Cyan

docker run --rm `
  -v "${InstallPath}:/server" `
  cm2network/steamcmd `
  /home/steam/steamcmd/steamcmd.sh `
  +force_install_dir /server `
  +login anonymous `
  +app_update 739590 validate `
  +quit

if ($LASTEXITCODE -ne 0) {
    Write-Host "SteamCMD exited with code $LASTEXITCODE. Check Docker and network." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "Download finished." -ForegroundColor Green
if (-not $TargetServer) {
    Write-Host "Copy from staging to Server, e.g.:" -ForegroundColor Gray
    Write-Host "  Copy-Item -Path '$InstallPath\EcoServer' -Destination '$ServerRoot\' -Force"
    Write-Host "  Copy-Item -Path '$InstallPath\linux64' -Destination '$ServerRoot\' -Recurse -Force"
    Write-Host "  (and Mods/__core__, Configs templates, WebClient if needed)"
}
