# Öppna arkitekturella frågor

| # | Fråga | Vad vi behöver veta |
|---|---|---|
| 2 | Hur hanteras varukorg och sessionstillstånd när flera instanser körs? | Alternativ: delad cache, databas eller klientsida |
| 3 | Vilken databas- och cachetyp ska användas? | Datamodell och lastprofil. Kandidat till egen ADR |
| 4 | Ska vi bygga egen inloggning med haschade lösenord i databasen eller använda en färdig tjänst/OAuth? |  |
| 5 | Hur autentiseras kunder och administratörer? | Krav på roller och behörigheter. Kandidat till ADR-003 |
| 6 | Behövs en separat adminapplikation eller räcker en roll i samma webbklient? | Hur omfattande adminfunktionerna blir |
| 7 | Vilken Azure-tjänst ska hosta applikationen (App Service eller Container Apps)? | Kunskap om driftmodell och kostnad |
| 8 | När ska en modul brytas ut till egen tjänst? | Mätvärden från drift (last, teamstorlek) |
