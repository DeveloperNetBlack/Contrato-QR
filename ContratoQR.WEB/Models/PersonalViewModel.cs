using ContratoQR.Entity;

namespace ContratoQR.WEB.Models
{
    public class PersonalViewModel : PersonalEntity
    {
        public List<PersonalEntity> ListaPersonal { get; set; } = new();
        public PersonalEntity personalQR { get; set; } = new();
    }
}
