using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioImagen : RepositorioBase
    {
        public RepositorioImagen(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Imagen imagen)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO imagen (imagen, idInmueble) 
                               VALUES (@imagen, @idInmueble);
                               SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@imagen", imagen.ImagenString);
                    cmd.Parameters.AddWithValue("@idInmueble", imagen.IdInmueble);
                    conn.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                    imagen.IdImagen = res;
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"DELETE FROM imagen WHERE idimagen = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }

        public IList<Imagen> ObtenerPorInmueble(int idInmueble)
        {
            IList<Imagen> lista = new List<Imagen>();
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT idimagen, imagen, idInmueble FROM imagen WHERE idInmueble = @idInmueble;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idInmueble", idInmueble);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Imagen
                            {
                                IdImagen = reader.GetInt32("idimagen"),
                                ImagenString = reader.GetString("imagen"),
                                IdInmueble = reader.GetInt32("idInmueble")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Imagen? ObtenerPorId(int id)
        {
            Imagen? entidad = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT idimagen, imagen, idInmueble FROM imagen WHERE idimagen = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entidad = new Imagen
                            {
                                IdImagen = reader.GetInt32("idimagen"),
                                ImagenString = reader.GetString("imagen"),
                                IdInmueble = reader.GetInt32("idInmueble")
                            };
                        }
                    }
                }
            }
            return entidad;
        }
    }
}