using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        public String? Concepto { get; set; }

        [Required]
        public decimal Importe { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        [Required]
        public String? MetodoDePago { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        [ForeignKey(nameof(Reserva))]
        public int IdReserva { get; set; }
        public Reserva? Reserva { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int IdUsuarioCreador { get; set; }
        public Usuario? UsuarioCreador { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int? IdUsuarioAnulador { get; set; }
        public Usuario? UsuarioAnulador { get; set; }

    }
}