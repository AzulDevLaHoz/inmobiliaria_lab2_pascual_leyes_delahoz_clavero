using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly RepositorioInmueble repositorio;
        private readonly IRepositorioPropietario repoPropietario;
        private readonly RepositorioTipoInmueble repoTipoInmueble;

        public InmuebleController(RepositorioInmueble repositorio, IRepositorioPropietario repoPropietrio, RepositorioTipoInmueble repoTipoInmueble)
        {
            this.repositorio = repositorio;
            this.repoPropietario = repoPropietrio;
            this.repoTipoInmueble = repoTipoInmueble;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();
            return View(lista);
        }

        public IActionResult Alta()
        {
            ViewBag.Propietarios = repoPropietario.ObtenerLista();
            ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
            return View();
        }

        [HttpPost]
        public IActionResult Alta(Inmueble inmueble)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorio.Alta(inmueble);
                    TempData["Id"] = inmueble.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Propietarios = repoPropietario.ObtenerLista();
                    return View(inmueble);
                }
            }
            catch (Exception e)
            {
                ViewBag.Propietarios = repoPropietario.ObtenerLista();
                ViewBag.Error = e.Message;
                ViewBag.StackTrate = e.StackTrace;
                return View(inmueble);
            }
        }
    }

}