using System.Collections.Generic;

namespace PlanningsAppStage.Models
{
    public class Locatie
    {
        // === FORMULIER ===
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        public string Gemeente { get; set; }
        public string Postcode { get; set; }

        // === NIEUW ===
        // Lijst van soorten (Kamp/Event + uren)
        public List<SoortInfo> Soorten { get; set; } = new List<SoortInfo>();

        // === BEREKEND ===
        public string Adres
        {
            get { return $"{Straat} {Nummer}, {Postcode} {Gemeente}"; }
        }
    }
}