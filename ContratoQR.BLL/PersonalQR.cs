using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks.Dataflow;

namespace ContratoQR.BLL
{
    public class PersonalQR
    {
        public void Insertar(PersonalQREntity personalQR, IConfiguration configuration)
        {
            DAL.PersonalQR dalPersonalQR = new();

            dalPersonalQR.Insertar(personalQR, configuration);
        }

        public void Actualizar(PersonalQREntity personalQR, IConfiguration configuration)
        {
            DAL.PersonalQR dalPersonalQR = new();
            dalPersonalQR.Actualizar(personalQR, configuration);
        }

        public List<PersonalQREntity> Listar(string rutPersona, string nombrePersona, IConfiguration configuration)
        {
            DAL.PersonalQR dalPersonalQR = new();
            return dalPersonalQR.Listar(rutPersona, nombrePersona, configuration);
        }
    }
}
