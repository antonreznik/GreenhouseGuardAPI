# Greenhouse Guard API

## Run the backend

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), then run from the repository root:

```bash
dotnet dev-certs https --trust
dotnet run --project GreenhouseGuard/GreenhouseGuard.csproj --launch-profile https
```

The API listens at `https://localhost:7040`. In Development, its OpenAPI document is available at `https://localhost:7040/openapi/v1.json`.

The backend uses SQLite; its connection string is configured in `GreenhouseGuard/appsettings.json`.

## Test data
Use GreenhouseGuard.http file to send requests with random readings.
- Create 20 Sensor Readings
- Create Sensor Reading

## Architecture

The solution separates responsibilities into layers:

- **GreenhouseGuard** hosts the ASP.NET Core API, HTTP endpoints, exception handling, and SignalR notifications to connected clients.
- **Application** contains request handlers, sensor analysis and in-memory storage services, DTOs, and interfaces.
- **Domain** defines core entities such as sensor readings and anomalies.
- **Infrastructure** implements data access with Entity Framework Core and SQLite, including database migrations.

The API wires these layers together through dependency injection.

## Future improvements
- proper logging
- thread safe in-memory stores
- increase test coverage
