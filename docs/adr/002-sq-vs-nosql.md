
# ADR-002: Datalagring – Relationsdatabas (SQL) [2]

## Status
Antagen

## Kontext
E-handeln behöver lagra transaktionsdata som order, kunduppgifter och produktkatalog. Det finns starka krav på dataintegritet, relaterad data och säkra transaktioner
Ordrar och betalningar vill ha transaktioner, medan katalogen är läsintensiv och kanske passar andra lagringsformer

## Alternativ
* Relationsdatabas (SQL, t.ex. Azure SQL Database)
* NoSQL-dokumentdatabas (t.ex. Azure Cosmos DB)

## Beslut
Vi använder en **Relationsdatabas (SQL)** för transaktionsdata

## Motivering
Order- och kundhantering kräver garanterad dataintegritet (ACID-transaktioner) och strukturerade relationer. SQL är beprövat, välkänt för teamet och passar e-handelns datamodell

## Konsekvenser / Trade-offs
++ Garanterad dataintegritet och konsistens vid transaktioner

++ Enkelt att ställa komplexa frågor mellan relaterade tabeller 

-- Mindre flexibelt schema jämfört med NoSQL om datastrukturen ändras ofta
