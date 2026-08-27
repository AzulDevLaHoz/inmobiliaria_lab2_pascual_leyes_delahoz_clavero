using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
public class Inmueble

    {
        [Key]
        [Display(Name = "Código Int.")]
        public int Id {get; set;}

        [Required]
        public String? Direccion {get; set;}

        [Required]
        public int Capacidad {get; set;}

        [Required]
        public Decimal Precio {get; set;}
        public Decimal Longitud {get; set;}
        public Decimal Latitud {get; set;}
        public IFormFile? ImagenPortada {get; set;}
        public bool Estado {get; set;} 
        public int PropietarioId {get; set;}

        [ForeignKey(nameof(Propietario.IdPropietario))]
        public Propietario? Duenio {get; set;}

        public int TipoInmuebleId {get; set;}
        [ForeignKey(nameof(TipoInmueble.Id))]
        public TipoInmueble? NombreTipo {get; set;} 
    }
}