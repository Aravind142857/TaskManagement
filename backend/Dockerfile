# First stage: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY backend.csproj ./
RUN dotnet restore

# Copy the rest of the code and publish the application
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS migration
WORKDIR /app
COPY --from=build /app ./

RUN dotnet ef database update

# Third stage: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .

COPY ./entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh
# Expose port 80 for the API
EXPOSE 80

# Run the application
ENTRYPOINT ["./entrypoint.sh"]