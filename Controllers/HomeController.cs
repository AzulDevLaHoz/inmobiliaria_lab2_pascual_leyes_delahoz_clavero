using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Authorization;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers;
 [Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

   

public IActionResult AccesoDenegado()
{
    TempData["Error"] = "No tienes permisos suficientes (requieres rol de Administrador) para realizar esa acción.";

    return RedirectToAction("Index", "Home");
}
}
