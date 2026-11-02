# Freaky Fashion

Kompilerbar .NET 10-grund för en modulär monolit för kläder och accessoarer. Backend körs som en driftsättningsenhet; domänmodulerna har separata projekt utan beroenden till varandra. `WebApi` är kompositionsroten.

## Kom igång

Kräver .NET 10 SDK. Öppna `FreakyFashion.slnx` i VS Code med C# Dev Kit eller kör:

```powershell
dotnet restore FreakyFashion.slnx
dotnet build FreakyFashion.slnx
dotnet run --project src/WebApi
```

API:t exponerar `/health`. Katalog-, order- och betalningscontrollers visar modulernas HTTP-gränser. Affärsflöden kräver att konkreta tjänster och Azure-adaptrar kopplas in.

## Projektstruktur

- `src/Catalog`, `Orders`, `Customers`, `Payments`: isolerade domänprojekt med publika kontrakt.
- `src/Infrastructure`: konfigurations- och integrationsgränser för Azure SQL, Managed Redis och Blob Storage.
- `src/WebApi`: HTTP API och kompositionsrot.
- `tests/UnitTests`: testprojektsskelett.
- `docs/adr`, `docs/c4`: arkitekturbeslut och C4-dokumentation.

## Konfiguration och hemligheter

Konfiguration bindas från `AzureServices` via ASP.NET Core standardkonfiguration. Sätt exempelvis `AzureServices__SqlConnectionString`, `AzureServices__RedisConnectionString` och `AzureServices__BlobServiceUri` som miljövariabler/Azure App Settings. Använd Key Vault-referenser i Azure. Lägg aldrig hemligheter i källkod eller appsettings-filer.

Azure SDK-adaptrar, databas-schema, cacheimplementering, autentisering och beställningshändelser är nästa implementeringssteg. Pipelinebygget publicerar en artefakt; konfigurera Azure DevOps-variablerna `azureServiceConnection` och `azureWebAppName` samt service connection för att aktivera deploymentsteget.

## Arkitekturdokumentation

- [System context](docs/c4/system-context.md)
- [Container diagram](docs/c4/container.md)
- [ADR-001: Modular monolith](docs/adr/001-modular-monolith.md)
- [ADR-002: Relational database](docs/adr/002-relational-database.md)
- [ADR-003: Redis cache](docs/adr/003-redis-cache.md)
- [ADR-004: Async communication](docs/adr/004-async-communication.md)
