using CompileIT.NET9.DB.SQLServer;
using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ContratoQR.DAL
{
    public class PersonalHonorario
    {
        public void Insertar(PersonalHonorarioEntity personal, IConfiguration configuration)
        {
            Connection<PersonalHonorarioEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_INS_PERSONAL_HONORARIO";

            parameters.addParameters("@PI_RUT_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personal.RutPersonal!);
            parameters.addParameters("@PI_NOMBRE_PERSONAL", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.NombrePersonal!);
            parameters.addParameters("@PI_NOMBRE_CARGO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NombreCargo!);
            parameters.addParameters("@PI_NRO_DECRETO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, personal.NroDecreto!);
            parameters.addParameters("@PI_COMETIDO", TypeData.DataType.Text, 0, ParameterDirection.Input, personal.Cometido!);
            parameters.addParameters("@PI_NOMBRE_PROGRAMA", TypeData.DataType.Varchar, 500, ParameterDirection.Input, personal.NombrePrograma!);
            parameters.addParameters("@PI_NRO_CUENTA_PRESUPUESTARIA", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NroCuentaPresupuestaria!);
            parameters.addParameters("@PI_MONTO", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.Monto!);
            parameters.addParameters("@PI_NOMBRE_ESTABLECIMIENTO", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.NombreEstablecimiento!);
            parameters.addParameters("@PI_FEC_INGRESO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecIngreso!);
            parameters.addParameters("@PI_FEC_INICIO_CONTRATO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecInicioContrato!);
            parameters.addParameters("@PI_FEC_TERMINO_CONTRATO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecTerminoContrato!);
            parameters.addParameters("@PI_FEC_NACIMIENTO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecNacimiento!);
            parameters.addParameters("@PI_DIRECCION", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.Direccion!);
            parameters.addParameters("@PI_NOMBRE_COMUNA", TypeData.DataType.Varchar, 50, ParameterDirection.Input, personal.NombreComuna!);
            parameters.addParameters("@PI_SEXO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, personal.Sexo!);
            parameters.addParameters("@PI_CORREO_ELECTRONICO", TypeData.DataType.Varchar, 255, ParameterDirection.Input, personal.CorreoElectronico!);
            parameters.addParameters("@PI_ID_USUARIO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, personal.IdUsuario!);

            conn.ExecuteSQL(parameters);
        }

        public void Actualizar(PersonalHonorarioEntity personal, IConfiguration configuration)
        {
            Connection<PersonalHonorarioEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.None;

            parameters.NameProcedure = "SP_UPD_PERSONAL_HONORARIO";

            parameters.addParameters("@PI_ID_PERSONAL_HONORARIO", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personal.IdPersonalHonorario);
            parameters.addParameters("@PI_RUT_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, personal.RutPersonal!);
            parameters.addParameters("@PI_NOMBRE_PERSONAL", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.NombrePersonal!);
            parameters.addParameters("@PI_NOMBRE_CARGO", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NombreCargo!);
            parameters.addParameters("@PI_NRO_DECRETO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, personal.NroDecreto!);
            parameters.addParameters("@PI_COMETIDO", TypeData.DataType.Text, 0, ParameterDirection.Input, personal.Cometido!);
            parameters.addParameters("@PI_NOMBRE_PROGRAMA", TypeData.DataType.Varchar, 500, ParameterDirection.Input, personal.NombrePrograma!);
            parameters.addParameters("@PI_NRO_CUENTA_PRESUPUESTARIA", TypeData.DataType.Varchar, 150, ParameterDirection.Input, personal.NroCuentaPresupuestaria!);
            parameters.addParameters("@PI_MONTO", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.Monto!);
            parameters.addParameters("@PI_NOMBRE_ESTABLECIMIENTO", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.NombreEstablecimiento!);
            parameters.addParameters("@PI_FEC_INGRESO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecIngreso!);
            parameters.addParameters("@PI_FEC_INICIO_CONTRATO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecInicioContrato!);
            parameters.addParameters("@PI_FEC_TERMINO_CONTRATO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecTerminoContrato!);
            parameters.addParameters("@PI_FEC_NACIMIENTO", TypeData.DataType.Date, 0, ParameterDirection.Input, personal.FecNacimiento!);
            parameters.addParameters("@PI_DIRECCION", TypeData.DataType.Varchar, 350, ParameterDirection.Input, personal.Direccion!);
            parameters.addParameters("@PI_NOMBRE_COMUNA", TypeData.DataType.Varchar, 50, ParameterDirection.Input, personal.NombreComuna!);
            parameters.addParameters("@PI_SEXO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, personal.Sexo!);
            parameters.addParameters("@PI_CORREO_ELECTRONICO", TypeData.DataType.Varchar, 255, ParameterDirection.Input, personal.CorreoElectronico!);
            parameters.addParameters("@PI_ID_USUARIO", TypeData.DataType.Varchar, 20, ParameterDirection.Input, personal.IdUsuario!);

            conn.ExecuteSQL(parameters);
        }

        public List<PersonalHonorarioEntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            Connection<PersonalHonorarioEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.Entity;

            parameters.NameProcedure = "SP_SEL_PERSONAL_HONORARIO";

            parameters.addParameters("@PI_RUT_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, rutPersona);
            parameters.addParameters("@PI_NOMBRE_PERSONAL", TypeData.DataType.Varchar, 150, ParameterDirection.Input, nombrePersona);

            conn.ExecuteSQL(parameters);

            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntity.ToList();
            }
            else
            {
                return new List<PersonalHonorarioEntity>();
            }
        }

        public PersonalHonorarioEntity Listar(string rutPersonal, IConfiguration configuration)
        {
            Connection<PersonalHonorarioEntity> conn = new(configuration);
            Parameters parameters = new Parameters();

            conn.Devolution = TypeRefund.Register.EntitySingle;

            parameters.NameProcedure = "SP_SEL_PERSONAL_HONORARIO";

            parameters.addParameters("@PI_RUT_PERSONAL", TypeData.DataType.Varchar, 12, ParameterDirection.Input, rutPersonal);
            parameters.addParameters("@PI_NOMBRE_PERSONAL", TypeData.DataType.Varchar, 150, ParameterDirection.Input, string.Empty);

            conn.ExecuteSQL(parameters);

            if (conn.ReturnEntity != null)
            {
                return conn.ReturnEntitySingle!;
            }
            else
            {
                return new PersonalHonorarioEntity();
            }
        }
    }
}
