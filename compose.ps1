param(
    [string]$Command
)

Clear-Host

$rootLocation = Get-Location

Set-Location ./Docker

function Show-Usage {
    Write-Host "Usage:"
    Write-Host "  dev      - Start docker-compose for development"
    Write-Host "  prod     - Start docker-compose for production"
    Write-Host "  preview  - Start docker-compose for preview"
}

switch ($Command) {
    "dev" {
        Write-Host "Starting docker-compose up for dev..."
        docker-compose -f docker-compose.dev.yml up -d
        Set-Location ..
    }
    "prod" {
        Write-Host "Starting docker-compose up for production..."
        docker-compose -f docker-compose.prod.yml up
        Set-Location ..
    }
    "preview" {
        Write-Host "Starting docker-compose up for preview..."
        docker-compose -f docker-compose.preview.yml up
    }
    default {
        Show-Usage
    }
}
