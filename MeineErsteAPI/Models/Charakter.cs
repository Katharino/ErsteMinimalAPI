namespace MeineErsteAPI.Models
{
    public class Charakter
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Element { get; set; } = "";
        public string Waffentyp { get; set; } = "";
        public int Sterne { get; set; }
        public bool HabIch { get; set; }

    }

    //Da wir beim erstellen eines neuen Charakters keine Id angeben wollen ersetllen wir ein 
    //Dto (= Data Tranfer Model). Hier können wir einen record verwenden. Dto's sind ähnlich wie ViewModels.
    //Dto => Datenübertragung (z.B. zwischen Methoden)
    //ViewModel => Datenanzeige an den Endbenutzer
    record CharDto(string Name, string Element, string Waffentyp, int Sterne, bool HabIch);
 }
