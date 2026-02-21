using CompileIT.NET9.DB.SQLServer;
using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoQR.DAL
{
    public class Encuesta
    {
        public List<CantidadRespuestaEntity> ListarCantidad(IConfiguration configuration)
        {
            Connection<CantidadRespuestaEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.Entity;

            parameters.NameProcedure = "SP_SEL_CANTIDAD_RESPUESTA";

            conn.ExecuteSQL(parameters);

            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntity.ToList();
            }
            else
            {
                return new List<CantidadRespuestaEntity>();
            }
        }

        public List<ResultadoEncuestaEntity> ListarResultadoEncuesta(IConfiguration configuration)
        {
            Connection<ResultadoEncuestaEntity> conn = new(configuration);
            Parameters parameters = new Parameters();
            
            conn.Devolution = TypeRefund.Register.Entity;

            parameters.NameProcedure = "SP_SEL_RESULTADO_ENCUESTA";
            
            conn.ExecuteSQL(parameters);
            
            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntity.ToList();
            }
            else
            {
                return new List<ResultadoEncuestaEntity>();
            }
        }
    }
}
