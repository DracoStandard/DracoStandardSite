# Exports the latest results from the database and publishes them to the live site.
# Run from the repository folder in PowerShell:   .\publish-data.ps1
#
# Uses the same connection string as the original web project (Visual Studio user secrets).

$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot

dotnet run --project DracoStandardSite.Export\DracoStandardSite.Export.csproj
if ($LASTEXITCODE -ne 0) { throw "Export failed - nothing was published." }

git add DracoStandardSite.Static/wwwroot/data
git diff --cached --quiet
if ($LASTEXITCODE -eq 0) {
    Write-Host "No data has changed since the last publish."
    exit 0
}

git commit -m "Update site data $(Get-Date -Format 'yyyy-MM-dd HH:mm')"
git push
Write-Host "Pushed. The site will update in a couple of minutes (GitHub > Actions shows progress)."
