using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;

namespace ContratoQR.BLL
{
    public class Personal
    {
        public void Insertar(PersonalEntity personalQR, IConfiguration configuration)
        {
            DAL.Personal dalPersonalQR = new();

            dalPersonalQR.Insertar(personalQR, configuration);
        }

        public void Actualizar(PersonalEntity personalQR, IConfiguration configuration)
        {
            DAL.Personal dalPersonalQR = new();
            dalPersonalQR.Actualizar(personalQR, configuration);
        }

        public List<PersonalEntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            DAL.Personal dalPersonalQR = new();
            return dalPersonalQR.Listar(rutPersona, nombrePersona, configuration);
        }
    }
}
