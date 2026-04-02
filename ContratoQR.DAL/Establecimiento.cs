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
    public class Establecimiento
    {
        public void Insertar(EstablecimientoEntity establecimiento, IConfiguration configuration)
        {
            Connection<PersonalEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_INS_ESTABLECIMIENTO";

            parameters.addParameters("@PI_ID_ESTABLECIMIENTO", TypeData.DataType.Varchar, 0, ParameterDirection.Input, establecimiento.IdEstablecimiento);
            parameters.addParameters("@PI_NOMBRE_ESTABLECIMIENTO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, establecimiento.NombreEstablecimiento!);
            parameters.addParameters("@PI_ID_USUARIO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, "ADMIN");

            conn.ExecuteSQL(parameters);
        }

        public List<EstablecimientoEntity> Listar(int idEstablecimiento, string nombreEstablecimiento, IConfiguration configuration)
        {
            Connection<EstablecimientoEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.Entity;

            parameters.NameProcedure = "SP_SEL_ESTABLECIMIENTO";

            parameters.addParameters("@PI_ID_ESTABLECIMIENTO", TypeData.DataType.Int, 0, ParameterDirection.Input, idEstablecimiento);
            parameters.addParameters("@PI_NOMBRE_ESTABLECIMIENTO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, nombreEstablecimiento);

            conn.ExecuteSQL(parameters);

            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntity.ToList();
            }
            else
            {
                return new List<EstablecimientoEntity>();
            }
        }
       
    }
}
