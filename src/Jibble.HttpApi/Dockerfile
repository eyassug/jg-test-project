#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Jibble.HttpApi/Jibble.HttpApi.csproj", "src/Jibble.HttpApi/"]
COPY ["src/Jibble.EntityFrameworkCore/Jibble.EntityFrameworkCore.csproj", "src/Jibble.EntityFrameworkCore/"]
COPY ["src/Jibble.Domain/Jibble.Domain.csproj", "src/Jibble.Domain/"]
RUN dotnet restore "src/Jibble.HttpApi/Jibble.HttpApi.csproj"
COPY . .
WORKDIR "/src/src/Jibble.HttpApi"
RUN dotnet build "Jibble.HttpApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jibble.HttpApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jibble.HttpApi.dll"]