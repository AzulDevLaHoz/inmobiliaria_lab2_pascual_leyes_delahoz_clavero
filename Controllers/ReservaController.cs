using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;
        private readonly IRepositorioInquilino repoInquilino;
        private readonly RepositorioInmueble repoInmueble;
        private readonly IConfiguration configuration;
        private readonly ILogger<ReservaController> logger;

        public ReservaController(IRepositorioReserva repositorio, IRepositorioInquilino repoInquilino, RepositorioInmueble repoInmueble, IConfiguration configuration, ILogger<ReservaController> logger)
        {
            this.repositorio = repositorio;
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
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
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View();
        }
        [HttpPost]
        public IActionResult Alta(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                repositorio.Alta(reserva);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View(reserva);
        }


        public ActionResult Modificar(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);

            if (entidad == null)
            {
                return NotFound();
            }
            ViewBag.Inquilino = repoInquilino.ObtenerPorId(entidad.IdInquilino);
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Modificar(int id, Reserva entidad)
        {
            var r = repositorio.ObtenerPorId(id);
            if (r == null) return NotFound();
            if (ModelState.IsValid)
            {
                r.FechaEntrada = entidad.FechaEntrada;
                r.FechaSalida = entidad.FechaSalida;
                r.IdInmueble = entidad.IdInmueble;
                r.IdInquilino = entidad.IdInquilino;
                r.FechaMulta = entidad.FechaMulta;
                repositorio.Modificar(r);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();

            return View(entidad);

        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            repositorio.Baja(id);
            return RedirectToAction(nameof(Index));
        }

    }
}