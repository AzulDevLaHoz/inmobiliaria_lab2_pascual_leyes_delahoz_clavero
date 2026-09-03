using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public interface IRepositorioReserva : IRepositorio<Reserva>
    {
        public IList<Reserva> ObtenerListaActivos(int paginaNro = 1, int tamPagina = 10);
        public bool ActualizarSalidaAnticipada(Reserva r);
    }
}