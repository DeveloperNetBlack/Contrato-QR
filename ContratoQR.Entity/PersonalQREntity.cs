using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoQR.Entity
{
    public class PersonalQREntity
    {
        public int IdPersonalQR { get; set; }
        public string? RutFuncionario { get; set; }
        public string? NombreFuncionario { get; set; }
        public string? UrlContrato { get; set; }
        public int IndEstado { get; set; }
    }
}
