using System.Collections.Generic;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public interface IRepositorioRol
    {
        IList<Rol> ObtenerTodos();
        Rol? ObtenerPorId(int id);
    }
}