# Use the .NET 8.0 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the working directory
COPY . .

# Restore NuGet packages
RUN dotnet restore Tours.Api/Tours.Api.csproj

# Build the project
RUN dotnet build Tours.Api/Tours.Api.csproj -c Release -o /app/build

# Publish the project
RUN dotnet publish Tours.Api/Tours.Api.csproj -c Release -o /app/publish

# Use the ASP.NET Core runtime image as the base image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published files from the build image to the runtime image
COPY --from=build /app/publish .

# Expose the port that the app will listen on
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Tours.Api.dll"]
