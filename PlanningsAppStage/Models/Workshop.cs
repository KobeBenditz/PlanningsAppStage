namespace PlanningsAppStage.Models
{
    // Deze klasse stelt één workshopdag voor
    // Meerdere dagen kunnen tot één workshop-groep behoren
    public class Workshop
    {
        public Guid WorkshopGroepId { get; set; }   // ✅ NIEUW

        public DateTime Datum { get; set; }
        public string Uren { get; set; }
        public string Locatie { get; set; }
        public string Titel { get; set; }
        public string Lesgever { get; set; }
    }
}