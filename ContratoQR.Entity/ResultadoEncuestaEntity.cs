namespace ContratoQR.Entity
{
    public class ResultadoEncuestaEntity
    {
        public int IdPregunta { get; set; }
        public string? DescripcionPregunta { get; set; }
        public int ValorRespuesta { get; set; }
        public string? DescripcionValorRespuesta { get; set; }
    }
}
