
using Microsoft.AspNetCore.Mvc;

namespace PlanningsAppStage.Controllers
{
    /*
        Deze controller is verantwoordelijk voor account-gerelateerde pagina's.
        Дляlopig is dit enkel een dummy loginpagina.
    */
    public class AccountController : Controller
    {
        /*
            GET: /Account/Login

            Deze actie toont gewoon de loginpagina.
            Er gebeurt nog geen echte authenticatie.
        */
        public IActionResult Login()
        {
            return View();
        }
    }
}
