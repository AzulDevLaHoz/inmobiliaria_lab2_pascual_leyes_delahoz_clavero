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

        public IActionResult Modificar(int id)
        {
            ViewBag.Propietarios = repoPropietario.ObtenerLista();
            ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
            var entidad = repositorio.ObtenerPorId(id);
            return View(entidad);
        }

        [HttpPost]
        public IActionResult Modificar(int id, Inmueble entidad)
        {
            try
            {
                entidad.Id = id;
                repositorio.Modificar(entidad);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Propietarios = repoPropietario.ObtenerLista(1, 50);
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            repositorio.Baja(id);
            return RedirectToAction(nameof(Index));
        }

    }

}