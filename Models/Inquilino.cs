using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{

    public class Inquilino
    {
        [Key]
        [Display(Name = "Código Int.")]
        public int IdInquilino { get; set; }

        [Required]
        public String Nombre { get; set; } = "";

        [Required]
        public String Apellido { get; set; } = "";

        [Required]
        public String Dni { get; set; } = "";

        [Display(Name = "Teléfono")]
        public String Telefono { get; set; } = "";
         
        [Required, EmailAddress]
        public String Email { get; set; } = "";

        [Required] 
        public bool estado {get;set;}
        
        public override string ToString()
        {
            var res = $"{Nombre} {Apellido}";

            return res;
        }
    }
}