using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioUsuario : RepositorioBase
    {

        public RepositorioUsuario(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Usuario u)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                // Se corrigieron las comas dobles en VALUES
                string sql = "INSERT INTO usuario (Nombre, Apellido, Email, Clave, Avatar, idRol, estado) VALUES (@n, @a, @e, @c, @av, @r, @es);";

                using (var command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@n", u.Nombre);
                    command.Parameters.AddWithValue("@a", u.Apellido);
                    command.Parameters.AddWithValue("@e", u.Email);
                    command.Parameters.AddWithValue("@c", u.Clave);
                    command.Parameters.AddWithValue("@av", DBNull.Value);
                    command.Parameters.AddWithValue("@r", u.IdRol);
                    command.Parameters.AddWithValue("@es", true);

                    conn.Open();
                    command.ExecuteNonQuery();
                    u.Id = Convert.ToInt32(command.LastInsertedId);
                    res = u.Id;
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE Usuario SET Estado = false WHERE IdUsuario = @id";
                using (var command = new MySqlCommand(sql, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    res = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }


        public Usuario? ObtenerPorEmail(string email)
        {
            Usuario? u = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
                SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.Clave, u.Avatar, u.IdRol, u.Estado, r.Nombre AS NombreRol
                FROM usuario u
                INNER JOIN rol r ON u.IdRol = r.IdRol
                WHERE u.Email = @e AND u.Estado = 1;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@e", email);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            u = new Usuario
                            {
                                Id = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString() ?? "",
                                Apellido = reader["Apellido"].ToString() ?? "",
                                IdRol = Convert.ToInt32(reader["IdRol"]),
                                Email = reader["Email"].ToString() ?? "",
                                Clave = reader["Clave"].ToString() ?? "",
                                Avatar = reader["Avatar"] != DBNull.Value ? reader["Avatar"].ToString() : null,
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(reader["IdRol"]),
                                    Nombre = reader["NombreRol"].ToString() ?? ""
                                }
                            };
                        }
                    }
                }
            }
            return u;
        }

        public int Modificar(Usuario u)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE usuario SET 
                        Nombre = @n, 
                        Apellido = @a, 
                        Email = @e, 
                        Avatar = @av, 
                        idRol = @r
                        WHERE IdUsuario = @id;";

                using (var command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@n", u.Nombre);
                    command.Parameters.AddWithValue("@a", u.Apellido);
                    command.Parameters.AddWithValue("@e", u.Email);
                    command.Parameters.AddWithValue("@av", u.Avatar);
                    command.Parameters.AddWithValue("@r", u.IdRol);
                    command.Parameters.AddWithValue("@id", u.Id);

                    conn.Open();
                    res = command.ExecuteNonQuery();
                }
            }
            return res;
        }

        public IList<Usuario> ObtenerLista(int pagNro = 1, int tamPagina = 10)
        {
            IList<Usuario> res = new List<Usuario>();

            int offset = (pagNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {

                string sql = @"SELECT 
                                    u.IdUsuario, 
                                    u.Nombre, 
                                    u.Apellido, 
                                    u.Email, 
                                    u.Clave, 
                                    u.Avatar, 
                                    u.IdRol, 
                                    u.Estado, 
                                    r.Nombre AS NombreRol
                                FROM Usuario u
                                INNER JOIN rol r ON u.IdRol = r.IdRol
                                WHERE u.estado = 1
                                ORDER BY u.IdUsuario
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
                            Usuario p = new Usuario
                            {
                                Id = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString() ?? "",
                                Apellido = reader["Apellido"].ToString() ?? "",
                                IdRol = Convert.ToInt32(reader["IdRol"]),
                                Email = reader["Email"].ToString() ?? "",
                                Clave = reader["Clave"].ToString() ?? "",
                                Avatar = reader["Avatar"] != DBNull.Value ? reader["Avatar"].ToString() : null,
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(reader["IdRol"]),
                                    Nombre = reader["NombreRol"].ToString() ?? ""
                                }
                            };
                            res.Add(p);
                        }
                    }
                }
            }
            return res;
        }

        public Usuario? ObtenerTodos()
        {
            Usuario? u = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
                SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.Clave, u.Avatar, u.IdRol, u.Estado, r.Nombre AS NombreRol
                FROM usuario u
                INNER JOIN rol r ON u.IdRol = r.IdRol";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            u = new Usuario
                            {
                                Id = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString() ?? "",
                                Apellido = reader["Apellido"].ToString() ?? "",
                                IdRol = Convert.ToInt32(reader["IdRol"]),
                                Email = reader["Email"].ToString() ?? "",
                                Clave = reader["Clave"].ToString() ?? "",
                                Avatar = reader["Avatar"] != DBNull.Value ? reader["Avatar"].ToString() : null,
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(reader["IdRol"]),
                                    Nombre = reader["NombreRol"].ToString() ?? ""
                                }
                            };
                        }
                    }
                }
            }
            return u;
        }

        public int ObtenerCantidadUsuarios()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT COUNT(IdUsuario) FROM Usuario WHERE estado = 1";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        virtual public Usuario? ObtenerPorId(int id)
        {
            Usuario? u = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdUsuario, Nombre, Apellido, Email, IdRol
                            FROM Usuario
                            WHERE IdUsuario = @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        u = new Usuario
                        {
                            Id = reader.GetInt32("IdUsuario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Email = reader.GetString("Email"),
                            IdRol = reader.GetInt32("IdRol")
                        };
                    }
                    connection.Close();
                }
            }
            return u;
        }

    }

}