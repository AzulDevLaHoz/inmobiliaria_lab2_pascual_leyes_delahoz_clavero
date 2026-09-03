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

    public int Baja(int id)
    {
        int res = -1;
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM tipoInmueble WHERE idTipoInmueble = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                res = cmd.ExecuteNonQuery();
            }
        }

        return res;
    }

    public int Alta(TipoInmueble i)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = "INSERT INTO tipoInmueble (nombre) VALUES (@n)";
            using (var cmd = new MySqlCommand(sql, conn))
            {

                cmd.Parameters.AddWithValue("@n", i.Nombre);

                conn.Open();
                cmd.ExecuteNonQuery();
                i.IdTipoInmueble = Convert.ToInt32(cmd.LastInsertedId);
                return i.IdTipoInmueble;
            }
        }
    }

    public int Modificar(TipoInmueble i)
    {
        int res = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @$"UPDATE tipoInmueble SET nombre=@nombre WHERE idTipoInmueble = @id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@nombre", i.Nombre);
                command.Parameters.AddWithValue("@id", i.IdTipoInmueble);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    virtual public TipoInmueble ObtenerPorId(int id)
    {
        TipoInmueble? i = null;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT idTipoInmueble, nombre
					FROM tipoInmueble
					WHERE idTipoInmueble=@id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                command.CommandType = CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    i = new TipoInmueble
                    {
                        IdTipoInmueble = reader.GetInt32("IdTipoInmueble"),
                        Nombre = reader.GetString("Nombre")
                    };
                }
                connection.Close();
            }
        }
        return i;
    }

}