using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public interface IRepositorioReserva : IRepositorio<Reserva>
    {
        public IList<Reserva> ObtenerListaActivos(int paginaNro = 1, int tamPagina = 10);
        public IList<Reserva> ObtenerLista(int paginaNro = 1, int tamPagina = 10);
        public bool ActualizarSalidaAnticipada(Reserva r);
        bool ExisteSolapamiento(int idInmueble, DateTime fechaEntrada, DateTime fechaSalida, int? idReservaExcluir = null);

        IList<Reserva> ObtenerPorInmueble(int idInmueble);
        bool ReactivarReserva(int idReserva);

        IList<Reserva> ObtenerListaPorEstado(string estado, int paginaNro = 1, int tamPagina = 10);
        int ObtenerCantidadPorEstado(string estado);

        IList<Reserva> ObtenerListaInactivos(int paginaNro = 1, int tamPagina = 10);
        int ObtenerCantidadInactivos();

    }
}