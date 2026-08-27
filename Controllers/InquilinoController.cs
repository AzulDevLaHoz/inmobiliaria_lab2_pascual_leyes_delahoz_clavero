using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly RepositorioInquilino repositorio;

        public InquilinoController(RepositorioInquilino repositorio)
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
        public IActionResult Alta(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                repositorio.Alta(inquilino);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }

        public ActionResult Modificar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Modificar(int id, Inquilino entidad)
        {
            var i = repositorio.ObtenerPorId(id);
            if (i == null) return NotFound();

            i.Nombre = entidad.Nombre;
            i.Apellido = entidad.Apellido;
            i.Dni = entidad.Dni;
            i.Email = entidad.Email;
            i.Telefono = entidad.Telefono;
            repositorio.Modificar(i);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            repositorio.Baja(entidad);
            return RedirectToAction(nameof(Index));
        }

    }

}

