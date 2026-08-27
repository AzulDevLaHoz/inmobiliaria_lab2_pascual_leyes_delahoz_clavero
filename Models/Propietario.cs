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
        ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public String Nombre { get; set; } = "";

        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
        ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public String Apellido { get; set; } = "";

        [Required]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos numéricos.")]
        public String Dni { get; set; } = "";

        [Display(Name = "telefono")]
        [RegularExpression(@"^\d{10}$",
        ErrorMessage = "Ingrese un número de teléfono válido (entre 7 y 15 dígitos).")]
        public String? Telefono { get; set; } = "";

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo no es válido.")]
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