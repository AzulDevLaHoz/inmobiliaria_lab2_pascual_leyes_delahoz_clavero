using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioRol : RepositorioBase, IRepositorioRol
    {
        public RepositorioRol(IConfiguration configuration) : base(configuration)
        {
        }

        public IList<Rol> ObtenerTodos()
        {
            var res = new List<Rol>();
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT IdRol, Nombre FROM rol ORDER BY IdRol;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(new Rol
                            {
                                Id = Convert.ToInt32(reader["IdRol"]),
                                Nombre = reader["Nombre"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            return res;
        }

        public Rol? ObtenerPorId(int id)
        {
            Rol? r = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT IdRol, Nombre FROM rol WHERE IdRol = @id;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            r = new Rol
                            {
                                Id = Convert.ToInt32(reader["IdRol"]),
                                Nombre = reader["Nombre"].ToString() ?? ""
                            };
                        }
                    }
                }
            }
            return r;
        }
    }
}