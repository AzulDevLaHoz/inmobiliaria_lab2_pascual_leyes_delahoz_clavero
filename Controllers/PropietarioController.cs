using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly IRepositorioPropietario repositorio;
        private readonly IConfiguration configuration;
        private readonly ILogger<PropietarioController> logger;

        public PropietarioController(IRepositorioPropietario repo, IConfiguration configuration, ILogger<PropietarioController> logger)
        {
            this.repositorio = repo;
            this.configuration = configuration;
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
                TempData["Mensaje"] = "El propietario fue registrado correctamente.";
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
            if (ModelState.IsValid)
            {
                p.Nombre = entidad.Nombre;
                p.Apellido = entidad.Apellido;
                p.Dni = entidad.Dni;
                p.Email = entidad.Email;
                p.Telefono = entidad.Telefono;
                repositorio.Modificar(p);
                TempData["Mensaje"] = "Datos actualizados correctamente.";
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

        public IActionResult Buscar(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(new List<object>());
            }

            var propietarios = repositorio.BuscarPorTexto(q)
                .Select(p => new
                {
                    id = p.IdPropietario,
                    texto = $"{p.Nombre} {p.Apellido} (DNI: {p.Dni})"
                });

            return Json(propietarios);
        }

    }


}