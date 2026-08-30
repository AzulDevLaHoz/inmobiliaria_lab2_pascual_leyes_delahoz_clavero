using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class RepositorioReserva : RepositorioBase, IRepositorioReserva
    {
        public RepositorioReserva(IConfiguration configuration) : base(configuration)
        {
        }

        public int ObtenerCantidad
        {
            get
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    string sql = "SELECT COUNT(*) FROM reserva";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        conn.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
        }

        public int Alta(Reserva p)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO reserva
                (fechaEntrada, fechaSalida, estado, fechaTerminacionAnticipada, multa, idInquilino, idInmueble)
                VALUES (@fe, @fs, @es, @fta, @multa, @idInq, @idInm)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fe", p.FechaEntrada);
                    cmd.Parameters.AddWithValue("@fs", p.FechaSalida);
                    cmd.Parameters.AddWithValue("@es", true);
                    cmd.Parameters.AddWithValue("@fta", (object?)p.FechaTerminacionAnticipada ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@multa", (object?)p.Multa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idInq", p.IdInquilino);
                    cmd.Parameters.AddWithValue("@idInm", p.IdInmueble);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    p.IdReserva = Convert.ToInt32(cmd.LastInsertedId);
                    return p.IdReserva;
                }
            }
        }

        public int Baja(int id)
        {
            int res = -1;
            var estado = true;

            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE reserva SET estado = @es WHERE idReserva = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@es", estado);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }

        public int Modificar(Reserva p)
        {
            int res = -1;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE reserva SET
                fechaEntrada = @fe,
                fechaSalida = @fs,
                estado = @estado,
                fechaTerminacionAnticipada = @fta,
                multa = @multa,
                idInquilino = @idInq,
                idInmueble = @idInm
                WHERE idReserva = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fe", p.FechaEntrada);
                    cmd.Parameters.AddWithValue("@fs", p.FechaSalida);
                    cmd.Parameters.AddWithValue("@estado", p.Estado);
                    cmd.Parameters.AddWithValue("@fta", (object?)p.FechaTerminacionAnticipada ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@multa", (object?)p.Multa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idInq", p.IdInquilino);
                    cmd.Parameters.AddWithValue("@idInm", p.IdInmueble);
                    cmd.Parameters.AddWithValue("@id", p.IdReserva);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }

        public IList<Reserva> ObtenerLista(int paginaNro = 1, int tamPagina = 10)
        {
            IList<Reserva> res = new List<Reserva>();
            int offset = (paginaNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
                SELECT idReserva, fechaEntrada, fechaSalida, estado, fechaTerminacionAnticipada, multa, idInquilino, idInmueble
                FROM reserva
                ORDER BY idReserva
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
                            //crear objeto reserva para traer objeto a la lista
                        }
                    }
                }
            }
            return res;
        }

        public IList<Reserva> ObtenerListaActivos(int paginaNro = 1, int tamPagina = 10)
        {
            IList<Reserva> res = new List<Reserva>();
            int offset = (paginaNro - 1) * tamPagina;

            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"
                SELECT idReserva, fechaEntrada, fechaSalida, estado, fechaTerminacionAnticipada, multa, idInquilino, idInmueble
                FROM reserva
                WHERE estado = 1
                ORDER BY idReserva
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
                            //crear objeto reserva para traer objeto a la lista
                        }
                    }
                }
            }
            return res;
        }

        public Reserva? ObtenerPorId(int id)
        {
            Reserva? p = null;
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT idReserva, fechaEntrada, fechaSalida, estado, fechaTerminacionAnticipada, multa, idInquilino, idInmueble
                FROM reserva
                WHERE idReserva = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //traer objeto reserva 
                        }
                    }
                }
            }
            return p;
        }
    }
}




