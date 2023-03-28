using MeineErsteAPI.Models;
using System.Collections.Concurrent;

namespace MeineErsteAPI
{
    public class CharakterHandlers
    {
        public static IEnumerable<Charakter> AlleCharsZurueckgeben(CharContext context) => context.Charaktere;

        public static IResult EinCharZurueckgeben(int id, CharContext context)
        {
            var charakter = context.Charaktere.FirstOrDefault(x => x.Id == id);
            if (charakter != null)
                return Results.Ok(charakter);
            return Results.NotFound();
        }

        public static IResult CharHinzufuegen(CharDto neuerChar, CharContext context)
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

            context.SaveChanges();

            return Results.Created($"/charaktere/{charZumHinzufuegen.Id}", charZumHinzufuegen);
        }

        public static IResult CharLoeschen(int id, CharContext context)
        {
            var charakterZumLoeschen = context.Charaktere.FirstOrDefault(x => x.Id == id);

            if (charakterZumLoeschen == null)
                return Results.NotFound("Kein Charakter mit dieser Id");

            context.Charaktere.Remove(charakterZumLoeschen);

            context.SaveChanges();

            return Results.NoContent();
        }

        public static IResult CharAendern(int id, CharDto charGeupdated, CharContext context)
        {
            var charakterZumAendern = context.Charaktere.FirstOrDefault(x => x.Id == id);

            if (charakterZumAendern == null)
                return Results.NotFound(charakterZumAendern);

            charakterZumAendern.Name = charGeupdated.Name;
            charakterZumAendern.Element = charGeupdated.Element;
            charakterZumAendern.Waffentyp = charGeupdated.Waffentyp;
            charakterZumAendern.Sterne = charGeupdated.Sterne;
            charakterZumAendern.HabIch = charGeupdated.HabIch;

            context.SaveChanges();

            return Results.Ok(charakterZumAendern);
        }
    }
}
