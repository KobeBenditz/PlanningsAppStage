
using Microsoft.AspNetCore.Mvc;
using PlanningsAppStage.Models;
using System.Diagnostics;

namespace PlanningsAppStage.Controllers
{
    public class HomeController : Controller
    {
        // ===============================
        // TIJDELIJKE DATA (zonder databank)
        // Deze lijst blijft bestaan zolang de app draait
        // ===============================
        private static List<Workshop> _workshops = new List<Workshop>
        {

            new Workshop
            {
                WorkshopCode = "123ABC",
                // Altijd een toekomstige datum → tests blijven geldig
                Datum = DateTime.Today.AddDays(3),

                // Vast maar leesbaar
                Uren = "09:00 - 15:00",

                Locatie = "De Stip Linden",

                // Duidelijk dat dit testdata is
                Titel = "TEST – Circus & Technologie",

                Lesgever = "TEST – Nog toe te wijzen"
            }

        };

        // ===============================
        // TESTDATA – WORKSHOP DEFINITIES
        // ===============================
        private static List<WorkshopInfo> _workshopInfos = new()
        {
            new WorkshopInfo
            {
                Titel = "TEST – Wonderwerken",
                Soort = "Kamp",
                AantalDagen = 3,
                AantalBeschikbaar = 4,
                VerkorteVersie = "1-2"
            },
            new WorkshopInfo
            {
                Titel = "TEST – Lego Robot Reeks",
                Soort = "Event",
                AantalDagen = 5,
                AantalBeschikbaar = 2,
                VerkorteVersie = "1"
            }
        };

        // ===============================
        // TIJDELIJKE DATA - LOCATIES
        // ===============================
        private static List<Locatie> _locaties = new List<Locatie>
        {
            new Locatie
            {
                Naam = "De Stip Linden",
                Straat = "Martelarenplaats",
                Nummer = "1",
                Gemeente = "Linden",
                Postcode = "3210",
                Soorten = new List<SoortInfo>
                {
                    new SoortInfo
                    {
                        Soort = "Kamp",
                        StartUur = "09:00",
                        EindUur = "15:00"
                    },
                    new SoortInfo
                    {
                        Soort = "Event",
                        StartUur = "10:00",
                        EindUur = "16:00"
                    }
                }
                
            }
        };


        // ===============================
        // HOME
        // ===============================
        public IActionResult Index()
        {
            return View();
        }

        // ===============================
        // OVERZICHT PLANNING - GET
        // Toont formulier + tabel
        // ===============================
        [HttpGet]
        public IActionResult OverzichtPlanning()
        {
            // Workshop DEFINITIES (Info workshops)
            ViewBag.WorkshopInfos = _workshopInfos;

            // Locaties
            ViewBag.Locaties = _locaties;

            // Geplande workshops (tabel) - gesorteerd op datum

            return View(
                _workshops
                    .OrderBy(w => w.Datum)
                    .ToList()
            );

        }

        // ===============================
        // OVERZICHT PLANNING - POST
        // Beperkt toevoegen op basis van
        // AantalBeschikbaar (per InfoWorkshop)
        // ===============================
        [HttpPost]
        public IActionResult OverzichtPlanning(
            string workshopTitel,
            string locatieNaam,
            string WorkshopCode,
            List<string> Datum,
            List<string> Lesgever
        )
        {
            // ===============================
            // Basisvalidatie
            // ===============================
            if (Datum == null || Datum.Count == 0)
                return RedirectToAction("OverzichtPlanning");

            // ===============================
            // 1️⃣ WorkshopInfo ophalen
            // ===============================
            var workshopInfo = _workshopInfos
                .FirstOrDefault(w => w.Titel == workshopTitel);

            if (workshopInfo == null)
                return RedirectToAction("OverzichtPlanning");

            // ===============================
            // 2️⃣ Aantal keer al toegevoegd
            // (per unieke workshop-groep)
            // ===============================
            int reedsToegevoegd = _workshops
                .Where(w => w.Titel == workshopTitel)
                .Select(w => w.WorkshopGroepId)
                .Distinct()
                .Count();

            // ===============================
            // 3️⃣ Limiet overschreden?
            // ===============================
            if (reedsToegevoegd >= workshopInfo.AantalBeschikbaar)
            {
                TempData["Foutmelding"] =
                    $"Deze workshop kan maximaal {workshopInfo.AantalBeschikbaar} keer ingepland worden.";

                return RedirectToAction("OverzichtPlanning");
            }

            // ===============================
            // 4️⃣ Locatie + uren bepalen
            // ===============================
            var locatie = _locaties.FirstOrDefault(l => l.Naam == locatieNaam);
            if (locatie == null) return RedirectToAction("OverzichtPlanning");

            var soortInfo = locatie.Soorten
                .FirstOrDefault(s => s.Soort == workshopInfo.Soort);

            if (soortInfo == null) return RedirectToAction("OverzichtPlanning");

            string correcteUren = $"{soortInfo.StartUur} - {soortInfo.EindUur}";

            // ===============================
            // 5️⃣ Nieuwe workshop-groep ID
            // ===============================
            Guid groepId = Guid.NewGuid();

            // ===============================
            // 6️⃣ Workshops toevoegen
            // ===============================
            for (int i = 0; i < Datum.Count; i++)
            {
                _workshops.Add(new Workshop
                {
                    WorkshopGroepId = groepId,
                    WorkshopCode = WorkshopCode,
                    Datum = DateTime.Parse(Datum[i]),
                    Uren = correcteUren,
                    Locatie = locatieNaam,
                    Titel = workshopTitel,
                    Lesgever = Lesgever[i]
                });
            }

            return RedirectToAction("OverzichtPlanning");
        }


        // ===============================
        // INFO MEDEWERKERS
        // ===============================
        public IActionResult InfoMedewerkers()
        {
            return View();
        }

        // ===============================
        // INFO WORKSHOPS - GET
        // Toont formulier + tabel
        // ===============================
        [HttpGet]
        public IActionResult InfoWorkshops()
        {
            return View(_workshopInfos);
        }


        // ===============================
        // INFO WORKSHOPS - POST
        // Ontvangt formulier en voegt toe
        // ===============================
        [HttpPost]
        public IActionResult InfoWorkshops(WorkshopInfo nieuweWorkshop)
        {
            _workshopInfos.Add(nieuweWorkshop);

            // voorkomt dubbele toevoeging bij refresh

            return RedirectToAction("InfoWorkshops");
        }



        // ===============================
        // INFO LOCATIES - GET
        // Toont formulier + tabel
        // ===============================
        [HttpGet]
        public IActionResult InfoLocaties()
        {
            return View(_locaties);
        }


        // ===============================
        // INFO LOCATIES - POST
        // Ontvangt formulier en voegt toe
        // ===============================
        [HttpPost]
        public IActionResult InfoLocaties(Locatie nieuweLocatie)
        {
            _locaties.Add(nieuweLocatie);

            // voorkomt dubbele toevoeging bij refresh
            return RedirectToAction("InfoLocaties");
        }


        // ===============================
        // ERROR / PRIVACY (standaard)
        // ===============================
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
