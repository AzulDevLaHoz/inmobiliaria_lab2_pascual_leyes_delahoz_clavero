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
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
        ErrorMessage ="El nombre solo puede contener letras y espacios")]
        public String Nombre { get; set; } = "";

        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
        ErrorMessage ="El apellido solo puede contener letras y espacios")]
        public String Apellido { get; set; } = "";

        [Required]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos numéricos")]
        public String Dni { get; set; } = "";

        [Display(Name = "telefono")]
        [RegularExpression(@"^\d{10}$",
        ErrorMessage ="El telefono es numerico y puede tener 10 digitos ")]
        public String? Telefono { get; set; } = "";

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