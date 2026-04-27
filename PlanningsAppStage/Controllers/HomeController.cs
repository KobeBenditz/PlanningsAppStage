
using Microsoft.AspNetCore.Mvc;
using PlanningsAppStage.Models;
using System.Diagnostics;

namespace PlanningsAppStage.Controllers
{
    public class HomeController : Controller
    {
        // ===============================test
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
            return View(_workshops);
        }

        // ===============================
        // OVERZICHT PLANNING - POST
        // Ontvangt formulier en voegt toe
        // ===============================
        [HttpPost]
        public IActionResult OverzichtPlanning(Workshop nieuweWorkshop)
        {
            _workshops.Add(nieuweWorkshop);

            // Redirect zodat refresh geen dubbele toevoeging doet
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
        // INFO WORKSHOPS (hardcoded)
        // ===============================
        public IActionResult InfoWorkshops()
        {
            var workshops = new List<WorkshopInfo>
            {
                new WorkshopInfo { Code = "2627NS013", Titel = "Wonderwerken", AantalDagen = 3, AantalBeschikbaar = 4 },
                new WorkshopInfo { Code = "2627NS010", Titel = "Lego Robot Reeks: Robo-Art", AantalDagen = 5, AantalBeschikbaar = 2 },
                new WorkshopInfo { Code = "2627NS001", Titel = "Circus Techtacular", AantalDagen = 4, AantalBeschikbaar = 3 },
                new WorkshopInfo { Code = "2627NS007", Titel = "STEM-Experimenten", AantalDagen = 3, AantalBeschikbaar = 2 }
            };

            return View(workshops);
        }

        // ===============================
        // INFO LOCATIES (hardcoded)
        // ===============================
        public IActionResult InfoLocaties()
        {
            var locaties = new List<Locatie>
            {
                new Locatie
                {
                    Naam = "De Stip Linden",
                    Adres = "Martelarenplaats 1, 3210 Linden"
                }
            };

            return View(locaties);
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
