namespace PlanningsAppStage.Models
{
    // Deze klasse stelt één workshop (info) voor
    // Elke eigenschap komt overeen met een kolom in de tabel
    public class WorkshopInfo
    {
        public string Code { get; set; }
        public string Titel { get; set; }
        public int AantalDagen { get; set; }
        public int AantalBeschikbaar { get; set; }
    }
}