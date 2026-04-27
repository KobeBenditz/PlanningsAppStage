namespace PlanningsAppStage.Models
{
    // Deze klasse stelt één locatie voor
    // Enkel de velden die we in de tabel nodig hebben
    public class Locatie
    {
        // Naam van de locatie
        public string Naam { get; set; }

        // Volledig adres van de locatie
        public string Adres { get; set; }
    }
}