# Gruppövning 5 – Datalagring

## Val av lagring per datatyp
## Val av lagring per datatyp

| Typ av data | Vald databas eller lagringslösning | Motivering |
| :--- | :--- | :--- |
| Kundinformation | Azure SQL Database | Strukturerad data med relationer och höga krav på korrekthet och konsistens |
| Produktinformation | Azure SQL Database | Strukturerad produktdata med relationer till exempelvis kategorier |
| Order och orderrader | Azure SQL Database | Kräver stark datakonsistens och stöd för transaktioner |
| Betalningsstatus/referenser | Azure SQL Database | Hör ihop med orderdata och behöver vara korrekt och spårbar |
| Produktbilder | Azure Blob Storage | Ostrukturerade filer som är bättre lämpade för objektlagring än relationsdatabas |
| Produktcache | Azure Managed Redis | Beslutad distribuerad cache för ofta läst produkt- och katalogdata. Azure Managed Redis minskar belastningen på Azure SQL Database vid kampanjtoppar och ingår i den valda arkitekturen (se ADR-003). Azure SQL Database är fortfarande source of truth. |


## Delad databas eller databas per tjänst/modul?
Eftersom Freaky Fashion initialt byggs som en modulär monolit använder vi en gemensam Azure SQL Database. Varje modul ska dock ha tydligt ansvar för sin egen data. Detta minskar den tekniska och operationella komplexiteten för det mindre utvecklingsteamet. Om delar av systemet senare bryts ut till självständiga tjänster kan respektive tjänsts datalagring separeras.

## Varför inte Cosmos DB?
Azure Cosmos DB övervägdes, särskilt för produktkatalogen, men valdes inte eftersom produktdatan i nuläget är tillräckligt strukturerad och passar väl i en relationsdatabas. Att införa ytterligare en databasteknik skulle öka komplexiteten utan ett tydligt behov.

## C4 Container Diagram
![alt text](/pics/image.png)
Diagrammet visar den valda containerarkitekturen för Freaky Fashion och hur applikationen använder Azure SQL Database, Azure Blob Storage och Azure Managed Redis samt integrerar med extern betaltjänst.

