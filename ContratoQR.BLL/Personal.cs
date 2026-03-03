using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;

namespace ContratoQR.BLL
{
    public class Personal
    {
        public void Insertar(PersonalEntity personal, IConfiguration configuration)
        {
            DAL.Personal dalPersonal = new();

            dalPersonal.Insertar(personal, configuration);
        }

        public void Actualizar(PersonalEntity personal, IConfiguration configuration)
        {
            DAL.Personal dalPersonal = new();

            dalPersonal.Actualizar(personal, configuration);
        }

        public List<PersonalEntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            DAL.Personal dalPersonal = new();

            return dalPersonal.Listar(rutPersona, nombrePersona, configuration);
        }

        public PersonalEntity Listar(string rutPersonal, IConfiguration configuration)
        {
            DAL.Personal dalPersonal = new();
            
            return dalPersonal.Listar(rutPersonal, configuration);
        }
    }
}
