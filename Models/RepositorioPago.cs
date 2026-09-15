using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioPago : RepositorioBase, IRepositorioPago
    {
        public RepositorioPago(IConfiguration configuration) : base(configuration) { }

        public int ObtenerCantidad()
        {
            throw new NotImplementedException();
        }

        public int Alta(Pago p)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO pago 
                (concepto, importe, fechaPago, metodoPago, estado, idReserva, idUsuarioCreador) 
                VALUES (@co, @im, @fe, @me, @es, @idRes, @idUsCre)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@co", p.Concepto);
                    cmd.Parameters.AddWithValue("@im", p.Importe);
                    cmd.Parameters.AddWithValue("@fe", p.FechaPago);
                    cmd.Parameters.AddWithValue("@me", p.MetodoDePago);
                    cmd.Parameters.AddWithValue("@es", true);
                    cmd.Parameters.AddWithValue("@idRes", p.IdReserva);
                    cmd.Parameters.AddWithValue("@idUsCre", p.IdUsuarioCreador);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    p.IdPago = Convert.ToInt32(cmd.LastInsertedId);
                    return p.IdPago;
                }
            }
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE pago SET estado = @es WHERE idPago = @id";
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

        public int AnularPago(int idPago, int idUsuarioAnulador)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE pago 
                        SET estado = @es, idUsuarioAnulador = @idUsAnu 
                        WHERE idPago = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPago);
                    cmd.Parameters.AddWithValue("@es", false);
                    cmd.Parameters.AddWithValue("@idUsAnu", idUsuarioAnulador);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }

        public int Modificar(Pago p)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE pago SET concepto = @co WHERE idPago = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", p.IdPago);
                    cmd.Parameters.AddWithValue("@co", p.Concepto);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }

        public IList<Pago> ObtenerLista(int pagNro = 1, int tamPagina = 10)
        {
            IList<Pago> res = new List<Pago>();
            int offset = (pagNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
            SELECT idPago, concepto, importe, fechaPago, metodoPago, estado,
            idReserva, idUsuarioCreador, idUsuarioAnulador
            FROM pago
            WHERE estado = 1
            ORDER BY idPago
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
                            res.Add(BuscarPago(reader));
                        }
                    }
                }
            }
            return res;
        }


        public Pago? ObtenerPorId(int id)
        {
            Pago? p = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT idPago, concepto, importe, fechaPago, metodoPago, estado,
                        idReserva, idUsuarioCreador, idUsuarioAnulador
                        FROM pago WHERE idPago = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = BuscarPago(reader);
                        }
                    }
                }
            }
            return p;
        }

        private Pago BuscarPago(MySqlDataReader reader)
        {
            return new Pago
            {
                IdPago = reader.GetInt32("idPago"),
                Concepto = reader.IsDBNull(reader.GetOrdinal("concepto")) ? "" : reader.GetString("concepto"),
                Importe = reader.GetDecimal("importe"),
                FechaPago = reader.GetDateTime("fechaPago"),
                MetodoDePago = reader.GetString("metodoPago"),
                Estado = reader.GetBoolean("estado"),
                IdReserva = reader.GetInt32("idReserva"),
                IdUsuarioCreador = reader.GetInt32("idUsuarioCreador"),
                IdUsuarioAnulador = reader.IsDBNull(reader.GetOrdinal("idUsuarioAnulador"))
                    ? (int?)null
                    : reader.GetInt32("idUsuarioAnulador"),
            };
        }

        public IList<Pago> ObtenerPorInmueble(int idInmueble, int pagNro = 1, int tamPagina = 5)
        {
            IList<Pago> res = new List<Pago>();
            int offset = (pagNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
                SELECT p.idPago, p.concepto, p.importe, p.fechaPago, p.metodoPago, p.estado,
                p.idReserva, p.idUsuarioCreador, p.idUsuarioAnulador,
                r.fechaEntrada, r.fechaSalida, r.multa, r.fechaMulta
                FROM pago p
                INNER JOIN reserva r ON p.idReserva = r.idReserva
                WHERE r.idInmueble = @idInmueble
                ORDER BY p.fechaPago DESC
                LIMIT @tamPagina OFFSET @offset;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@idInmueble", MySqlDbType.Int32).Value = idInmueble;
                    cmd.Parameters.AddWithValue("@tamPagina", tamPagina);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pago = BuscarPago(reader);
                            pago.Reserva = new Reserva
                            {
                                IdReserva = pago.IdReserva,
                                FechaEntrada = reader.GetDateTime("fechaEntrada"),
                                FechaSalida = reader.GetDateTime("fechaSalida"),
                                Multa = reader.IsDBNull(reader.GetOrdinal("multa")) ? (decimal?)null : reader.GetDecimal("multa"),
                                FechaMulta = reader.IsDBNull(reader.GetOrdinal("fechaMulta")) ? (DateTime?)null : reader.GetDateTime("fechaMulta")
                            };
                            res.Add(pago);
                        }
                    }
                }
            }
            return res;
        }

        public bool ExistePagoCompletado(int idReserva)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT COUNT(*) FROM pago 
                        WHERE idReserva = @idReserva AND concepto = @concepto AND estado = 1";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    cmd.Parameters.AddWithValue("@concepto", "Completado");
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
        public bool ExistePagoMulta(int idReserva)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT COUNT(*) FROM pago 
                        WHERE idReserva = @idReserva AND concepto = @concepto AND estado = 1";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    cmd.Parameters.AddWithValue("@concepto", "Multa");
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool ExistePagoSenia(int idReserva)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT COUNT(*) FROM pago 
                        WHERE idReserva = @idReserva AND concepto = @concepto AND estado = 1";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    cmd.Parameters.AddWithValue("@concepto", "Seña");
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public decimal? ObtenerImportePorConcepto(int idReserva, string concepto)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT importe FROM pago 
                        WHERE idReserva = @idReserva AND concepto = @concepto AND estado = 1
                        LIMIT 1";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    cmd.Parameters.AddWithValue("@concepto", concepto);
                    conn.Open();
                    var resultado = cmd.ExecuteScalar();
                    return (resultado == null || resultado == DBNull.Value)
                        ? (decimal?)null
                        : Convert.ToDecimal(resultado);
                }
            }
        }

        public IList<Pago> ObtenerPorReserva(int idReserva)
        {
            IList<Pago> res = new List<Pago>();
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT idPago, concepto, importe, fechaPago, metodoPago, estado,
                        idReserva, idUsuarioCreador, idUsuarioAnulador
                        FROM pago
                        WHERE idReserva = @idReserva
                        ORDER BY fechaPago;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(BuscarPago(reader));
                        }
                    }
                }
            }
            return res;
        }

        public IList<Reserva> ObtenerReservasFinalizadas(int cantidad = 10)
        {
            IList<Reserva> res = new List<Reserva>();
            using (var conn = new MySqlConnection(connectionString))
            {
                // "Finalizada" = tiene Completado vigente y nunca tuvo Salida Anticipada,
                // O tuvo Salida Anticipada (fechaMulta no nula) y ya tiene la Multa vigente cobrada.
                string sql = @"
                    SELECT r.idReserva, r.fechaEntrada, r.fechaSalida, r.estado, r.fechaMulta, r.multa,
                           r.idInquilino, r.idInmueble, inq.Nombre, inq.Apellido,
                           SUM(p.importe) AS montoTotal,
                           MAX(p.fechaPago) AS ultimoPago
                    FROM reserva r
                    INNER JOIN inquilino inq ON r.idInquilino = inq.IdInquilino
                    INNER JOIN pago p ON p.idReserva = r.idReserva AND p.estado = 1
                    WHERE
                        (r.fechaMulta IS NULL AND EXISTS (
                            SELECT 1 FROM pago pc WHERE pc.idReserva = r.idReserva AND pc.concepto = 'Completado' AND pc.estado = 1
                        ))
                        OR
                        (r.fechaMulta IS NOT NULL AND EXISTS (
                            SELECT 1 FROM pago pm WHERE pm.idReserva = r.idReserva AND pm.concepto = 'Multa' AND pm.estado = 1
                        ))
                    GROUP BY r.idReserva, r.fechaEntrada, r.fechaSalida, r.estado, r.fechaMulta, r.multa,
                             r.idInquilino, r.idInmueble, inq.Nombre, inq.Apellido
                    ORDER BY ultimoPago DESC
                    LIMIT @cantidad;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(new Reserva
                            {
                                IdReserva = reader.GetInt32(nameof(Reserva.IdReserva)),
                                FechaEntrada = reader.GetDateTime(nameof(Reserva.FechaEntrada)),
                                FechaSalida = reader.GetDateTime(nameof(Reserva.FechaSalida)),
                                Estado = reader.GetBoolean(nameof(Reserva.Estado)),
                                FechaMulta = reader.IsDBNull(reader.GetOrdinal(nameof(Reserva.FechaMulta)))
                                    ? null
                                    : reader.GetDateTime(nameof(Reserva.FechaMulta)),
                                Multa = reader.IsDBNull(reader.GetOrdinal(nameof(Reserva.Multa)))
                                    ? null
                                    : reader.GetDecimal(nameof(Reserva.Multa)),
                                IdInquilino = reader.GetInt32(nameof(Reserva.IdInquilino)),
                                IdInmueble = reader.GetInt32(nameof(Reserva.IdInmueble)),
                                MontoTotalAbonado = reader.GetDecimal("montoTotal"),
                                Inquilino = new Inquilino
                                {
                                    IdInquilino = reader.GetInt32(nameof(Reserva.IdInquilino)),
                                    Nombre = reader.GetString("Nombre"),
                                    Apellido = reader.GetString("Apellido"),
                                }
                            });
                        }
                    }
                }
            }
            return res;
        }
    }
}