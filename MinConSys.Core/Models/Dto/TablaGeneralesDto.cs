using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class TablaGeneralesDto
    {
        public int IdGeneral { get; set; }
        public string TipoGeneral { get; set; }  
        public string Codigo { get; set; } 
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
