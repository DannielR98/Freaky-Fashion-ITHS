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

## Containrar och ansvar

| Container | Ansvar |
|---|---|
| Webbklient | Kundens och adminens gränssnitt i webbläsaren |
| Applikation | All affärslogik, uppdelad i moduler. Skalas horisontellt |
| Databas | Persistent lagring av produkter, ordrar och kunder |
