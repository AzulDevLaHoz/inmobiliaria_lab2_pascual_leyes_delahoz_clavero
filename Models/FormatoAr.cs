using System.Globalization;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    public static class FormatoAr
    {
        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");

        public static string Moneda(decimal valor)
        {
            return "$ " + valor.ToString("N2", Cultura);
        }
    }
}