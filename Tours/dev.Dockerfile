FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /app

COPY . .

ENTRYPOINT [ "dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080", "--project", "Tours.Api" ]
