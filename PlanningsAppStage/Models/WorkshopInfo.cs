namespace PlanningsAppStage.Models
{
    public class WorkshopInfo
    {
        public string Titel { get; set; }
        public int AantalDagen { get; set; }
        public int AantalBeschikbaar { get; set; }

        // Extra info
        public List<int> VerkorteDagen { get; set; } = new();

        // NIEUW: soort workshop
        // voorlopig: "Kamp" of "Event"
        public string Soort { get; set; }
    }
}