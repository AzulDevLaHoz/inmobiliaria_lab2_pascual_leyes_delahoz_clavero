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

        public IActionResult Index(int pagina = 1)
        {
            int tamPagina = 10;

            var inquilinos = repositorio.ObtenerLista(pagNro: pagina, tamPagina: tamPagina);


            int totalRegistros = repositorio.ObtenerCantidad();

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamPagina);

            return View(inquilinos);
        }

        [HttpGet]
        public IActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alta(Inquilino inquilino)
        {
            var existente = repositorio.ObtenerPorDni(inquilino.Dni);

            if (existente != null && existente.Estado)
            {
                TempData["Error"] = "Ya existe un inquilino activo con ese DNI.";
                return View(inquilino);
            }

            if (existente != null && !existente.Estado)
            {
                ViewBag.DniDuplicadoJson = System.Text.Json.JsonSerializer.Serialize(new
                {
                    id = existente.IdInquilino,
                    nombre = $"{existente.Nombre} {existente.Apellido}"
                });
                return View(inquilino);
            }

            if (ModelState.IsValid)
            {
                repositorio.Alta(inquilino);
                TempData["Mensaje"] = "El inquilino fue registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }

        public ActionResult Modificar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            return View(entidad);
        }

        public IActionResult Detalles(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            if (entidad == null) return NotFound();
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

        public IActionResult Buscar(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(new List<object>());
            }

            var inquilinos = repositorio.BuscarPorTexto(q)
                .Select(p => new
                {
                    id = p.IdInquilino,
                    texto = $"{p.Nombre} {p.Apellido} DNI: {p.Dni}"
                });

            return Json(inquilinos);
        }

        [HttpPost]
        public IActionResult Reactivar(int id)
        {
            repositorio.Reactivar(id);
            TempData["Mensaje"] = "El inquilino fue reactivado correctamente.";
            return RedirectToAction(nameof(Detalles), new { id });
        }

    }

}

