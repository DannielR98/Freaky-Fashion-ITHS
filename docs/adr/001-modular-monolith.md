# ADR-001: Använd modulär monolit

Status: Accepterad | Datum: 2026-10-07

## Kontext
Freaky Fashion förvaltas av ett litet utvecklingsteam och behöver låg operationell och kognitiv komplexitet. Systemet ska samtidigt klara 10× normal trafik under Black Friday och ha tydliga domängränser (Katalog, Order, Kund, Betalning). Driftsättning sker i Azure.

## Alternativ
- Traditionell monolit
- Modulär monolit
- Mikrotjänster

## Beslut
Vi bygger systemet som en modulär monolit med modulerna Katalog, Order, Kund och Betalning. Applikationen skalas horisontellt med auto-skalning av instanser i Azure (t.ex. App Service eller Container Apps).

## Motivering
- En driftsättningsenhet ger enkel CI/CD och felsökning för ett litet team.
- Tydliga modulgränser ger underhållbarhet utan mikrotjänsternas nätverks- och driftkomplexitet (distribuerade transaktioner, tracing, många pipelines).
- Horisontell skalning av hela applikationen räcker för att möta 10× trafik.
- Modulgränserna gör det möjligt att senare bryta ut en modul om behov uppstår.

## Konsekvenser / trade-offs

| Trade-off / risk | Mitigering |
|---|---|
| Moduler kan inte skalas oberoende, hela applikationen skalas ihop | Caching avlastar katalogen. Extra instanser under kampanjer är billigare än mikrotjänstdrift |
| Modulgränser kan eroderas över tid (tysta direktberoenden) | Moduler exponerar endast publika kontrakt. Arkitekturtester i CI som felar vid otillåtna beroenden |
| Delad databas kan leda till cross-modul-joins | Ett schema per modul, ingen åtkomst till andra modulers tabeller |
| En driftsättningsenhet: ett allvarligt fel eller en dålig release kan påverka hela systemet | Flera instanser, health checks och möjlighet till snabb rollback i pipelinen |

Fördelar: enklare utveckling, testning och deployment samt lägre driftkostnad. Vi accepterar nackdelarna eftersom drivkraften "litet team" väger tyngre än oberoende skalning.
