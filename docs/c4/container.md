# Container Diagram (utkast)

Teknikneutralt utkast. Modulerna Katalog, Order, Kund och Betalning ligger inuti applikationen (se [ADR-001](../adr/001-modular-monolith.md)) och är inte egna containers.

```mermaid
C4Container
  title Container Diagram – Freaky Fashion (utkast)

  Person(kund, "Kund")
  Person(admin, "Administratör")

  System_Boundary(ff, "Freaky Fashion") {
    Container(web, "Webbklient", "Webbläsare", "Kund- och adminvy")
    Container(app, "Applikation", "Modulär monolit", "Moduler: Katalog, Order, Kund, Betalning. Skalas horisontellt")
    ContainerDb(db, "Databas", "Relationsdatabas", "Produkter, ordrar, kunder")
    Container(cache, "Cache", "Cache", "Katalogdata")
    Container(kö, "Meddelandekö", "Kö", "Asynkrona notifieringar")
    Container(secrets, "Secrets-hantering", "Nyckelvalv", "Hemligheter, least privilege")
  }

  System_Ext(betal, "Betaltjänst")
  System_Ext(epost, "E-post/SMS-leverantör")

  Rel(kund, web, "Använder", "HTTPS")
  Rel(admin, web, "Använder", "HTTPS")
  Rel(web, app, "Anropar", "HTTP/REST")
  Rel(app, db, "Läser/skriver")
  Rel(app, cache, "Läser katalogdata")
  Rel(app, secrets, "Hämtar hemligheter")
  Rel(app, betal, "Begär betalning (timeout, idempotens)", "HTTPS")
  Rel(betal, app, "Betalningsresultat", "Webhook")
  Rel(app, kö, "Publicerar notifieringshändelser")
  Rel(kö, epost, "Levererar notifieringar")
```

## Containrar och ansvar

| Container | Ansvar |
|---|---|
| Webbklient | Kundens och adminens gränssnitt i webbläsaren |
| Applikation | All affärslogik, uppdelad i moduler. Skalas horisontellt |
| Databas | Persistent lagring av produkter, ordrar och kunder |
| Cache | Snabb läsning av katalogdata under hög last |
| Meddelandekö | Asynkron leverans av notifieringar |
| Secrets-hantering | Hemligheter utanför källkoden |
