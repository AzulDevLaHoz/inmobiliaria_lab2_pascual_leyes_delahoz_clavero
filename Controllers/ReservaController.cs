using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;
        private readonly IConfiguration configuration;
        private readonly ILogger<ReservaController> logger;
        private readonly IRepositorioInquilino repoInquilino;
        private readonly RepositorioInmueble repoInmueble;

        public ReservaController(IRepositorioReserva repositorio, IConfiguration configuration, ILogger<ReservaController> logger, IRepositorioInquilino repoInquilino, RepositorioInmueble repoInmueble)
        {
            this.repositorio = repositorio;
            this.configuration = configuration;
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
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
            if(ModelState.IsValid)
            {
                repositorio.Alta(reserva);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View(reserva);
        }

    }
}