using MeineErsteAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace MeineErsteAPI
{
    public class CharakterHandlers
    {
        public static async Task<IEnumerable<Charakter>> AlleCharsZurueckgeben(CharContext context) => await context.Charaktere.ToListAsync();

        public static async Task<IResult> EinCharZurueckgeben(int id, CharContext context)
        {
            var charakter = await context.Charaktere.FirstOrDefaultAsync(x => x.Id == id);
            if (charakter != null)
                return Results.Ok(charakter);
            return Results.NotFound();
        }

        public static async Task<IResult> CharHinzufuegen(CharDto neuerChar, CharContext context)
        {
            var charZumHinzufuegen = new Charakter
            {
                Name = neuerChar.Name,
                Element = neuerChar.Element,
                Waffentyp = neuerChar.Waffentyp,
                Sterne = neuerChar.Sterne,
                HabIch = neuerChar.HabIch
            };

            context.Charaktere.Add(charZumHinzufuegen);

            await context.SaveChangesAsync();

            return Results.Created($"/charaktere/{charZumHinzufuegen.Id}", charZumHinzufuegen);
        }

        public static async Task<IResult> CharLoeschen(int id, CharContext context)
        {
            var charakterZumLoeschen = context.Charaktere.FirstOrDefault(x => x.Id == id);

            if (charakterZumLoeschen == null)
                return Results.NotFound("Kein Charakter mit dieser Id");

            context.Charaktere.Remove(charakterZumLoeschen);

            await context.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> CharAendern(int id, CharDto charGeupdated, CharContext context)
        {
            var charakterZumAendern = context.Charaktere.FirstOrDefault(x => x.Id == id);

            if (charakterZumAendern == null)
                return Results.NotFound(charakterZumAendern);

            charakterZumAendern.Name = charGeupdated.Name;
            charakterZumAendern.Element = charGeupdated.Element;
            charakterZumAendern.Waffentyp = charGeupdated.Waffentyp;
            charakterZumAendern.Sterne = charGeupdated.Sterne;
            charakterZumAendern.HabIch = charGeupdated.HabIch;

            await context.SaveChangesAsync();

            return Results.Ok(charakterZumAendern);
        }
    }
}
