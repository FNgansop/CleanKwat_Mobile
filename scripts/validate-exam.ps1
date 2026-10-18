$ErrorActionPreference = 'Stop'
Write-Host '=== CleanKwat - validation examen ===' -ForegroundColor Green

dotnet --info
dotnet restore .\CleanKwat.sln
dotnet build .\CleanKwat\CleanKwat.csproj -f net10.0-android -c Debug

Write-Host ''
Write-Host 'Build Android termine. Lancez maintenant l application sur un emulateur/appareil et capturez les 13 ecrans.' -ForegroundColor Green
Write-Host 'Consultez Docs\SCREENSHOTS_AND_PDF.md et Docs\VIDEO_30S.md.'
