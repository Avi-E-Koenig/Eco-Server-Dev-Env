# Check Server directory size to see if Docker/download is writing data.
# Run from dev-files or Server root; pass -Watch to recheck every 30s and show growth.

param(
    [switch]$Watch
)

$serverPath = if (Test-Path "C:\workspace\Server") { "C:\workspace\Server" } else { Join-Path $PSScriptRoot ".." }
$serverPath = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($serverPath)

function Get-ServerSizeGB {
    $bytes = (Get-ChildItem -Path $serverPath -Recurse -File -ErrorAction SilentlyContinue | Measure-Object -Property Length -Sum).Sum
    [math]::Round($bytes / 1GB, 3)
}

if ($Watch) {
    Write-Host "Watching Server size (Ctrl+C to stop). Path: $serverPath"
    $prev = Get-ServerSizeGB
    Write-Host ([DateTime]::Now.ToString("HH:mm:ss")) "Current size: $prev GB"
    while ($true) {
        Start-Sleep -Seconds 30
        $curr = Get-ServerSizeGB
        $delta = $curr - $prev
        $sign = if ($delta -ge 0) { "+" } else { "" }
        Write-Host ([DateTime]::Now.ToString("HH:mm:ss")) "Size: $curr GB ($sign$([math]::Round($delta, 3)) GB)"
        $prev = $curr
    }
} else {
    $gb = Get-ServerSizeGB
    Write-Host "Server path: $serverPath"
    Write-Host "Total size:  $gb GB"
}
