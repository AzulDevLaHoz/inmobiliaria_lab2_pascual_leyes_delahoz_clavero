using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;
        private readonly IConfiguration configuration;
        private readonly ILogger<ReservaController> logger;
        public ReservaController(IRepositorioReserva repositorio, IConfiguration configuration, ILogger<ReservaController> logger)
        {
            this.repositorio = repositorio;
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
        public IActionResult Alta(Reserva reserva)
        {
            if(ModelState.IsValid)
            {
                repositorio.Alta(reserva);
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

    }
}