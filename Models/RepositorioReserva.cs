using System;
using System.Data;
using Microsoft.AspNetCore.Routing.Internal;
using MySql.Data.MySqlClient;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioReserva: RepositorioBase, IRepositorioReserva
    {
        public RepositorioReserva(IConfiguration configuration): base(configuration)
        {
        }

        public int ObtenerCantidad => throw new NotImplementedException();

        public int Alta(Reserva p)
        {
            throw new NotImplementedException();
        }

        public int Baja(Reserva p)
        {
            throw new NotImplementedException();
        }

        public int Modificar(Reserva p)
        {
            throw new NotImplementedException();
        }

        public IList<Reserva> ObtenerLista(int paginaNro = 1, int tamPagina = 10)
        {
            throw new NotImplementedException();
        }

        public IList<Reserva> ObtenerListaActivos(int paginaNro = 1, int tamPagina = 10)
        {
            throw new NotImplementedException();
        }

        public Reserva? ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    } 
}