namespace PlanningsAppStage.Models
{
    // Deze klasse stelt één workshop voor
    // Elke eigenschap komt overeen met één kolom in de tabel
    public class Workshop
    {
        // Datum van de workshop
        public DateTime Datum { get; set; }

        // Uren waarin de workshop doorgaat
        public string Uren { get; set; }

        // Locatie van de workshop
        public string Locatie { get; set; }

        // Titel van de workshop
        public string Titel { get; set; }

        // Naam van de lesgever
        public string Lesgever { get; set; }
    }
}
