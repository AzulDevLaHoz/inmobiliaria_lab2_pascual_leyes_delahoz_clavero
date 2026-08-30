using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{

    public class RepositorioInquilino : RepositorioBase, IRepositorioInquilino
    {
        public RepositorioInquilino(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Inquilino> ObtenerTodos()
        {
            var lista = new List<Inquilino>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM inquilino", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Inquilino
                    {
                        IdInquilino = Convert.ToInt32(reader["idInquilino"]),
                        Nombre = reader["nombre"].ToString() ?? "",
                        Apellido = reader["apellido"].ToString() ?? "",
                        Dni = reader["dni"].ToString() ?? "",
                        Telefono = reader["telefono"].ToString() ?? "",
                        Email = reader["email"].ToString() ?? ""
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
                string sql = "UPDATE inquilino SET estado = @es WHERE IdInquilino = @id";
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



        public int Alta(Inquilino i)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO inquilino (nombre, apellido, dni, telefono, email, estado) VALUES (@n, @a, @d, @t, @e, @es)";
                using (var cmd = new MySqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("@n", i.Nombre);
                    cmd.Parameters.AddWithValue("@a", i.Apellido);
                    cmd.Parameters.AddWithValue("@d", i.Dni);
                    cmd.Parameters.AddWithValue("@t", i.Telefono);
                    cmd.Parameters.AddWithValue("@e", i.Email);
                    cmd.Parameters.AddWithValue("@es", true);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    i.IdInquilino = Convert.ToInt32(cmd.LastInsertedId);
                    return i.IdInquilino;
                }
            }
        }

        public int Modificar(Inquilino i)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @$"UPDATE inquilino 
					SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email
					WHERE idInquilino = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@id", i.IdInquilino);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        virtual public Inquilino ObtenerPorId(int id)
        {
            Inquilino? i = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
					idInquilino, nombre, apellido, dni, telefono, email
					FROM inquilino
					WHERE idInquilino=@id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            IdInquilino = reader.GetInt32("IdInquilino"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.GetString("Telefono"),
                            Email = reader.GetString("Email"),
                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }

        public IList<Inquilino> ObtenerLista(int pagNro = 1, int tamPagina = 10)
        {
            IList<Inquilino> res = new List<Inquilino>();
            int offset = (pagNro - 1) * tamPagina;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
                SELECT IdInquilino, Nombre, Apellido, Telefono, Dni, Email, Estado
                FROM inquilino
                WHERE estado = 1
                ORDER BY IdInquilino
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
                            Inquilino p = new Inquilino
                            {
                                IdInquilino = Convert.ToInt32(reader[nameof(Inquilino.IdInquilino)]),
                                Nombre = reader[nameof(Inquilino.Nombre)]?.ToString() ?? "",
                                Apellido = reader[nameof(Inquilino.Apellido)]?.ToString() ?? "",
                                Dni = reader.GetString(nameof(Inquilino.Dni)),
                                Telefono = reader[nameof(Inquilino.Telefono)]?.ToString() ?? "",
                                Email = reader[nameof(Inquilino.Email)]?.ToString() ?? "",
                                Estado = Convert.ToBoolean(reader[nameof(Inquilino.Estado)]),
                            };
                            res.Add(p);
                        }
                    }
                }
            }
            return res;
        }

        public int ObtenerCantidad => throw new NotImplementedException();
    }
}