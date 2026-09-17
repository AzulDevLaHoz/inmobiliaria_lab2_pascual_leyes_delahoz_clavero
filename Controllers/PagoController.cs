using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class PagoController : Controller
    {
        private readonly IRepositorioPago repositorio;
        private readonly RepositorioInmueble repoInmueble;
        private readonly IRepositorioReserva repoReserva;
        private readonly RepositorioUsuario repoUsuario;
        private readonly ILogger<PagoController> logger;

        public PagoController(IRepositorioPago repositorio, RepositorioInmueble repoInmueble, IRepositorioReserva repoReserva, RepositorioUsuario repoUsuario, ILogger<PagoController> logger)
        {
            this.repositorio = repositorio;
            this.repoReserva = repoReserva;
            this.repoInmueble = repoInmueble;
            this.repoUsuario = repoUsuario;
            this.logger = logger;
        }

        public IActionResult Index(string estado = "Todos", int pagina = 1)
        {
            int tamPagina = 10;
            var lista = repositorio.ObtenerReservasPorEstado(estado, null, pagina, tamPagina);
            int totalRegistros = repositorio.ObtenerCantidadReservasPorEstado(estado, null);

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamPagina);
            ViewBag.EstadoSeleccionado = estado;

            return View(lista);
        }

        public IActionResult DetalleReserva(int idReserva)
        {
            var reserva = repoReserva.ObtenerPorId(idReserva);
            if (reserva == null) return NotFound();

            var pagos = repositorio.ObtenerPorReserva(idReserva);
            foreach (var p in pagos)
            {
                p.UsuarioCreador = repoUsuario.ObtenerPorId(p.IdUsuarioCreador);
                if (p.IdUsuarioAnulador.HasValue)
                {
                    p.UsuarioAnulador = repoUsuario.ObtenerPorId(p.IdUsuarioAnulador.Value);
                }
            }
            ViewBag.Reserva = reserva;
            return View(pagos);
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

            var inmueble = repoInmueble.ObtenerPorId(reserva.IdInmueble);
            int dias = (reserva.FechaSalida - reserva.FechaEntrada).Days;
            decimal montoTotal = dias * inmueble.montoDia;

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
            else if (tipo == "Seña")
            {
                pago.Concepto = "Seña";
                pago.Importe = montoTotal * (inmueble.porcentajeReserva / 100m);
            }
            else
            {
                decimal montoSeniaPagada = repositorio.ObtenerImportePorConcepto(idReserva.Value, "Seña") ?? 0m;
                pago.Concepto = "Completado";
                pago.Importe = Math.Max(0m, montoTotal - montoSeniaPagada);
            }

            ViewBag.Tipo = tipo;
            return View(pago);
        }

        [HttpPost]
        [Authorize] 
        public IActionResult Alta(Pago pago)
        {
            if (pago.IdReserva <= 0)
            {
                ModelState.AddModelError("IdReserva", "El pago debe estar asociado a una reserva válida.");
            }

            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int idUsuario))
                {
                    pago.IdUsuarioCreador = idUsuario;
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo identificar al usuario autenticado.");
                    return View(pago);
                }

                repositorio.Alta(pago);
                TempData["Mensaje"] = "Pago registrado correctamente.";
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
                TempData["Mensaje"] = "Pago modificado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        [HttpPost]
        [Authorize(Roles ="Administrador")]
        public IActionResult Eliminar(int id)
        {
            var pago = repositorio.ObtenerPorId(id);
            repositorio.Baja(id);
            if (pago != null)
            {
                if (pago.Concepto == "Completado")
                {
                    // Si este pago venia de una Salida Anticipada, esto la revierte:
                    // limpia FechaMulta/Multa y vuelve a poner la reserva activa.
                    repoReserva.ReactivarReserva(pago.IdReserva);
                }
                TempData["Mensaje"] = "Pago dado de baja correctamente.";
                return RedirectToAction(nameof(DetalleReserva), new { idReserva = pago.IdReserva });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AnularPago(int id)
        {
            var pago = repositorio.ObtenerPorId(id);
            // Modificar por el usuario que esta logueado cuando hagamos loguin
            int idUsuarioAnulador = 1;
            repositorio.AnularPago(id, idUsuarioAnulador);
            if (pago != null)
            {
                if (pago.Concepto == "Completado")
                {
                    repoReserva.ReactivarReserva(pago.IdReserva);
                }
                TempData["Mensaje"] = "Pago anulado correctamente.";
                return RedirectToAction(nameof(DetalleReserva), new { idReserva = pago.IdReserva });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Buscar(int? idInmueble, string estado = "Todos", int pagina = 1)
        {
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            ViewBag.EstadoSeleccionado = estado;

            if (idInmueble.HasValue && idInmueble > 0)
            {
                ViewBag.IdInmuebleSeleccionado = idInmueble.Value;

                int tamPagina = 10;
                var lista = repositorio.ObtenerReservasPorEstado(estado, idInmueble.Value, pagina, tamPagina);
                int totalRegistros = repositorio.ObtenerCantidadReservasPorEstado(estado, idInmueble.Value);

                ViewBag.PaginaActual = pagina;
                ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamPagina);

                return View(lista);
            }

            return View(new List<Reserva>());
        }

    }
}