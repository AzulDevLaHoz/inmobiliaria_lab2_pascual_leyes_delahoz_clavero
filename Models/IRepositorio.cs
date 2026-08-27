using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public interface IRepositorio<T>
    {
        int Alta(T p);
        int Baja(int id);
        int Modificar(T p);

        IList<T> ObtenerLista(int paginaNro = 1, int tamPagina = 10);
        int ObtenerCantidad { get; }
        T? ObtenerPorId(int id);

    }
}