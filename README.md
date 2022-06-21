# Instructions

## docker-compose
- `docker-compose build`
- `docker-compose up -d`

### Browse to:
- Web API <https://localhost:8001/swagger>
- Front End <https://localhost:8881>
- Hangfire <https://localhost:8001/jobs>

## Visual Studio Debug (local)

- Update connection strings in `appsettings.json` to a valid SQL Server connection (LocalDb, SQLEXPRESS ...): No need to create database as it will be automatically created.

`"ConnectionStrings": {
    "Default": "Server=localhost\\SQLEXPRESS;Database=employees;Trusted_Connection=True;",
    "Hangfire": "Server=localhost\\SQLEXPRESS;Database=employees;Trusted_Connection=True;"
  },`
- Set multiple Start-up projects
- Choose Jibble.HttpApi and Jibble.Web Projects

## `dotnet run`
- Update connection strings in `appsettings.json`  to a valid SQL Server connection (LocalDb, SQLEXPRESS ...): No need to create database as it will be automatically created.

`"ConnectionStrings": {
    "Default": "Server=localhost\\SQLEXPRESS;Database=employees;Trusted_Connection=True;",
    "Hangfire": "Server=localhost\\SQLEXPRESS;Database=employees;Trusted_Connection=True;"
  },`
- `cd` to `Jibble.HttpApi` and `Jibble.Web`
- `dotnet run`

- Launch
## Visual Studio Debug (docker-compose)
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