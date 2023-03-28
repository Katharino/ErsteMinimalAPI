using MeineErsteAPI;
using MeineErsteAPI.Models;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<FakeDb>();

var app = builder.Build();
app.MapGet("/", () => "Hello World");

// API Routen:
// GET      /charaktere             => gibt eine Liste mit allen Charakteren zurück
// GET      /charaktere/{id}        => gibt Details eines bestimmten Charakters zurück
// POST     /charaktere             => neuen Charakter hinzufügen
// DELETE   /charaktere/{id}        => löscht einen bestimmten Charakter
// PUT      /charaktere/{id}        => updatet einen Charakter

// Aufbau eines Charakters:
// - Id
// - Name
// - Element
// - Waffentyp
// - Sterne (4 oder 5)
// - HabIch

// Alle Charaktere werden zurückgegeben
app.MapGet("/charaktere", CharakterHandlers.AlleCharsZurueckgeben);

// Ein bestimmter Charakter wird zurückgegeben
app.MapGet("/charaktere/{id}", CharakterHandlers.EinCharZurueckgeben);

// Ein neuer Charakter wird angelegt
app.MapPost("/charaktere", CharakterHandlers.CharHinzufuegen);

// Ein bestimmter Charakter wird gelöscht
app.MapDelete("/charaktere/{id}", CharakterHandlers.CharLoeschen);

// Ein bestimmter Charakter wird verändert
app.MapPut("/charaktere/{id}", CharakterHandlers.CharAendern);



app.Run();
