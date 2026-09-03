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
                (direccion,
                capacidad,
                latitud,
                longitud,
                porcentajeReserva,
                imagenPortada,
                montoDia,
                estado,
                idPropietario,
                idTipoInmueble)
                VALUES (@direc, @capacidad, @lat, @lon, @porcentaje, @imagen, @montoDia, @est, @idProp, @idTipo)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@direc", inmueble.Direccion);
                    cmd.Parameters.AddWithValue("@capacidad", inmueble.Capacidad);
                    cmd.Parameters.AddWithValue("@lat", inmueble.Latitud);
                    cmd.Parameters.AddWithValue("@lon", inmueble.Longitud);
                    cmd.Parameters.AddWithValue("@porcentaje", inmueble.porcentajeReserva);
                    cmd.Parameters.AddWithValue("@imagen", (object)inmueble.StringPortada ?? DBNull.Value);
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
                string sql = @$"UPDATE inmueble  SET estado=false  WHERE idInmueble = @id";
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
                string sql = @$"UPDATE inmueble  SET 
                direccion=@direc,
                capacidad=@capacidad,
                latitud=@lat,
                longitud=@lon,
                porcentajeReserva=@porcentaje,
                imagenPortada=@imagen,
                montoDia=@monto,
                idPropietario=@idprop,
                idTipoInmueble=@idtipo
                WHERE idInmueble=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@direc", inmueble.Direccion);
                    cmd.Parameters.AddWithValue("@capacidad", inmueble.Capacidad);
                    cmd.Parameters.AddWithValue("@lat", inmueble.Latitud);
                    cmd.Parameters.AddWithValue("@lon", inmueble.Longitud);
                    cmd.Parameters.AddWithValue("@porcentaje", inmueble.porcentajeReserva);
                    cmd.Parameters.AddWithValue("@imagen", inmueble.StringPortada);
                    cmd.Parameters.AddWithValue("@monto", inmueble.montoDia);
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
                        i.idInmueble AS Id, 
                        i.Direccion, 
                        i.capacidad AS Capacidad, 
                        i.latitud AS Latitud, 
                        i.longitud AS Longitud,
                        i.porcentajeReserva, 
                        i.ImagenPortada, 
                        i.montoDia, 
                        i.estado AS Estado,
                        i.idPropietario AS PropietarioId, 
                        i.idTipoInmueble AS TipoInmuebleId,
                        p.Nombre, 
                        p.Apellido
                    FROM inmueble i
                    INNER JOIN propietario p ON i.idPropietario = p.IdPropietario
                    WHERE i.estado = 1
                    ORDER BY i.idInmueble
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

        virtual public Inmueble ObtenerPorId(int id)
        {
            Inmueble? i = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT 
                    idInmueble,
					direccion,
                    capacidad,
                    latitud,
                    longitud,
                    porcentajeReserva,
                    imagenPortada,
                    montoDia,
                    estado,
                    idPropietario,
                    idTipoInmueble
					FROM inmueble
					WHERE idInmueble=@id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inmueble
                        {
                            Id = reader.GetInt32("idInmueble"),
                            Direccion = reader.GetString("direccion"),
                            Capacidad = reader.GetInt32("capacidad"),
                            Latitud = reader.GetInt32("latitud"),
                            Longitud = reader.GetInt32("longitud"),
                            porcentajeReserva = reader.GetDecimal("porcentajeReserva"),
                            //StringPortada = reader.GetString("")
                            montoDia = reader.GetDecimal("montoDia"),
                            Estado = reader.GetBoolean("estado"),
                            PropietarioId = reader.GetInt32("idPropietario"),
                            TipoInmuebleId = reader.GetInt32("idTipoInmueble"),
                        };
                    }
                    connection.Close();
                }
            }
            //if(i == null) return => deberiamos transformar estos metodos en int para validarlos en el controller por los posibles nulos.
            return i;
        }

        public IList<Inmueble> ObtenerPorPropietario(int idPropietario)
{
    IList<Inmueble> lista = new List<Inmueble>();
    using (var conn = new MySqlConnection(connectionString))
    {
        string sql = @"SELECT idInmueble, direccion, capacidad, montoDia, porcentajeReserva, imagenPortada, estado 
                       FROM inmueble 
                       WHERE idPropietario = @idProp AND estado = 1;";

        using (var cmd = new MySqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@idProp", idPropietario);
            conn.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Inmueble
                    {
                        Id = reader.GetInt32("idInmueble"),
                        Direccion = reader.GetString("direccion"),
                        Capacidad = reader.GetInt32("capacidad"),
                        montoDia = reader.GetDecimal("montoDia"),
                        porcentajeReserva = reader.GetDecimal("porcentajeReserva"),
                        StringPortada = reader.IsDBNull(reader.GetOrdinal("imagenPortada")) ? "" : reader.GetString("imagenPortada")
                    });
                }
            }
        }
    }
    return lista;
}

    }

}