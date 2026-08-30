using System;
using System.ComponentModel.DataAnnotations;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class Inquilino
    {
        [Key]
        [Display(Name = "Código Int.")]
        public int IdInquilino { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos numéricos.")]
        public string Dni { get; set; } = "";

        [Display(Name = "Teléfono")]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Ingrese un número de teléfono válido (entre 7 y 15 dígitos).")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo no es válido.")]
        public string Email { get; set; } = "";

        [Display(Name = "Estado Activo")]
        public Boolean Estado { get; set; } = false;

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}