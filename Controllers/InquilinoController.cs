using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly IRepositorioInquilino repositorio;
        private readonly IConfiguration config;
        private readonly ILogger<InquilinoController> logger;

        public InquilinoController(IRepositorioInquilino repositorio, IConfiguration config, ILogger<InquilinoController> logger)
        {
            this.repositorio = repositorio;
            this.config = config;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();
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
            if (ModelState.IsValid)
            {
                i.Nombre = entidad.Nombre;
                i.Apellido = entidad.Apellido;
                i.Dni = entidad.Dni;
                i.Email = entidad.Email;
                i.Telefono = entidad.Telefono;
                repositorio.Modificar(i);
                return RedirectToAction(nameof(Index));
            }
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var i = repositorio.ObtenerPorId(id);
            if (i == null) return NotFound();
            repositorio.Baja(i);
            return RedirectToAction(nameof(Index));
        }

    }

}

