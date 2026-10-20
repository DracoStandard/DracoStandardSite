# DracoStandard static site

The public site (home page, rankings, army stats, random army selector, PDF downloads)
as a Blazor WebAssembly app. It runs entirely in the visitor's browser and reads its data
from `wwwroot/data/*.json`, so it needs no server or database while people are using it.
It is hosted on Cloudflare Pages.

## Projects

| Project | What it does |
|---|---|
| `DracoStandardSite.Static` | The site itself. |
| `DracoStandardSite.Shared` | The data classes (`Comp`, `CompResult`, `AllRankings`, ...) used by the site and the export tool. |
| `DracoStandardSite.Export` | Reads the database views and writes the JSON files into `DracoStandardSite.Static/wwwroot/data`. |
| `DracoStandardSite` | The original server site. Still holds the Army Builder and the PDF files. |

## Updating the results

1. Edit the data in SQL as usual.
2. Run `.\publish-data.ps1` from the repository folder.
   It exports the data, commits the JSON files and pushes. GitHub Actions then
   builds and deploys the site (about two minutes).

Alternatively: GitHub > Actions > "Publish static site to Cloudflare Pages" > Run workflow,
with "refresh_data" ticked. That exports from the database on GitHub's machines, so it needs
the `DB_CONNECTION` secret and the SQL firewall must allow GitHub.

## Running locally

Set `DracoStandardSite.Static` as the startup project in Visual Studio and press F5.
Run the export tool once first so `wwwroot/data` has files in it.
PDF links only work on the live site (the PDFs are added during publishing).

## GitHub secrets needed

| Secret | Value |
|---|---|
| `CLOUDFLARE_API_TOKEN` | Cloudflare API token with the "Cloudflare Pages: Edit" permission |
| `CLOUDFLARE_ACCOUNT_ID` | Your Cloudflare account ID (shown on the dashboard overview) |
| `DB_CONNECTION` | Only for the "refresh_data" option: the SQL connection string |
