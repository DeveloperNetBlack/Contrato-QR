using ContratoQR.Entity;

namespace ContratoQR.WEB.Models
{
    public class FileExcelViewModel : FileExcel
    {
        public List<FileExcel> Funcionarios { get; set; } = new List<FileExcel>();
        public FileExcel? Funcionario { get; set; }
        public string? CodigoQR { get; set; }
        public string? Mensaje { get; set; }
        public string? IsError { get; set; }
    }
}
