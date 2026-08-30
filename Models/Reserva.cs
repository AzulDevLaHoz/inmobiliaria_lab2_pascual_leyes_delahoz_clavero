using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models

{
    public class Reserva
    {
        [Key]
        [Display(Name = "Codigo Int.")]
        public int IdReserva { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaEntrada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; } 

        [DataType(DataType.Date)]
        public DateTime? FechaMulta { get; set; }//Puede ser por terminacion anticipada o por exceso de dias

        public decimal? Multa { get; set; }//monto de la multa. En el pago se coloca el concepto si es por irse antes o despues.

        [Required]
        public bool Estado { get; set; } = false;

        // --- Relación con Inquilino ---
        [Required]
        [ForeignKey(nameof(Inquilino))]
        public int IdInquilino { get; set; }

        // --- Relación con Inmueble ---
        [Required]
        [ForeignKey(nameof(Inmueble))]
        public int IdInmueble { get; set; }
    }
}