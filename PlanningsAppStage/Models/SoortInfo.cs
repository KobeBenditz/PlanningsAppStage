
namespace PlanningsAppStage.Models
{
    // Stelt één soort (Kamp / Event) met uren voor
    public class SoortInfo
    {
        public string Soort { get; set; }     // Kamp of Event
        public string StartUur { get; set; }  // bv. 10:00
        public string EindUur { get; set; }   // bv. 17:00
    }
}
