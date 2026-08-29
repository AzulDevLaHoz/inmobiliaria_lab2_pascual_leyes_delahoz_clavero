using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class Inmueble
    {
        [Key]
        [Display(Name = "Código Int.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\.,º°#-]+$", ErrorMessage = "La dirección contiene caracteres no válidos.")]
        public string Direccion { get; set; } = "";

        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, 100, ErrorMessage = "La capacidad debe ser de al menos 1 persona.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "La capacidad debe ser un número entero positivo.")]
        public int Capacidad { get; set; }

        [Display(Name = "Longitud")]
        [RegularExpression(@"^-?([1-8]?\d(\.\d+)?|90(\.0+)?)$", ErrorMessage = "La longitud debe ser una coordenada válida (-180 a 180).")]
        public decimal Longitud { get; set; }

        [Display(Name = "Latitud")]
        [RegularExpression(@"^-?([1-8]?\d(\.\d+)?|90(\.0+)?)$", ErrorMessage = "La latitud debe ser una coordenada válida (-90 a 90).")]
        public decimal Latitud { get; set; }

        [Display(Name = "Portada")]
        public string? StringPortada { get; set; }

        [NotMapped]
        [Display(Name = "Imagen de Portada")]
        public IFormFile? ImagenPortada { get; set; }

        [Display(Name = "Estado Activo")]
        public bool Estado { get; set; } = true;

        [Required(ErrorMessage = "Debe seleccionar un propietario.")]
        [Display(Name = "Propietario")]
        public int PropietarioId { get; set; }

        [ForeignKey(nameof(PropietarioId))]
        public Propietario? Duenio { get; set; }

        [Required(ErrorMessage = "El monto por día es obligatorio.")]
        [Display(Name = "Monto por Día")]
        [Range(0.01, 9999999.99, ErrorMessage = "El monto debe ser mayor a 0.")]
        [RegularExpression(@"^\d+([.,]\d{1,2})?$", ErrorMessage = "Ingrese un monto válido (hasta 2 decimales).")]
        public decimal montoDia { get; set; }

        [Required(ErrorMessage = "El porcentaje es obligatorio.")]
        [Display(Name = "Porcentaje de Reserva")]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100.")]
        [RegularExpression(@"^\d+([.,]\d{1,2})?$", ErrorMessage = "Ingrese un porcentaje válido.")]
        public decimal porcentajeReserva { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de inmueble.")]
        [Display(Name = "Tipo de Inmueble")]
        public int TipoInmuebleId { get; set; }

        [ForeignKey(nameof(TipoInmuebleId))]
        public TipoInmueble? NombreTipo { get; set; }
    }
}