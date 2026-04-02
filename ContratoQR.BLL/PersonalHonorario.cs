using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;

namespace ContratoQR.BLL
{
    public class PersonalHonorario
    {
        public void Insertar(PersonalHonorarioEntity personal, IConfiguration configuration)
        {
            DAL.PersonalHonorario dalPersonal = new();

            dalPersonal.Insertar(personal, configuration);
        }

        public void Actualizar(PersonalHonorarioEntity personal, IConfiguration configuration)
        {
            DAL.PersonalHonorario dalPersonal = new();

            dalPersonal.Actualizar(personal, configuration);
        }

        public List<PersonalHonorarioEntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            DAL.PersonalHonorario dalPersonal = new();

            return dalPersonal.Listar(rutPersona, nombrePersona, configuration);
        }

        public PersonalHonorarioEntity Listar(string rutPersonal, IConfiguration configuration)
        {
            DAL.PersonalHonorario dalPersonal = new();

            return dalPersonal.Listar(rutPersonal, configuration);
        }
    }
}
