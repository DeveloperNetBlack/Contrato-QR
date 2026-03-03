using ContratoQR.Entity;

namespace ContratoQR.WEB.Models
{
    public class FileExcelViewModel : FileExcelEntity
    {
        public List<FileExcelEntity> Funcionarios { get; set; } = new List<FileExcelEntity>();
        public List<PersonalEntity> ListaPersonal { get; set; } = new();
        public FileExcelEntity? Funcionario { get; set; }
        public string? CodigoQR { get; set; }
        public string? Mensaje { get; set; }
        public string? IsError { get; set; }
        public int IdCodigoQR { get; set; }
        public int IdCertificado { get; set; }
    }
}
