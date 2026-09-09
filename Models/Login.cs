using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
	public class Login
	{
		[DataType(DataType.EmailAddress)]
        [EmailAddress]
		public string? Email { get; set; }
		[DataType(DataType.Password)]
		public string? Clave { get; set; }
	}
}
