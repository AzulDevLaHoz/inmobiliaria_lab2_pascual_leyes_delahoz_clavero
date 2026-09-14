using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{

    public class Usuario
    {
        [Key]
        [Display(Name = "Codigo")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio ")]
        public string Nombre { get; set; } = "";
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string Apellido { get; set; } = "";
        [Required(ErrorMessage = "El Email es obligatorio"), EmailAddress]
        public string Email { get; set; } = "";
        //[Required(ErrorMessage ="la clave es obligatoria"), DataType(DataType.Password)]
        public string Clave { get; set; } = "";

        public string? Avatar { get; set; }

        [NotMapped]
        public IFormFile? AvatarFile { get; set; }

        [Required]
        public int IdRol { get; set; }

        public Rol? rol { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}