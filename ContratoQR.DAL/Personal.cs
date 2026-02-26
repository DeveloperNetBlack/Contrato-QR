using CompileIT.NET9.DB.SQLServer;
using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ContratoQR.DAL
{
    public class Personal
    {
        public void Insertar(PersonalEntity personal, IConfiguration configuration)
        {
            Connection<PersonalEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_INS_PERSONAL";

            parameters.addParameters("@PI_RUT_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personal.RutPersonal!);
            parameters.addParameters("@PI_NOMBRE_PERSONAL", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NombrePersonal!);
            parameters.addParameters("@PI_APELLIDO_PERSONAL", TypeData.DataType.Varchar, 1000, ParameterDirection.Input, personal.ApellidoPersonal!);
            parameters.addParameters("@PI_ID_ESTABLECIMIENTO", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.IdEstablecimiento);
            parameters.addParameters("@PI_ID_TIPO_CONTRATO", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.IdTipoContrato);
            parameters.addParameters("@PI_NOMBRE_CARGO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NombreCargo!);
            parameters.addParameters("@PI_FEC_INICIO_CONTRATO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecInicioContrato);
            parameters.addParameters("@PI_NRO_FICHA", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.NroFicha);
            parameters.addParameters("@PI_NRO_HORA", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.NroHora);
            parameters.addParameters("@PI_CATEGORIA", TypeData.DataType.Varchar, 1, ParameterDirection.Input, personal.Categoria!);
            parameters.addParameters("@PI_NIVEL", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.Nivel);
            parameters.addParameters("@PI_URL_CONTRATO", TypeData.DataType.Varchar, 1000, ParameterDirection.Input, personal.UrlContrato!);
            parameters.addParameters("@PI_IND_ESTADO", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.IndEstado);
            parameters.addParameters("@PI_ID_USUARIO", TypeData.DataType.Varchar, 0, ParameterDirection.Input, personal.IdUsuario!);

            conn.ExecuteSQL(parameters);
        }

        public void Actualizar(PersonalEntity personal, IConfiguration configuration)
        {
            Connection<PersonalEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_UPD_CONTRATOQR";

            parameters.addParameters("@PI_ID_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personal.IdPersonal);
            parameters.addParameters("@PI_RUT_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personal.RutPersonal!);
            parameters.addParameters("@PI_NOMBRE_PERSONAL", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NombrePersonal!);
            parameters.addParameters("@PI_APELLIDO_PERSONAL", TypeData.DataType.Varchar, 1000, ParameterDirection.Input, personal.ApellidoPersonal!);
            parameters.addParameters("@PI_ID_ESTABLECIMIENTO", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.IdEstablecimiento);
            parameters.addParameters("@PI_ID_TIPO_CONTRATO", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.IdTipoContrato);
            parameters.addParameters("@PI_NOMBRE_CARGO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NombreCargo!);
            parameters.addParameters("@PI_FEC_INICIO_CONTRATO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecInicioContrato);
            parameters.addParameters("@PI_NRO_FICHA", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.NroFicha);
            parameters.addParameters("@PI_NRO_HORA", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.NroHora);
            parameters.addParameters("@PI_CATEGORIA", TypeData.DataType.Varchar, 1, ParameterDirection.Input, personal.Categoria!);
            parameters.addParameters("@PI_NIVEL", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.Nivel);
            parameters.addParameters("@PI_URL_CONTRATO", TypeData.DataType.Varchar, 1000, ParameterDirection.Input, personal.UrlContrato!);
            parameters.addParameters("@PI_IND_ESTADO", TypeData.DataType.Int, 0, ParameterDirection.Input, personal.IndEstado);
            parameters.addParameters("@PI_ID_USUARIO", TypeData.DataType.Varchar, 0, ParameterDirection.Input, personal.IdUsuario!);

            conn.ExecuteSQL(parameters);
        }

        public List<PersonalEntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            Connection<PersonalEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.Entity;

            parameters.NameProcedure = "SP_SEL_PERSONAL";

            parameters.addParameters("@PI_RUT_FUNCIONARIO", TypeData.DataType.Varchar, 12, ParameterDirection.Input, rutPersona);
            parameters.addParameters("@PI_NOMBRE_FUNCIONARIO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, nombrePersona);

            conn.ExecuteSQL(parameters);

            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntity.ToList();
            }
            else
            {
                return new List<PersonalEntity>();
            }
        }
    }
}
