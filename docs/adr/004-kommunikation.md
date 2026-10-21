# ADR-004: Asynkron orderbekräftelse via Azure Service Bus

## Status
Antagen

## Kontext
Kunden ska få orderbekräftelse via e-post/SMS. Utskicket är beroende av en extern leverantör och får inte fördröja eller stoppa orderflödet, särskilt inte under Black Friday.

## Alternativ
* Synkront anrop till e-postleverantören i orderflödet
* Bakgrundsjobb i samma applikation (utan kö)
* Azure Service Bus + Azure Function
* Azure Event Grid

## Beslut
Order publicerar ett OrderLagd-meddelande till en Service Bus-kö (via outbox i SQL). En Azure Function konsumerar meddelandet och skickar bekräftelsen.

## Motivering
- Orderflödet blir oberoende av leverantörens tillgänglighet och svarstid
- Kön buffrar toppar och behåller meddelanden vid avbrott
- Retry och dead-letter finns inbyggt
- Event Grid är onödigt eftersom vi har en enda mottagare

## Konsekvenser / Trade-offs
++ Högre tillförlitlighet och lägre kopplingsgrad
++ Skalar vid kampanjer
-- Eventual consistency (mejlet kommer med fördröjning)
-- Outbox och idempotent konsument behövs
-- Ytterligare Azure-tjänster innebär extra kostnad, drift och övervakning