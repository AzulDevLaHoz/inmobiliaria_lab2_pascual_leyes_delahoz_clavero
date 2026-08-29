using System;
using System.ComponentModel.DataAnnotations;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class TipoInmueble
    {
        [Key]
        [Display(Name = "Código Int.")]
        public int IdTipoInmueble { get; set; }

        [Required]
        public String Nombre { get; set; } = "";

        public override string ToString()
        {
            var res = $"{Nombre}";
            return res;
        }
    }

}