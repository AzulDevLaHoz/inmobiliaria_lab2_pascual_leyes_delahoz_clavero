using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioInmueble : RepositorioBase
    {
        public RepositorioInmueble(IConfiguration configuration) : base(configuration)
        {

        }

        public int Alta(Inmueble inmueble)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO inmueble
                 (Direccion,capacidad,latitud,longitud,
                porcentajeReserva,imagenPortada,montoDia,estado,idPropietario,idTipoInmueble) 
                 VALUES (@direc,@capacidad,@lat,@lon,@porcentaje,@imagen,@montoDia,@est,@idProp,@idTipo)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@direc", inmueble.Direccion);
                    cmd.Parameters.AddWithValue("@capacidad", inmueble.Capacidad);
                    cmd.Parameters.AddWithValue("@lat", inmueble.Latitud);
                    cmd.Parameters.AddWithValue("@lon", inmueble.Longitud);
                    cmd.Parameters.AddWithValue("@porcentaje", inmueble.porcentajeReserva);
                    cmd.Parameters.AddWithValue("@imagen", inmueble.ImagenPortada);
                    cmd.Parameters.AddWithValue("@montoDia", inmueble.montoDia);
                    cmd.Parameters.AddWithValue("@est", true);
                    cmd.Parameters.AddWithValue("@idProp", inmueble.PropietarioId);
                    cmd.Parameters.AddWithValue("@idTipo", inmueble.TipoInmuebleId);
                    conn.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                    inmueble.Id = res;
                    conn.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @$"UPDATE inmueble  SET estado=false  WHERE Id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }

        public int Modificar(Inmueble inmueble)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @$"UPDATE inmueble  SET Direccion=@direc,
                capacidad=@capacidad,
                latitud=@lat,
                longitud=@lon,
                porcentajeReserva=@porcentaje,
                imagenPortada=@imagen,
                montoDia=@monto,
                estado=@estado,
                idPropietario=@idprop,
                idTipoInmueble=@idtipo
                WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@direc", inmueble.Direccion);
                    cmd.Parameters.AddWithValue("@capacidad", inmueble.Capacidad);
                    cmd.Parameters.AddWithValue("@lat", inmueble.Latitud);
                    cmd.Parameters.AddWithValue("@lon", inmueble.Longitud);
                    cmd.Parameters.AddWithValue("@porcentaje", inmueble.porcentajeReserva);
                    cmd.Parameters.AddWithValue("@imagen", inmueble.StringPortada);
                    cmd.Parameters.AddWithValue("@monto", inmueble.montoDia);
                    cmd.Parameters.AddWithValue("@estado", inmueble.Estado);
                    cmd.Parameters.AddWithValue("@idprop", inmueble.PropietarioId);
                    cmd.Parameters.AddWithValue("@idtipo", inmueble.TipoInmuebleId);
                    cmd.Parameters.AddWithValue("@id", inmueble.Id);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            return res;
        }


        public IList<Inmueble> ObtenerLista(int pagNro = 1, int tamPagina = 10)
        {
            IList<Inmueble> res = new List<Inmueble>();


            int offset = (pagNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {

                string sql = @"
            SELECT 
                i.IdInmueble, i.Direccion, i.capacidad, i.latitud, i.longitud,
                i.porcentajeReserva, i.ImagenPortada, i.montoDia, i.estado,
                i.idPropietario, i.idTipoInmueble,
                p.Nombre AS PropietarioNombre, p.Apellido AS PropietarioApellido
            FROM inmueble i
            INNER JOIN propietario p ON i.idPropietario = p.IdPropietario
            ORDER BY i.IdInmueble
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
                            Inmueble p = new Inmueble
                            {
                                Id = Convert.ToInt32(reader[nameof(Inmueble.Id)]),
                                Direccion = reader[nameof(Inmueble.Direccion)]?.ToString() ?? "",
                                Capacidad = reader.GetInt32(nameof(Inmueble.Capacidad)),
                                Latitud = reader.GetDecimal(nameof(Inmueble.Latitud)),
                                Longitud = reader.GetDecimal(nameof(Inmueble.Longitud)),
                                porcentajeReserva = reader.GetDecimal(nameof(Inmueble.porcentajeReserva)),
                                StringPortada = reader[nameof(Inmueble.ImagenPortada)]?.ToString() ?? "",
                                montoDia = reader.GetDecimal(nameof(Inmueble.montoDia)),
                                Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                                PropietarioId = reader.GetInt32(nameof(Inmueble.PropietarioId)),
                                TipoInmuebleId = reader.GetInt32(nameof(Inmueble.TipoInmuebleId)),
                                Duenio = new Propietario
                                {
                                    IdPropietario = reader.GetInt32(nameof(Inmueble.PropietarioId)),
                                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                }


                            };
                            res.Add(p);
                        }
                    }
                }
            }
            return res;
        }
    }

}