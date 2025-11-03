using ContratoQR.Entity;

namespace ContratoQR.WEB.Models
{
    public class PersonalViewModel : PersonalQREntity
    {
        public List<PersonalQREntity> ListaPersonal { get; set; } = new();
        public PersonalQREntity personalQR { get; set; } = new();
    }
}
