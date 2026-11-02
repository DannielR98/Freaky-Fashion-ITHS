# ADR-004: Synkron REST och asynkrona händelser

## Status
Antagen

## Kontext
Frontend behöver ett enkelt HTTP API. Betalning initieras hos en extern betaltjänst och måste ge ett direkt svar i kassan. Bakgrundsarbete som orderbekräftelser och bildbehandling ska inte blockera användarens anrop.

## Alternativ
* Synkrona REST-anrop för alla integrationer
* Asynkrona meddelanden för alla integrationer
* Synkron REST för frontend och betalningsinitiering, händelser för bakgrundsjobb

## Beslut
Frontend kommunicerar synkront med backend via REST/HTTPS. Backend initierar betalning via synkront REST/HTTPS och tar emot provider-webhooks. Bakgrundsprocesser använder asynkrona händelser via Azure Service Bus (eller Event Grid när fan-out behövs); exempel är orderbekräftelser och bildbehandling. Orderhändelser skrivs via outbox i SQL för tillförlitlig publicering.

## Motivering
- Kassan får direkt svar från betaltjänsten medan långsamma bakgrundsuppgifter hålls utanför användarflödet
- Köer buffrar trafiktoppar och möjliggör retry/dead-letter
- Outbox och idempotenta konsumenter behövs för tillförlitliga orderhändelser

## Konsekvenser / Trade-offs
++ REST ger enkel request/response för användarflöden
++ Asynkron bearbetning isolerar bakgrundsjobb från svarstider och toppar
-- Betaltjänsten måste skyddas med timeout, retry och verifiering av webhook-signatur
-- Händelser kräver outbox, idempotens och övervakning av dead-letter
-- Ytterligare Azure-tjänster innebär extra kostnad och drift
