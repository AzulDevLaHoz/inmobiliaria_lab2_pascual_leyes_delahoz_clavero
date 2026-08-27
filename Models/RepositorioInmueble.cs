using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
public class RepositorioInmueble :RepositorioBase
{
    public RepositorioInmueble(IConfiguration configuration) : base (configuration)
        {
            
        }

    public int Alta(Inmueble inmueble)
        {
       int res =-1;
            using(var conn= new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Inmueble
                 (Direccion,capacidad,latitud,longitud,
                porcentajeReserva,imagenPortada,montoDia,estado,idPropietario,idTipoInmueble) 
                 VALUES (@direc,@capacidad,@lat,@lon,@porcentaje,@imagen,@montoDia,@est,@idProp,@idTipo)";

                 using (var cmd= new MySqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@direc",inmueble.Direccion);
                    cmd.Parameters.AddWithValue("@capacidad",inmueble.Capacidad);
                    cmd.Parameters.AddWithValue("@lat",inmueble.Latitud);
                    cmd.Parameters.AddWithValue("@lon",inmueble.Longitud);
                    cmd.Parameters.AddWithValue("@porcentaje",inmueble.porcentajeReserva);
                    cmd.Parameters.AddWithValue("@imagen",inmueble.ImagenPortada);
                    cmd.Parameters.AddWithValue("@montoDia",inmueble.montoDia);
                    cmd.Parameters.AddWithValue("@est",true);
                    cmd.Parameters.AddWithValue("@idProp",inmueble.PropietarioId);
                    cmd.Parameters.AddWithValue("@idTipo",inmueble.TipoInmuebleId);
                   conn.Open();
                   res=Convert.ToInt32(cmd.ExecuteScalar());
                   inmueble.Id=res; 
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
				string sql = @$"UPDATE Inmuebles  SET estado=false  WHERE Id = @id";
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

        public int Modificar (Inmueble inmueble)
        {
            int res = -1; 
            using (var conn= new MySqlConnection(connectionString))
            {
                string sql= @$"UPDATE INMUEBLE  SET Direccion=@direc,
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
                using (var cmd=new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@direc", inmueble.Direccion);
                    cmd.Parameters.AddWithValue("@capacidad", inmueble.Capacidad);
                    cmd.Parameters.AddWithValue("@lat", inmueble.Latitud);
                    cmd.Parameters.AddWithValue("@lon", inmueble.Longitud);
                    cmd.Parameters.AddWithValue("@porcentaje", inmueble.porcentajeReserva);
                    cmd.Parameters.AddWithValue("@imagen", inmueble.ImagenPortada);
                    cmd.Parameters.AddWithValue("@monto", inmueble.montoDia);
                    cmd.Parameters.AddWithValue("@estado", inmueble.Estado);
                    cmd.Parameters.AddWithValue("@idprop", inmueble.PropietarioId);
                    cmd.Parameters.AddWithValue("@idtipo", inmueble.TipoInmuebleId);
                     cmd.Parameters.AddWithValue("@id", inmueble.Id);
                     cmd.CommandType= CommandType.Text; 
                     conn.Open();
                     res= cmd.ExecuteNonQuery();
                     conn.Close();

                }
            } 
            return res;
        }
}

}