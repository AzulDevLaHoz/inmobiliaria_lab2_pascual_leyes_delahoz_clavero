using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly IRepositorioPropietario repositorio;
        private readonly IConfiguration config;
        private readonly ILogger<PropietarioController> logger;

        public PropietarioController(IRepositorioPropietario repo, IConfiguration config, ILogger<PropietarioController> logger)
        {
            this.repositorio = repo;
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
        public IActionResult Alta(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                repositorio.Alta(propietario);
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }
        public ActionResult Modificar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Modificar(int id, Propietario entidad)
        {
            var p = repositorio.ObtenerPorId(id);
            if (p == null) return NotFound();

            p.Nombre = entidad.Nombre;
            p.Apellido = entidad.Apellido;
            p.Dni = entidad.Dni;
            p.Email = entidad.Email;
            p.Telefono = entidad.Telefono;
            repositorio.Modificar(p);
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