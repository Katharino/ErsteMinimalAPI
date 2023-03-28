using MeineErsteAPI.Models;
using System.Collections.Concurrent;

namespace MeineErsteAPI
{
    public class CharakterHandlers
    {
        public static IEnumerable<Charakter> AlleCharsZurueckgeben(FakeDb fakeDb) => fakeDb.CharDict.Values;

        public static IResult EinCharZurueckgeben(int id, FakeDb fakeDb)
        {
            if (fakeDb.CharDict.TryGetValue(id, out Charakter? charakter))
                return Results.Ok(charakter);
            return Results.NotFound();
        }

        public static IResult CharHinzufuegen(CharDto neuerChar, FakeDb fakeDb)
        {
            fakeDb.NaechsterCharId++;

            var charZumHinzufuegen = new Charakter
            {
                Id = fakeDb.NaechsterCharId,
                Name = neuerChar.Name,
                Element = neuerChar.Element,
                Waffentyp = neuerChar.Waffentyp,
                Sterne = neuerChar.Sterne,
                HabIch = neuerChar.HabIch
            };

            if (!fakeDb.CharDict.TryAdd(fakeDb.NaechsterCharId, charZumHinzufuegen))
                Results.StatusCode(StatusCodes.Status500InternalServerError);

            return Results.Created($"/charaktere/{fakeDb.NaechsterCharId}", charZumHinzufuegen);
        }

        public static IResult CharLoeschen(int id, FakeDb fakeDb)
        {
            if (!fakeDb.CharDict.Remove(id, out var _))
                return Results.NotFound("Kein Charakter mit dieser Id");

            fakeDb.NaechsterCharId--;

            return Results.NoContent();
        }

        public static IResult CharAendern(int id, CharDto charGeupdated, FakeDb fakeDb)
        {
            if (!fakeDb.CharDict.TryGetValue(id, out Charakter? charakter))
                return Results.NotFound(charakter);

            charakter.Name = charGeupdated.Name; 
            charakter.Element = charGeupdated.Element; 
            charakter.Waffentyp = charGeupdated.Waffentyp; 
            charakter.Sterne = charGeupdated.Sterne; 
            charakter.HabIch = charGeupdated.HabIch;

            return Results.Ok(charakter);
        }
    }
}
