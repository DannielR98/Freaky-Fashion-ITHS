# Container Diagram (utkast)

Teknikneutralt utkast. Modulerna Katalog, Order, Kund och Betalning ligger inuti applikationen (se [ADR-001](../adr/001-modular-monolith.md)) och är inte egna containers.

```mermaid
%%{init: {"c4": {"diagramMarginX": 50, "diagramMarginY": 50, "c4ShapeMargin": 120, "c4ShapePadding": 20, "width": 260, "height": 140, "boxMargin": 20, "personFontSize": "16px", "systemFontSize": "16px", "containerFontSize": "16px", "messageFontSize": "15px"}}}%%
C4Container
    title E-handel – Containerdiagram

    Person(kund, "Kund", "Handlar och betalar")
    System_Ext(betaltjanst, "Betaltjänst", "Genomför betalningar")

    System_Boundary(ehandel, "E-handel") {
        Container(webb, "Webbapplikation", "Webbgränssnitt", "Visar produkter och hanterar kassan")
        Container(api, "Backend / API", "Applikation", "Hanterar kunder, order och betalningsflöden")
        ContainerDb(db, "Relationsdatabas", "SQL", "Kunder, order, produkter och betalningsstatus")
        
    }

    Rel(kund, webb, "Använder via HTTPS")
    Rel(webb, api, "Hämtar produkter och skickar order via HTTPS")
    Rel(api, db, "Läser och skriver via SQL", "ACID-transaktioner")
    Rel(api, betaltjanst, "Initierar betalning via HTTPS")
    Rel(betaltjanst, api, "Skickar betalningsstatus via webhook")
    UpdateRelStyle(api, betaltjanst, $offsetX="-40", $offsetY="-20")
    UpdateRelStyle(betaltjanst, api, $offsetX="40", $offsetY="20")
    UpdateRelStyle(webb, api, $offsetX="-125", $offsetY="-11")

    
    UpdateLayoutConfig($c4ShapeInRow="2", $c4BoundaryInRow="1")
```

@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

title Freaky Fashion – Containerdiagram (med kommunikation)

LAYOUT_WITH_LEGEND()

Person(kund, "Kund", "Söker, handlar och betalar")
Person(admin, "Administratör", "Hanterar sortiment och ordrar")
System_Ext(betaltjanst, "Betaltjänst", "Genomför betalningar")
System_Ext(epost, "E-post/SMS-leverantör", "Skickar notifieringar till kunder")

System_Boundary(ff, "Freaky Fashion e-handelsplattform") {
    Container(webb, "Webbapplikation", "Webbgränssnitt", "Visar produkter och hanterar kassan")
    Container(api, "Backend / API", "Modulär monolit", "Katalog, Order, Kund, Betalning")
    ContainerDb(db, "Azure SQL Database", "Relationsdatabas", "Source of truth, inkl. outbox")
    ContainerDb(redis, "Azure Managed Redis", "Cache", "Cachad produkt- och katalogdata")
    ContainerQueue(bus, "Azure Service Bus", "Meddelandekö", "OrderLagd")
    Container(func, "Notifieringsfunktion", "Azure Function", "Skickar orderbekräftelser")
}

Rel(kund, webb, "Använder", "HTTPS")
Rel(admin, webb, "Hanterar produkter och ordrar", "HTTPS")
Rel(webb, api, "Hämtar produkter, skickar order", "REST/HTTPS, synkron")
Rel(api, redis, "Läser och invaliderar cache", "Synkron")
Rel(api, db, "Läser och skriver", "SQL, synkron")
Rel(api, betaltjanst, "Initierar betalning", "REST/HTTPS, synkron")
Rel_Back(api, betaltjanst, "Skickar betalningsstatus", "Webhook, asynkron")
Rel(api, bus, "Publicerar OrderLagd (via outbox)", "Asynkront meddelande")
Rel(bus, func, "Levererar meddelande", "Asynkron")
Rel(func, epost, "Skickar orderbekräftelse", "REST/HTTPS, synkron")
@enduml

## Containrar och ansvar

| Container | Ansvar |
|---|---|
| Webbklient | Kundens och adminens gränssnitt i webbläsaren |
| Applikation | All affärslogik, uppdelad i moduler. Skalas horisontellt |
| Databas | Persistent lagring av produkter, ordrar och kunder |
