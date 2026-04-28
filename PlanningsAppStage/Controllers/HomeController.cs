
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
                Datum = "27/04/2026",
                Uren = "9:00-15:00",
                Locatie = "De Stip Linden",
                Titel = "Circus Techtacular",
                Lesgever = "Ik wil deze les geven"
            }
        };

        // ===============================
        // TIJDELIJKE DATA - WORKSHOP INFO
        // ===============================
        private static List<WorkshopInfo> _workshopInfos = new List<WorkshopInfo>
        {
            new WorkshopInfo
            {
                Code = "2627NS013",
                Titel = "Wonderwerken",
                AantalDagen = 3,
                AantalBeschikbaar = 4
            },
            new WorkshopInfo
            {
                Code = "2627NS010",
                Titel = "Lego Robot Reeks: Robo-Art",
                AantalDagen = 5,
                AantalBeschikbaar = 2
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
                Postcode = "3210"
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

            // Geplande workshops (tabel)
            return View(_workshops);
        }

        // ===============================
        // OVERZICHT PLANNING - POST
        // Ontvangt formulier met meerdere dagen
        // Maakt per dag één workshop-item aan
        // ===============================
        [HttpPost]
        public IActionResult OverzichtPlanning(
            string workshopTitel,
            string locatieNaam,
            string uren,
            List<string> Datum,
            List<string> Lesgever
        )
        {
            // Voor elke ingevulde dag maken we één nieuwe workshop aan
            for (int i = 0; i < Datum.Count; i++)
            {
                _workshops.Add(new Workshop
                {
                    Datum = Datum[i],
                    Uren = uren,
                    Locatie = locatieNaam,
                    Titel = workshopTitel,
                    Lesgever = Lesgever[i]
                });
            }

            // Redirect voorkomt dubbele toevoeging bij refresh
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
