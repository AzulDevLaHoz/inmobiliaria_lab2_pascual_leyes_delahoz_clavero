using System;
using System.ComponentModel.DataAnnotations;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class Propietario
    {
        [Key]
        [Display(Name = "Código Int.")]
        public int IdPropietario { get; set; }

        [Required]
        public String Nombre { get; set; } = "";

        [Required]
        public String Apellido { get; set; } = "";

        [Required]
        public String Dni { get; set; } = "";

        [Display(Name = "telefono")]
        public String Telefono { get; set; } = "";

        [Required, EmailAddress]
        public String Email { get; set; } = "";
        [Required]
        public bool Estado { get; set; }
        public override string ToString()
        {
            var res = $"{Nombre} {Apellido}";

            return res;
        }
    }
}