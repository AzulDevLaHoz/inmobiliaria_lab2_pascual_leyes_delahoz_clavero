using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly IRepositorioPropietario repositorio;
        private readonly RepositorioInmueble repoInmueble;
        private readonly IConfiguration configuration;
        private readonly ILogger<PropietarioController> logger;

        public PropietarioController(IRepositorioPropietario repo, RepositorioInmueble repoInmueble, IConfiguration configuration, ILogger<PropietarioController> logger)
        {
            this.repositorio = repo;
            this.repoInmueble = repoInmueble;
            this.configuration = configuration;
            this.logger = logger;
        }
        public IActionResult Index(int pagina = 1)
        {
            int tamPagina = 10;

            var propietarios = repositorio.ObtenerLista(pagNro: pagina, tamPagina: tamPagina);

            int totalRegistros = repositorio.ObtenerCantidad();

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamPagina);

            return View(propietarios);
        }
        public IActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alta(Propietario propietario)
        {
            var existente = repositorio.ObtenerPorDni(propietario.Dni);

            if (existente != null && existente.Estado)
            {
                TempData["Error"] = "Ya existe un propietario activo con ese DNI.";
                return View(propietario);
            }

            if (existente != null && !existente.Estado)
            {
                ViewBag.DniDuplicadoJson = System.Text.Json.JsonSerializer.Serialize(new
                {
                    id = existente.IdPropietario,
                    nombre = $"{existente.Nombre} {existente.Apellido}"
                });
                return View(propietario);
            }

            if (ModelState.IsValid)
            {
                repositorio.Alta(propietario);
                TempData["Mensaje"] = "El propietario fue registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }

        public IActionResult Detalles(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            if (entidad == null) return NotFound();

            var inmuebles = repoInmueble.ObtenerPorPropietario(id);
            ViewBag.InmueblesJson = System.Text.Json.JsonSerializer.Serialize(inmuebles);
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

        [HttpPost]
        public IActionResult Reactivar(int id)
        {
            repositorio.Reactivar(id);
            TempData["Mensaje"] = "El propietario fue reactivado correctamente.";
            return RedirectToAction(nameof(Detalles), new { id });
        }

    }


}