using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class PagoController : Controller
    {
        private readonly IRepositorioPago repositorio;
        private readonly RepositorioInmueble repoInmueble;
        private readonly IRepositorioReserva repoReserva;
        private readonly ILogger<PagoController> logger;

        public PagoController(IRepositorioPago repositorio, RepositorioInmueble repoInmueble, IRepositorioReserva repoReserva, ILogger<PagoController> logger)
        {
            this.repositorio = repositorio;
            this.repoReserva = repoReserva;
            this.repoInmueble = repoInmueble;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Alta(int? idReserva, string tipo = "Completo")
        {
            if (!idReserva.HasValue || idReserva <= 0)
            {
                return RedirectToAction("Index", "Reserva");
            }

            var reserva = repoReserva.ObtenerPorId(idReserva.Value);
            if (reserva == null) return NotFound();

            var pago = new Pago { IdReserva = idReserva.Value };

            if (tipo == "Multa")
            {
                if (reserva.Multa == null)
                {
                    return BadRequest("Esta reserva todavia no tiene una multa calculada.");
                }
                pago.Concepto = "Multa";
                pago.Importe = reserva.Multa.Value;
            }
            else
            {
                var inmueble = repoInmueble.ObtenerPorId(reserva.IdInmueble);
                int dias = (reserva.FechaSalida - reserva.FechaEntrada).Days;
                pago.Concepto = "Completado";
                pago.Importe = dias * inmueble.montoDia;
            }

            ViewBag.Tipo = tipo;
            return View(pago);
        }

        [HttpPost]
        public IActionResult Alta(Pago pago)
        {
            if (pago.IdReserva <= 0)
            {
                ModelState.AddModelError("IdReserva", "El pago debe estar asociado a una reserva valida.");
            }
            if (ModelState.IsValid)
            {
                // Modificar por el usuario que esta logueado cuando hagamos loguin
                pago.IdUsuarioCreador = 1;
                repositorio.Alta(pago);
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        public IActionResult Modificar(int id)
        {
            var pago = repositorio.ObtenerPorId(id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        [HttpPost]
        public IActionResult Modificar(int id, Pago pago)
        {
            var p = repositorio.ObtenerPorId(id);
            if (p == null) return NotFound();

            if (ModelState.IsValid)
            {
                p.Concepto = pago.Concepto;
                repositorio.Modificar(p);
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            repositorio.Baja(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AnularPago(int id)
        {
            // Modificar por el usuario que esta logueado cuando hagamos loguin
            int idUsuarioAnulador = 1;
            repositorio.AnularPago(id, idUsuarioAnulador);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalles(int id)
        {
            var pago = repositorio.ObtenerPorId(id);
            if (pago == null) return NotFound();
            ViewBag.MostrarAuditoria = User.IsInRole("administrador");

            return View(pago);
        }

        [HttpGet]
        public IActionResult Buscar(int? idInmueble)
        {
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();

            if (idInmueble.HasValue && idInmueble > 0)
            {
                ViewBag.IdInmuebleSeleccionado = idInmueble.Value;
                var pagos = repositorio.ObtenerPorInmueble(idInmueble.Value);
                return View(pagos);
            }

            return View(new List<Pago>());
        }

        public IActionResult ObtenerPorId(int id)
        {
            var pago = repositorio.ObtenerPorId(id);
            if (pago == null) return NotFound();

            ViewBag.Reserva = repoReserva.ObtenerPorId(pago.IdReserva);
            ViewBag.MostrarAuditoria = User.IsInRole("administrador");

            return View(pago);
        }
    }
}