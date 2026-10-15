# ADR-002: Kommunikationsmönster mellan moduler och externa system

Status: Accepterad | Datum: 2026-10-07

## Kontext
Fel i externa tjänster, till exempel betaltjänsten, får inte krascha köpflödet. Katalogen är läsintensiv under belastningstoppar. Teamet är litet och vill undvika onödig komplexitet. Beslutet bygger på [ADR-001](001-modular-monolith.md).

## Alternativ
- Synkron kommunikation överallt
- Asynkron (händelsedriven) kommunikation överallt
- Hybrid: synkront som default, asynkront där fördröjning eller fel måste isoleras

## Beslut
Vi använder en hybrid:

| Interaktion | Mönster |
|---|---|
| Kund ↔ Katalog | Synkron (HTTP/REST) + caching |
| Order ↔ Betaltjänst | Synkron begäran (timeout, idempotens) + asynkron callback/webhook |
| Order ↔ Lager | Synkron reservation + asynkron fulfillment |
| Order ↔ Notifiering | Asynkron via meddelandekö (Azure Service Bus) |
| Modul ↔ Modul | Synkrona metodanrop som default + interna domänhändelser |

## Motivering
- Synkront där användaren väntar på svar är enklast och ger omedelbar feedback.
- Asynkront där fördröjning är acceptabel skyddar köpflödet mot fel i externa tjänster och jämnar ut belastning.
- Timeout och idempotens gör att omförsök inte ger dubbla betalningar.
- Caching avlastar katalogen vid 10× trafik.

## Konsekvenser / trade-offs

| Trade-off / risk | Mitigering |
|---|---|
| Asynkrona flöden ger eventual consistency och är svårare att felsöka | Korrelations-id i loggar |
| Meddelanden kan levereras mer än en gång eller gå förlorade | Dead-letter-kö, retry med backoff, idempotent hantering |
| Webhooks från betaltjänsten kan komma dubbelt eller sent | Idempotensnycklar, signaturverifiering, avstämning av obekräftade betalningar |
| Fler rörliga delar (kö, webhook-hantering) för ett litet team | Begränsa asynkronitet till de flöden som listas ovan |

Fördelar: köpflödet tål att notifiering eller betaltjänst är långsam eller nere.
