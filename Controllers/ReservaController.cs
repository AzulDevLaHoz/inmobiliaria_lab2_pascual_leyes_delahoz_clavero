using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorioReserva;
        private readonly IRepositorioInquilino repositorioInquilino;

        private readonly RepositorioInmueble repositorioInmueble;
        private readonly IConfiguration configuration;
        private readonly ILogger<ReservaController> logger;
<<<<<<< HEAD
        public ReservaController(IRepositorioReserva repositorio, IConfiguration configuration, ILogger<ReservaController> logger)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioInquilino = repositorioInquilino;
            this.repositorioInmueble = repositorioInmueble;
            this.configuration = configuration;
=======
        private readonly IRepositorioInquilino repoInquilino;
        private readonly RepositorioInmueble repoInmueble;

        public ReservaController(IRepositorioReserva repositorio, IRepositorioInquilino repoInquilino, RepositorioInmueble repoInmueble, IConfiguration configuration, ILogger<ReservaController> logger)
        {
            this.repositorio = repositorio;
>>>>>>> Clavero
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
            this.configuration = configuration;
            this.logger = logger;
        }


        public IActionResult Index()
        {
            var lista = repositorioReserva.ObtenerLista();
            return View(lista);
        }
        public IActionResult Alta()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Alta(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                repositorioReserva.Alta(reserva);
                return RedirectToAction(nameof(Index));
=======
                repositorio.Alta(reserva);
>>>>>>> Clavero
            }
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View(reserva);
        }

    }
}