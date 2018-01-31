# Board game architecture

* Spelmotorn

Vi har byggt spelmotorn runt att vi har en objektklass GameBoards, som har en lista med GameBoard objekt.
GameBoards har en properties för att hämta ut GameBoard som är öppna att joina (OpenGames). Metoder att lägga till nya GameBoard (Add), få ut ett GameBoard (Get) genom en så kallad GameKey (6 slumpade siffror och bokstäver), en annan metod som genom en GameKey kan ta bort ett bord (Remove).
Den har även två mindre metoder för att kolla om bordet finns (DoesItExist) och en private metod för att genera game key när man lägger in ett nytt spelbord (GenerateGameKey).

GameBoard objekten har i sin tur en array med två Player objekt (Players), samt variabler för spara GameKey (GameKey), om spelet är privat (Private), vems tur det är (Turn) och när senaste händelsen på bordet skede (lastUpdate).
Finns även properties för kolla om spelet är aktivt (Active), och om båda spelarna har sett spelets slutscenen (BothPlayerHasSeenEndScreen).
Metoder finns för att skjuta på motståndarens skepp (Shoot) och kontrollera om någon användare har förlorat (IsGameEnd). 

Player objekten har i sin tur en lista med sju Boat objekt (Boats), samt spelarens namn (Name), en lista med positioner som motståndaren har skjutit på (Hits) och om denna användaren har sett spelet slutscen (HaveSeenEndSeen).
Properties för returnera om spelaren har förlorat (HasPlayerLost). Båtar antalet och typ är hårdkodade, och läggs till i spelarens constructor.
Där anropas även metoden PositionsBoats, som random placerar ut båtarna på ledig plats på spelbordet.
I övrigt finns det metoder för returnera en position rutas (Square objekt) värden (CheckPosition), samt liknade metoder för endast kolla om en position har träffats innan (IsPositionAlreadyHit), eller kolla om det finns någon båt på en eller flertalet positioner (IsAnyBoatHere).
Slutligen finns det även en metod att hantera skjutningarna IsABoatHit, som kontrollerar om positionen inte har skjutits på innan och om det finns en båt där. Och sedan sparar denna information.

Boat objekt har en array med två Position objekt för att sätta dennes position (Positions), samt en lista med Position objekt för att sätta träffar på båten (Hits).
Båt objekten har också en Enum för BoatType (Type), som också sätter båtens storlek (properties: Size). Detta då nummervärdet på enum är just storleken. Den har även properties för kolla om båten har blivit sänkt (Sink).
Metoder för sätta båtens position (SetPositions), kolla om båten är på en eller inom flertalet positioner (AnyYouHere). Samt metod IsItAHit för att hjälpa skjutningarna med att kolla om båten är på en position, och i sådana fall spara träffen.

Positions objekten innehåller bara variabler (x, y) och properties (X, Y) för koordinater på spelbordet, enklare objektklass för slippa ha två eller fler variabler för samma sak över allt.

Square objekt innehåller enbart instans variabler för sätta träff (HaveBeenHit) och om det finns en båt i denna rutan (HaveBoat), dessa är ju endast tänkt att transportera denna datan till webbapplikationen.

* Webbapplikationen

Har två modeller, en för skicka e-post (MailModel) (bortkommenterad på Github) och GameModel för att stöta webbapplikationen med hantera data från och till spelmotorn.

GameModel är en objektklass, som har två variabler Game som innehåller ett GameBoard och Letters som innehåller en lista med bokstäverna A till J.
Har properties att returnera Player objektet för Session (YourPlayer) eller motståndaren (EnemyPlayer), spelar id för respektive (YourPlayerId, EnemyPlayerId).
En metod (PositionsClasses) för att genom ett Position objekt ta emot information (Square objekt) från spelmotorn om den rutan, och omvandla det till CSS klasser.
Samt en annan metod att omvandla skjutning från frontend till skjutningar i spelmotorn.

Har en Helper class (MessageHandler) som hjälper till att skriva ut meddelanden i form av Bootstrap alerts.

HomeControllen har en constructor som deklarerar en static GameBoards objekt samt en static Dictionary innehållandes strängnyckel och som värden sträng lista (messages) för spara meddelanden till användaren.
Actions med View för startsidan (Index), för skapandet av ett spel (NewGame), för joina existerande spel (JoinGame), väntsida för den som skapade spelet (Waiting), Spelsidan (Game), spel uppdateringssida (UpdateGame) som skickar datan som JSON (Ajax), skjutanrop (Shoot) som skickar resultatet som JSON om det requestas (Ajax), ett gametest (GameTestInfo) är vanligtvis bortkommenterat, samt slutscenen (GameEnd).
Actions utan View för hanterande av skapande av ett spel (NewJoinGame), för hanterandet av joina ett game (JoinGame) och en (EmailFriend) för att skicka inbjudningsmail (bortkommenterad).
HomeControllen har också fyra privata metoder för att underlätta saker som är återkommer i flera action metoder, GetSessionGame hämtar spelbord med session Game key och returnerar den inne i en GameModel, RemoveSessionGame låter spelet tas bort om båda användarna har sett slutscenen, AddMessage hjälper till att föra in ett meddelande i messages samt InsertMessages som skickar med meddelanden till ViewBag:en.

Utöver det finns en extra css för spelet (battleship.css) och javascript fil (battleship.js).
Det sistnämnda har ett klickevent om man klickar på valfri ruta på motståndarens spelplan när det är ens tur, då anropar den Shoot action och hanterar svaret helt utan att ladda upp sidan.
De finns även en funktion som laddas var femte sekund, vilket anropar UpdateGame action för att få speldatan och hanterar den sedan. Detta för slippa uppdatera hela sidan, samt göra de hela mera dynamiskt.