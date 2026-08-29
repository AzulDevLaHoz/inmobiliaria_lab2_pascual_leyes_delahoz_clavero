using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;

public class RepositorioTipoInmueble : RepositorioBase
{
    public RepositorioTipoInmueble(IConfiguration configuration) : base(configuration) { }

    public List<TipoInmueble> ObtenerTodos()
    {
        var lista = new List<TipoInmueble>();

        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM tipoInmueble", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new TipoInmueble
                {
                    IdTipoInmueble = Convert.ToInt32(reader["idTipoInmueble"]),
                    Nombre = reader["nombre"].ToString() ?? "",
                });
            }
        }
        return lista;
    }
}