using ContratoQR.Entity;
using DocumentFormat.OpenXml.Presentation;

namespace ContratoQR.WEB.Models
{
    public class FileExcelViewModel : FileExcelEntity
    {
        public List<FileExcelEntity> Funcionarios { get; set; } = new List<FileExcelEntity>();
        public List<PersonalEntity> ListaPersonal { get; set; } = new();
        public FileExcelEntity? Funcionario { get; set; }
        public List<TipoFuncionarioEntity> ListaTipoFuncionario { get; set; } = new();
        public string? CodigoQR { get; set; }
        public string? Mensaje { get; set; }
        public string? IsError { get; set; }
        public int IdCodigoQR { get; set; }
        public int IdPlantaContrata { get; set; }
        public int IdHonorario { get; set; }
    }
}
