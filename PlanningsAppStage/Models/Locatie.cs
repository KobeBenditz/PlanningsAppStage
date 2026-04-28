namespace PlanningsAppStage.Models
{
    public class Locatie
    {
        // === WORDT INGEVULD VIA FORMULIER ===
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        public string Gemeente { get; set; }
        public string Postcode { get; set; }

        // === WORDT NIET INGEVULD, MAAR BEREKEND ===
        // Dit wordt gebruikt in de tabel
        public string Adres
        {
            get
            {
                return $"{Straat} {Nummer}, {Postcode} {Gemeente}";
            }
        }
    }
}