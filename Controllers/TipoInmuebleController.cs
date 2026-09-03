using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class TipoInmuebleController : Controller
    {
        private readonly RepositorioTipoInmueble repositorio;

        public TipoInmuebleController(RepositorioTipoInmueble repositorio)
        {
            this.repositorio = repositorio;
        }
        public IActionResult Index()
        {
            var lista = repositorio.ObtenerTodos();
            return View(lista);
        }

        public IActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alta(TipoInmueble t)
        {
            if (ModelState.IsValid)
            {
                repositorio.Alta(t);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(t);
        }

        public ActionResult Modificar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Modificar(int id, TipoInmueble entidad)
        {
            var i = repositorio.ObtenerPorId(id);
            if (i == null) return NotFound();
            if (ModelState.IsValid)
            {
                i.Nombre = entidad.Nombre;
                repositorio.Modificar(i);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            repositorio.Baja(id);
            TempData["Mensaje"] = "Se dio de baja correctamente.";
            return RedirectToAction(nameof(Index));
        }

    }
}