using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class ContratoDto
    {
        public int IdContrato { get; set; }
        public string CodigoContrato { get; set; }
        public string TipoContrato { get; set; }
        public string Clase { get; set; }
        public string Producto { get; set; }
        public string Empresa { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        
    }
}
