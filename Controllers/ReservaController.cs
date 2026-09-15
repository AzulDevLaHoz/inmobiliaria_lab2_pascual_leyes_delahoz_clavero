using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly IRepositorioReserva repositorio;
        private readonly IRepositorioInquilino repoInquilino;
        private readonly RepositorioInmueble repoInmueble;
        private readonly IRepositorioPago repoPago;
        private readonly IConfiguration configuration;
        private readonly ILogger<ReservaController> logger;

        public ReservaController(IRepositorioReserva repositorio, IRepositorioInquilino repoInquilino, RepositorioInmueble repoInmueble, IRepositorioPago repoPago, IConfiguration configuration, ILogger<ReservaController> logger)
        {
            this.repositorio = repositorio;
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
            this.repoPago = repoPago;
            this.configuration = configuration;
            this.logger = logger;
        }


        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();

            var reservasConSenia = new HashSet<int>();
            var reservasConPago = new HashSet<int>();
            var reservasConMultaPagada = new HashSet<int>();
            foreach (var r in lista)
            {
                if (repoPago.ExistePagoSenia(r.IdReserva))
                {
                    reservasConSenia.Add(r.IdReserva);
                }
                if (repoPago.ExistePagoCompletado(r.IdReserva))
                {
                    reservasConPago.Add(r.IdReserva);
                }
                if (r.FechaMulta != null && repoPago.ExistePagoMulta(r.IdReserva))
                {
                    reservasConMultaPagada.Add(r.IdReserva);
                }
            }
            ViewBag.ReservasConSenia = reservasConSenia;
            ViewBag.ReservasConPago = reservasConPago;
            ViewBag.ReservasConMultaPagada = reservasConMultaPagada;

            return View(lista);
        }
        public IActionResult Alta()
        {
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();
            return View();
        }

        [HttpGet]
        public IActionResult Alta(int? idInmueble, DateTime? fechaEntrada, DateTime? fechaSalida)
        {
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();

            var reserva = new Reserva();
            if (idInmueble.HasValue && idInmueble > 0)
            {
                reserva.IdInmueble = idInmueble.Value;
            }

            if (fechaEntrada.HasValue && fechaEntrada.Value != DateTime.MinValue)
            {
                reserva.FechaEntrada = fechaEntrada.Value;
            }

            if (fechaSalida.HasValue && fechaSalida.Value != DateTime.MinValue)
            {
                reserva.FechaSalida = fechaSalida.Value;
            }

            return View(reserva);
        }
        [HttpPost]
        public IActionResult Alta(Reserva reserva)
        {
            if (reserva.IdInquilino <= 0)
            {
                ModelState.AddModelError("IdInquilino", "Debe seleccionar un inquilino válido.");
            }
            if (reserva.IdInmueble <= 0)
            {
                ModelState.AddModelError("IdInmueble", "Debe seleccionar un inmueble válido.");
            }
            if (reserva.FechaEntrada.Date < DateTime.Today)
            {
                ModelState.AddModelError("FechaEntrada", "La fecha de entrada no puede ser anterior a hoy.");
            }
            if (reserva.FechaSalida.Date < reserva.FechaEntrada.Date)
            {
                ModelState.AddModelError("FechaSalida", "La fecha de Salida no puede ser anterior a la de Ingreso.");
            }

            if (reserva.IdInmueble > 0 && reserva.FechaEntrada.Date <= reserva.FechaSalida.Date
            && repositorio.ExisteSolapamiento(reserva.IdInmueble, reserva.FechaEntrada, reserva.FechaSalida))
            {
                ModelState.AddModelError("IdInmueble", "El inmueble ya tiene una reserva activa en esas fechas.");
            }
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

        public IActionResult Detalles(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            if (entidad == null) return NotFound();
            ViewBag.Inquilino = repoInquilino.ObtenerPorId(entidad.IdInquilino);
            ViewBag.Inmueble = repoInmueble.ObtenerPorId(entidad.IdInmueble);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Modificar(int id, Reserva entidad)
        {
            var r = repositorio.ObtenerPorId(id);
            if (r == null) return NotFound();
            if (entidad.IdInmueble > 0 && entidad.FechaEntrada.Date <= entidad.FechaSalida.Date
            && repositorio.ExisteSolapamiento(entidad.IdInmueble, entidad.FechaEntrada, entidad.FechaSalida, id))
            {
                ModelState.AddModelError("IdInmueble", "El inmueble ya tiene una reserva activa en esas fechas.");
            }
            if (ModelState.IsValid)

            {
                r.FechaEntrada = entidad.FechaEntrada;
                r.FechaSalida = entidad.FechaSalida;
                r.IdInmueble = entidad.IdInmueble;
                r.IdInquilino = entidad.IdInquilino;
                repositorio.Modificar(r);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Inquilinos = repoInquilino.ObtenerLista();
            ViewBag.Inmuebles = repoInmueble.ObtenerLista();

            return View(entidad);

        }

        [HttpPost]
        [Authorize(Roles ="Administrador")]
        public ActionResult Eliminar(int id)
        {
            repositorio.Baja(id);
            return RedirectToAction(nameof(Index));
        }

        //--------------------------
        public IActionResult SalidaAnticipada(int id)
        {
            var reserva = repositorio.ObtenerPorId(id);
            if (reserva == null) return NotFound();

            var inmueble = repoInmueble.ObtenerPorId(reserva.IdInmueble);
            ViewBag.MontoDiario = inmueble.montoDia;
            ViewBag.MontoSeniaPagada = repoPago.ObtenerImportePorConcepto(id, "Seña") ?? 0m;

            return View(reserva);
        }

        [HttpPost]
        [Authorize]
        public IActionResult SalidaAnticipada(int idReserva, DateTime fechaRetiro, string metodoDePago)
        {
            var reserva = repositorio.ObtenerPorId(idReserva);
            if (reserva == null) return NotFound();

            var inmueble = repoInmueble.ObtenerPorId(reserva.IdInmueble);
            decimal montoDiario = inmueble.montoDia;

            int diasTotales = (reserva.FechaSalida - reserva.FechaEntrada).Days;
            int diasQuedado = (fechaRetiro - reserva.FechaEntrada).Days;

            decimal montoSeniaPagada = repoPago.ObtenerImportePorConcepto(idReserva, "Seña") ?? 0m;
            decimal montoHospedajePendiente = Math.Max(0m, (diasQuedado * montoDiario) - montoSeniaPagada);

            decimal multa;

            if (fechaRetiro < reserva.FechaSalida)
            {
                int diasRestantes = (reserva.FechaSalida - fechaRetiro).Days;
                decimal montoRestante = diasRestantes * montoDiario;
                // "mitad incluida": si se cumplió exactamente la mitad de los días, sigue siendo 50%
                decimal porcentaje = diasQuedado <= diasTotales / 2.0 ? 0.50m : 0.25m;
                multa = montoRestante * porcentaje;
            }
            else if (fechaRetiro > reserva.FechaSalida)
            {
                return BadRequest("Regla de exceso de días aún no definida.");
            }
            else
            {
                multa = 0m;
            }

            // Se cobra ahora mismo el hospedaje correspondiente a los días efectivamente consumidos,
            // y -si corresponde- la multa, en la misma operación.
            if (montoHospedajePendiente > 0 || multa > 0)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out int idUsuario))
                {
                    return BadRequest("No se pudo identificar al usuario autenticado.");
                }

                if (montoHospedajePendiente > 0)
                {
                    var pagoHospedaje = new Pago
                    {
                        Concepto = "Completado",
                        Importe = montoHospedajePendiente,
                        FechaPago = DateTime.Today,
                        MetodoDePago = metodoDePago,
                        IdReserva = idReserva,
                        IdUsuarioCreador = idUsuario
                    };
                    repoPago.Alta(pagoHospedaje);
                }

                if (multa > 0)
                {
                    var pagoMulta = new Pago
                    {
                        Concepto = "Multa",
                        Importe = multa,
                        FechaPago = DateTime.Today,
                        MetodoDePago = metodoDePago,
                        IdReserva = idReserva,
                        IdUsuarioCreador = idUsuario
                    };
                    repoPago.Alta(pagoMulta);
                }
            }

            // FechaMulta/Multa quedan igual para tener el registro histórico de la salida anticipada,
            // aunque ahora la multa (si corresponde) ya se cobró arriba. "Pagar Multa" sigue existiendo
            // como respaldo para reservas que quedaron a mitad de camino con el flujo anterior.
            reserva.FechaMulta = fechaRetiro;
            reserva.Multa = multa;
            reserva.Estado = false; // libera las fechas del inmueble para nuevas reservas

            repositorio.ActualizarSalidaAnticipada(reserva);

            return RedirectToAction("Index");
        }

        [HttpGet("Reserva/HistorialJson/{idInmueble}")]
        public IActionResult HistorialJson(int idInmueble)
        {
            var inmueble = repoInmueble.ObtenerPorId(idInmueble);
            if (inmueble == null) return NotFound();

            var reservas = repositorio.ObtenerPorInmueble(idInmueble);

            var resultado = reservas.Select(r => new
            {
                id = r.IdReserva,
                fechaEntrada = r.FechaEntrada.ToString("dd/MM/yyyy"),
                fechaSalida = r.FechaSalida.ToString("dd/MM/yyyy"),
                estado = r.Estado,
                inquilino = r.Inquilino != null ? $"{r.Inquilino.Nombre} {r.Inquilino.Apellido}" : "-",
                multa = r.Multa,
                montoTotal = (r.FechaSalida - r.FechaEntrada).Days * inmueble.montoDia
            });

            return Json(resultado);
        }

    }
}