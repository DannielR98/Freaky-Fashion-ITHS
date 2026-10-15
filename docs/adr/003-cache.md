# ADR-003: Cachelagring med Azure Managed Redis

## Status
Antagen

## Kontext
Freaky Fashion har en läsintensiv produktkatalog. Produktinformation läses ofta men ändras mer sällan.
Under kampanjer, exempelvis Black Friday, kan trafiken öka kraftigt. Om alla produktförfrågningar går direkt mot Azure SQL Database kan databasen belastas onödigt mycket och svarstiderna påverkas.

## Alternativ
* Ingen cache, alla läsningar går direkt mot Azure SQL Database
* Lokal cache i applikationen
* Distribuerad cache med Azure Managed Redis

## Beslut
Vi använder Azure Managed Redis som cache för ofta läst produkt- och katalogdata.
Azure SQL Database fortsätter att vara systemets primära datakälla och source of truth.

## Motivering
Redis kan minska antalet återkommande läsningar mot Azure SQL Database och förbättra svarstider vid hög belastning.
Eftersom applikationen ska kunna köras på flera instanser är en distribuerad cache mer lämplig än en lokal cache i varje instans.

## Konsekvenser / Trade-offs
++ Snabbare läsning av produktdata
++ Minskad belastning på Azure SQL Database
++ Bättre stöd för hög trafik och kampanjtoppar
++ Cachen kan delas mellan flera applikationsinstanser

-- Ökad komplexitet i arkitekturen
-- Cache måste hållas uppdaterad när produktdata ändras
-- Risk för tillfälligt inaktuell data om cache-invalidering inte hanteras korrekt
-- Ytterligare Azure-tjänst innebär extra kostnad och drift

