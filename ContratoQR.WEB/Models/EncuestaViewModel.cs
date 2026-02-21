using ContratoQR.Entity;

namespace ContratoQR.WEB.Models
{
    public class EncuestaViewModel
    {
        public List<CantidadRespuestaEntity> ListaCantidad { get; set; } = new List<CantidadRespuestaEntity>();
        public List<ResultadoEncuestaEntity> resultadoEncuestas { get; set; } = new List<ResultadoEncuestaEntity>();
        public int CantidadTotalRespuestas { get; set; }
        public int CantidadEncuestasCompletadas { get; set; }
    }
}
