using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public interface IRepositorioPago : IRepositorio<Pago>
    {
        int AnularPago(int idPago, int idUsuarioAnulador);
        IList<Pago> ObtenerPorInmueble(int idInmueble, int pagNro = 1, int tamPagina = 5);
        IList<Pago> ObtenerPorReserva(int idReserva);
        IList<Reserva> ObtenerReservasFinalizadas(int cantidad = 10);
        bool ExistePagoCompletado(int idReserva);
        bool ExistePagoMulta(int idReserva);
        bool ExistePagoSenia(int idReserva);
        decimal? ObtenerImportePorConcepto(int idReserva, string concepto);
    }
}