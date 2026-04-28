namespace PlanningsAppStage.Models
{
    public class WorkshopInfo
    {
        public string Code { get; set; }
        public string Titel { get; set; }
        public int AantalDagen { get; set; }
        public int AantalBeschikbaar { get; set; }

        // Extra info
        public string VerkorteVersie { get; set; }

        // NIEUW: soort workshop
        // voorlopig: "Kamp" of "Event"
        public string Soort { get; set; }
    }
}