namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models
{
    // Criterio  de clasificacin de reservas por estado de pago.
    // Lo usan tanto RepositorioReserva como RepositorioPago para que las dos pantallas
    // (Reserva/Index y Pago/Index-Buscar) clasifiquen exactamente igual.
    public static class FiltroEstadoReserva
    {
        
        private const string CriterioFinalizada = @"
            (r.fechaMulta IS NULL AND EXISTS (
                SELECT 1 FROM pago pc WHERE pc.idReserva = r.idReserva AND pc.concepto = 'Completado' AND pc.estado = 1
            ))
            OR
            (r.fechaMulta IS NOT NULL AND EXISTS (
                SELECT 1 FROM pago pm WHERE pm.idReserva = r.idReserva AND pm.concepto = 'Multa' AND pm.estado = 1
            ))";

        // Devuelve el fragmento "AND (...)" a concatenar en el WHERE, o "" para "Todos".
        // Valores validos de estado: "Todos", "EnCurso", "Finalizada", "AdeudaPago".
        public static string Construir(string estado)
        {
            return estado switch
            {
                "Finalizada" => $"AND ({CriterioFinalizada})",
                "AdeudaPago" => $"AND NOT ({CriterioFinalizada}) AND r.fechaSalida < CURDATE()",
                "EnCurso" => $"AND NOT ({CriterioFinalizada}) AND r.fechaSalida >= CURDATE()",
                _ => "",
            };
        }
    }
}