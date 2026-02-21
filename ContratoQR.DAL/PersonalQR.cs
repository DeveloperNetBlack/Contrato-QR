using CompileIT.NET9.DB.SQLServer;
using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ContratoQR.DAL
{
    public class PersonalQR
    {
        public void Insertar(PersonalQREntity personalQR, IConfiguration configuration)
        {
            Connection<PersonalQREntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_INS_CONTRATOQR";

            parameters.addParameters("@PI_RUT_FUNCIONARIO", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personalQR.RutFuncionario!);
            parameters.addParameters("@PI_NOMBRE_FUNCIONARIO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personalQR.NombreFuncionario!);
            parameters.addParameters("@PI_URL_CONTRATO", TypeData.DataType.Varchar, 1000, ParameterDirection.Input, personalQR.UrlContrato!);
            parameters.addParameters("@PI_IND_ESTADO", TypeData.DataType.Int, 0, ParameterDirection.Input, personalQR.IndEstado);

            conn.ExecuteSQL(parameters);
        }

        public void Actualizar(PersonalQREntity personalQR, IConfiguration configuration)
        {
            Connection<PersonalQREntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_UPD_CONTRATOQR";

            parameters.addParameters("@PI_ID_CONTRATOQR", TypeData.DataType.Int, 0, ParameterDirection.Input, personalQR.IdPersonalQR);
            parameters.addParameters("@PI_RUT_FUNCIONARIO", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personalQR.RutFuncionario!);
            parameters.addParameters("@PI_NOMBRE_FUNCIONARIO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personalQR.NombreFuncionario!);
            parameters.addParameters("@PI_URL_CONTRATO", TypeData.DataType.Varchar, 1000, ParameterDirection.Input, personalQR.UrlContrato!);
            parameters.addParameters("@PI_IND_ESTADO", TypeData.DataType.Int, 0, ParameterDirection.Input, personalQR.IndEstado);

            conn.ExecuteSQL(parameters);
        }

        public List<PersonalQREntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            Connection<PersonalQREntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.Entity;

            parameters.NameProcedure = "SP_SEL_CONTRATOQR";

            parameters.addParameters("@PI_RUT_FUNCIONARIO", TypeData.DataType.Varchar, 12, ParameterDirection.Input, rutPersona);
            parameters.addParameters("@PI_NOMBRE_FUNCIONARIO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, nombrePersona);

            conn.ExecuteSQL(parameters);

            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntity.ToList();
            }
            else
            {
                return new List<PersonalQREntity>();
            }
        }
    }
}
