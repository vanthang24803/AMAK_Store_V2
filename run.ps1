Clear-Host

$rootLocation = Get-Location

Set-Location ./Src/AMAK.API

Write-Host "Running the application... 🚀"

dotnet watch run

try {
    dotnet watch run
}
finally {
    Set-Location $rootLocation
      Write-Host "Returned to the root location! 👋"
}
