# Instructions
TODO: Add docker-compose instructions
# Architecture
## Back-End
- ASP.NET Core Web API
- EF Core with SQL Server Storage
## Front-End
- Blazor Server
# Libraries Used
- Automapper
- MediatR - for file upload
- Hangfire - to offload csv processing to a background job after successful upload (faster response time for upload API requests)
- ETL.NET with SQL Server sink - stream processing
## Unit/Integration Tests
- Moq
- Xunit