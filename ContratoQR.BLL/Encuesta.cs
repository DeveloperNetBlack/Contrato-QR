using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoQR.BLL
{
    public class Encuesta
    {
        public List<CantidadRespuestaEntity> ListarCantidad(IConfiguration configuration)
        {            
            DAL.Encuesta encuestaDAL = new DAL.Encuesta();
            
            return encuestaDAL.ListarCantidad(configuration);
        }

        public List<ResultadoEncuestaEntity> ListarResultadoEncuesta(IConfiguration configuration)
        {             
            DAL.Encuesta encuestaDAL = new DAL.Encuesta();
            
            return encuestaDAL.ListarResultadoEncuesta(configuration);
        }
    }
}
