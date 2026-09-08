using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class UsuarioController : Controller
    {
        

   [HttpGet]
    public IActionResult Login()
   {
    return View("Login");
   }
    }

}