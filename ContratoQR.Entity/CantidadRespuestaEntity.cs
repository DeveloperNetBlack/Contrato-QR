using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoQR.Entity
{
    public class CantidadRespuestaEntity
    {
        public int IdPregunta { get; set; }
        public string? DescripcionPregunta { get; set; }
        public int CantidadRespuesta { get; set; }
    }
}
