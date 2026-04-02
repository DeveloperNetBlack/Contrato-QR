using ContratoQR.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoQR.BLL
{
    public class Establecimiento
    {
        public void Insertar(EstablecimientoEntity establecimiento, IConfiguration configuration)
        {
            DAL.Establecimiento dalEstablecimiento = new DAL.Establecimiento();

            dalEstablecimiento.Insertar(establecimiento, configuration);
        }

        public List<EstablecimientoEntity> Listar(int idEstablecimiento, string nombreEstablecimiento, IConfiguration configuration)
        {
            DAL.Establecimiento dalEstablecimiento = new DAL.Establecimiento();
            return dalEstablecimiento.Listar(idEstablecimiento, nombreEstablecimiento, configuration);
        }
    }
}
