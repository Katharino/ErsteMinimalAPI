using MeineErsteAPI;
using MeineErsteAPI.Models;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<FakeDb>();

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

// Alle Charaktere werden zur�ckgegeben
app.MapGet("/charaktere", CharakterHandlers.AlleCharsZurueckgeben);

// Ein bestimmter Charakter wird zur�ckgegeben
app.MapGet("/charaktere/{id}", CharakterHandlers.EinCharZurueckgeben);

// Ein neuer Charakter wird angelegt
app.MapPost("/charaktere", CharakterHandlers.CharHinzufuegen);

// Ein bestimmter Charakter wird gel�scht
app.MapDelete("/charaktere/{id}", CharakterHandlers.CharLoeschen);

// Ein bestimmter Charakter wird ver�ndert
app.MapPut("/charaktere/{id}", CharakterHandlers.CharAendern);



app.Run();
