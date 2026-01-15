# DeviceManager
Backend: C#.NET-9.0; Frontend: Angular;


# Backend:

Devices Backend

Übersicht
Dieses Projekt ist das Backend für das Devices-Management-System, umgesetzt in C# .NET 9.0.
Es dient zur Verwaltung von Geräten, bietet RESTful-APIs und ist modular nach DDD-Prinzipien aufgebaut.
Das Backend ist darauf ausgelegt, leicht erweiterbar, testbar und skalierbar zu sein.

Architektur
Die Architektur basiert auf Domain-Driven Design (DDD) und ist in mehrere Layer aufgeteilt:

/src/Devices
  Devices.Domain
    Enthält die Kern-Entities (z. B. Device, OverviewDevice, DetailedDevice, FullDevice) und Repository-Interfaces.
    Entities basieren auf BaseModels für konsistente Felddefinitionen.
  Devices.Infrastructure
    Umsetzung der Repository-Interfaces.
    Persistenz über EF Core (InMemory-DB für Tests / Prototyping).
    BaseModels für Entities können hier liegen, um redundante Definitionen zu vermeiden.
  Devices.Application
    Enthält Services, DTOs und Interfaces.
    Vermittelt zwischen Domain und API, enthält Business-Logik.
    DTOs basieren auf den BaseModels (OverviewDeviceBase, DetailedDeviceBase, FullDeviceBase) zur Vermeidung von Redundanz.
  Devices.Api
    RESTful-Controller, DTO-Mapping, Validierung, Swagger-Dokumentation.
    Optional ResponseWrapper für einheitliche API-Responses.
    Alle Endpoints async/await für Performance und Skalierbarkeit.

Dependency Flow
API greift auf Application Services zu
Application Services greifen auf Repositories zu
Repositories greifen auf Domain Entities und EF Core DbContext zu

Features
- RESTful API für Geräteverwaltung:
  GET /devices/overview -> Liste aller Geräte (übersichtlich)
  GET /devices/{guid} -> Vollständige Details
  GET /devices/{guid}/detailed -> Teil-Details für Detailseite
  POST /devices -> Neue Geräte anlegen (IDs als Metafeld, Guid intern generiert)
  DELETE /devices/{guid} -> Geräte löschen

- Async / Task-basiert für alle Services & Repositories
- Validation & Error Handling über zentralen ResponseWrapper
- Swagger für API-Dokumentation
- Unit Tests für Application Layer (xUnit + Moq)
- InMemory-Datenbank für Tests und schnelle Prototypen

Technologien
Backend: C# .NET 9.0, EF Core (InMemory DB für Tests)
Testing: xUnit, Moq
API: ASP.NET Core Web API, Swagger
Logging: ASP.NET Core ILogger (kann mit Serilog erweitert werden)
Frontend: Angular (getrenntes Projekt, Client lädt JSON-Files und kommuniziert mit API)

Projektstruktur
/src
  /Devices
    /Devices.Api
    /Devices.Application
    /Devices.Domain
    /Devices.Infrastructure

/Tests
  /Devices
    /Devices.Api.Tests
    /Devices.Application.Tests
    /Devices.Domain.Tests
    /Devices.Infrastructure.Tests

- Tests: Fokus auf Application Layer (Services), Repository-Mocks, async-Methoden
- Helpers: FakeLogger und FullDevice-Factory für einfache, lesbare Tests

Einrichtung
1. Backend-Dependencies installieren:
   cd src/Devices/Devices.Api
   dotnet restore

2. Projekt bauen und starten:
   dotnet build
   dotnet run

3. Swagger-Dokumentation aufrufen:
   https://localhost:{PORT}/swagger

4. UnitTests ausführen:
   cd /Tests/Devices.Application.Tests
   dotnet test

Hinweise
- Modular aufgebaut nach DDD -> leicht erweiterbar für mehrere Microservices
- ResponseWrapper sorgt für einheitliche API-Responses (data + meta)
- Alle Services & Repositories async -> Skalierbarkeit & Performance
- Entities nutzen BaseModels als gemeinsame Felddefinitionen
- Tests isolieren Services von Repositories -> schnelle UnitTests ohne DB-Abhängigkeit
- Create-Endpunkte generieren intern die Guid, die ID bleibt ein Metafeld
- Übersicht, Detail und FullDevice haben jeweils eigene DTOs/Entities für gezielte Abfragen
