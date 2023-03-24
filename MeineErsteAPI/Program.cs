using MeineErsteAPI.Models;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World");

// API Routen:
// GET      /charaktere             => gibt eine Liste mit allen Charakteren zur�ck
// GET      /charaktere/{id}        => gibt Details eines bestimmten Charakters zur�ck
// POST     /charaktere             => neuen Charakter hinzuf�gen
// DELETE   /charaktere/{id}        => l�scht einen bestimmten Charakter
// PUT      /charaktere/{id}        => updatet einen Charakter

// Aufbau eines Charakters:
// - Id
// - Name
// - Element
// - Waffentyp
// - Sterne (4 oder 5)
// - HabIch

var naechsterCharId = 0;
var charDict = new ConcurrentDictionary<int, Charakter>();


// Alle Charaktere werden zur�ckgegeben
app.MapGet("/charaktere", () => charDict.Values);


// Ein bestimmter Charakter wird zur�ckgegeben
app.MapGet("/charaktere/{id}", (int id) =>
{
	if (charDict.TryGetValue(id, out Charakter? charakter))
		return Results.Ok(charakter);
	return Results.NotFound();
});


// Ein neuer Charakter wird angelegt
app.MapPost("/charaktere", (CharDto neuerChar) => 
{
    // Neue Id erstellen
    naechsterCharId++;
    // Andere Vorgehensweise f�r Multithreading (besser):
    // Interlocked.Increment(ref naechsterChar);

    // Dto in Model umwandeln (CharErstellen => Charakter)
    var charZumHinzufuegen = new Charakter
    {
        Id = naechsterCharId,
        Name = neuerChar.Name,
        Element = neuerChar.Element,
        Waffentyp = neuerChar.Waffentyp,
        Sterne = neuerChar.Sterne,
        HabIch = neuerChar.HabIch
    };

    // Charakter der Liste hinzuf�gen
    // Falls hier ein Fehler auftreten sollte (was NIE passieren sollte) wird ein Statuscode (ServerError) zur�ckgegeben
    if (!charDict.TryAdd(naechsterCharId, charZumHinzufuegen))
        Results.StatusCode(StatusCodes.Status500InternalServerError);

    // Return Created (Statuscode 201)
    // Bei Created gibt man den Pfad wo sich diese Objekt befindet und das Objekt zur�ck
    return Results.Created($"/charaktere/{naechsterCharId}", charZumHinzufuegen);
});


// Ein bestimmter Charakter wird gel�scht
app.MapDelete("/charaktere/{id}", (int id) => 
{
    if (!charDict.Remove(id, out var _))
        return Results.NotFound("Kein Charakter mit dieser Id");

    naechsterCharId--;

    return Results.NoContent();
});


// Ein bestimmter Charakter wird ver�ndert
app.MapPut("/charaktere/{id}", (int id, CharDto charGeupdated) => 
{
    if (!charDict.TryGetValue(id, out Charakter? charakter))
        return Results.NotFound(charakter);

    charakter.Name = charGeupdated.Name; 
    charakter.Element = charGeupdated.Element; 
    charakter.Waffentyp = charGeupdated.Waffentyp; 
    charakter.Sterne = charGeupdated.Sterne; 
    charakter.HabIch = charGeupdated.HabIch;

    return Results.Ok(charakter);
} );




app.Run();
