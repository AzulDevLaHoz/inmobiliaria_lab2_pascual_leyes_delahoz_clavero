using System;
using System.Data;
using Microsoft.AspNetCore.Routing.Internal;
using MySql.Data.MySqlClient;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{

    public class RepositorioPropietario : RepositorioBase, IRepositorioPropietario
    {
        public RepositorioPropietario(IConfiguration configuration) : base(configuration)
        {

        }

        public int Alta(Propietario p)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO propietario (Nombre, Apellido, Telefono, Dni, Email,Estado) VALUES (@n, @a, @t, @d, @e,@es)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", p.Nombre);
                    cmd.Parameters.AddWithValue("@a", p.Apellido);
                    cmd.Parameters.AddWithValue("@t", p.Telefono);
                    cmd.Parameters.AddWithValue("@d", p.Dni);
                    cmd.Parameters.AddWithValue("@e", p.Email);
                    cmd.Parameters.AddWithValue("@es", true);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    p.IdPropietario = Convert.ToInt32(cmd.LastInsertedId);
                    return p.IdPropietario;
                }
            }
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE propietario SET estado = @es WHERE IdPropietario = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@es", false);

                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }

            return res;
        }

        public int Modificar(Propietario p)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE propietario 
                    SET Nombre = @n, 
                    Apellido = @a, 
                    Dni = @d,
                    Telefono = @t, 
                    Email = @e 
                    WHERE IdPropietario = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", p.IdPropietario);
                    cmd.Parameters.AddWithValue("@n", p.Nombre);
                    cmd.Parameters.AddWithValue("@a", p.Apellido);
                    cmd.Parameters.AddWithValue("@d", p.Dni);
                    cmd.Parameters.AddWithValue("@t", p.Telefono);
                    cmd.Parameters.AddWithValue("@e", p.Email);

                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }

        public List<Propietario> ObtenerTodos()
        {
            var lista = new List<Propietario>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM propietario";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Propietario
                        {
                            IdPropietario = Convert.ToInt32(reader["idPropietario"]),
                            Nombre = reader["nombre"].ToString() ?? "",
                            Apellido = reader["apellido"].ToString() ?? "",
                            Telefono = reader["telefono"].ToString() ?? "",
                            Dni = reader["dni"].ToString() ?? "",
                            Email = reader["email"].ToString() ?? ""
                        });
                    }
                }
                return lista;
            }
        }

        public IList<Propietario> ObtenerLista(int pagNro = 1, int tamPagina = 10)
        {
            IList<Propietario> res = new List<Propietario>();


            int offset = (pagNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {

                string sql = @"
            SELECT IdPropietario, Nombre, Apellido, Telefono,Dni, Email
            FROM propietario
            WHERE estado = 1
            ORDER BY IdPropietario
            LIMIT @tamPagina OFFSET @offset;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tamPagina", tamPagina);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Propietario p = new Propietario
                            {
                                IdPropietario = Convert.ToInt32(reader[nameof(Propietario.IdPropietario)]),
                                Nombre = reader[nameof(Propietario.Nombre)]?.ToString() ?? "",
                                Apellido = reader[nameof(Propietario.Apellido)]?.ToString() ?? "",
                                Dni = reader[nameof(Propietario.Dni)]?.ToString() ?? "",
                                Telefono = reader[nameof(Propietario.Telefono)]?.ToString() ?? "",
                                Email = reader[nameof(Propietario.Email)]?.ToString() ?? ""
                            };
                            res.Add(p);
                        }
                    }
                }
            }
            return res;
        }

        public Propietario? ObtenerPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IList<Propietario> BuscarPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public int ObtenerCantidad => throw new NotImplementedException();

        virtual public Propietario ObtenerPorId(int id)
        {
            Propietario? p = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
					idPropietario, nombre, apellido, dni, telefono, email,estado
					FROM propietario
					WHERE idPropietario=@id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Dni = reader.GetString("dni"),
                            Telefono = reader.GetString("telefono"),
                            Email = reader.GetString("email"),
                            Estado = reader.GetBoolean("estado")
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public IList<Propietario> BuscarPorTexto(string q)
        {
            IList<Propietario> res = new List<Propietario>();
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
            SELECT IdPropietario, Nombre, Apellido, Dni 
            FROM propietario 
            WHERE Nombre LIKE @q OR Apellido LIKE @q OR Dni LIKE @q
            LIMIT 10;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@q", $"%{q}%");
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(new Propietario
                            {
                                IdPropietario = reader.GetInt32("IdPropietario"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido"),
                                Dni = reader.GetString("Dni")
                            });
                        }
                    }
                }
            }
            return res;
        }

    }
}