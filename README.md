# Instructions

## docker-compose
- `docker-compose build`
- `docker-compose up -d`

## Visual Studio Debug
- Choose `docker-compose` profile
- Debug

# Architecture
## Back-End
- ASP.NET Core Web API
- EF Core with SQL Server Storage
## Front-End
- Blazor Server (jquery, SpreadsheetJS)

# Libraries Used
- Automapper
- MediatR - for file upload
- Hangfire - to offload csv processing to a background job after successful upload (faster response time for upload API requests)
- ETL.NET with SQL Server sink - stream processing
## Unit/Integration Tests
- Moq
- Xunit