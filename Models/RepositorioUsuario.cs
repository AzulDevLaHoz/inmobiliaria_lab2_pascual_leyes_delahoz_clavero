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
            int res =-1;
            using (var conn=new MySqlConnection(connectionString) )
            {
                string sql= "INSERT INTO USUARIO (Nombre,Apellido ,Email,Clave,Avatar,idRol,estado ) VALUES (@n,@a,@e,@c,,@av,@r,@e) ";
                using (var command= new MySqlCommand( sql,conn))
                {
                   command.Parameters.AddWithValue("@n",u.Nombre);
                   command.Parameters.AddWithValue("@a",u.Apellido);
                   command.Parameters.AddWithValue("@e",u.Email);
                   command.Parameters.AddWithValue("@c",u.Clave);
                   command.Parameters.AddWithValue("@av", null);
                   command.Parameters.AddWithValue("@r",u.IdRol); 
                   command.Parameters.AddWithValue("@e",true); 
                   conn.Open();
                   res= command.ExecuteNonQuery();
                
                   
                }
            }
            return res;
        }

        public int Baja(int id)
		{
			int res = -1;
			using (var conn = new MySqlConnection(connectionString))
			{
				string sql = "UPDATE Usuario SET Estado=false WHERE Id = @id";
				using (var command= new MySqlCommand(sql, conn))
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
            SELECT u.Id, u.Nombre, u.Apellido, u.Email, u.Clave, u.Avatar, u.IdRol, u.Estado, r.Nombre AS NombreRol
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
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString() ?? "",
                        Apellido = reader["Apellido"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Clave = reader["Clave"].ToString() ?? "",
                        Avatar = reader["Avatar"] != DBNull.Value ? reader["Avatar"].ToString() : null,
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        Estado = Convert.ToBoolean(reader["Estado"]),
                        rol = new Rol
                        {
                            Id = Convert.ToInt32(reader["IdRol"]),
                            Nombre = reader["NombreRol"].ToString() ?? ""
                        }
                    };
                }
            }
        }
    }
    return u;
}
    }

}