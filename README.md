# Instructions

## docker-compose
- `docker-compose build`
- `docker-compose up -d`

### Browse to:
- Web API <https://localhost:8001/swagger>
- Front End <https://localhost:8881>
- Hangfire <https://localhost:8001/jobs>

## Visual Studio Debug
- Choose `docker-compose` profile
- Debug

# Architecture
## Back-End
- ASP.NET Core Web API
- EF Core with MS SQL Server
## Front-End
- Blazor Server (jquery, SpreadsheetJS)

# Libraries Used
- Automapper
- MediatR - for CQRS-esque Request/Response
- Hangfire - to offload csv processing to a background job after successful upload (faster response time for upload API requests)
- ETL.NET with SQL Server sink - stream processing

## Unit/Integration Tests
- Xunit
- WebApplicationFactory - Integration Tests