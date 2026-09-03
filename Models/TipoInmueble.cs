using System;
using System.ComponentModel.DataAnnotations;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class TipoInmueble
    {
        [Key]
        [Display(Name = "Código Int.")]
        public int IdTipoInmueble { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public String Nombre { get; set; } = "";

        public override string ToString()
        {
            var res = $"{Nombre}";
            return res;
        }
    }

}