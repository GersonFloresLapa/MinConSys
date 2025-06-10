using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class LeyContenido
    {
        public int IdLeyContenido { get; set; }
        public int IdLey { get; set; }
        public string Elemento { get; set; }
        public decimal Contenido { get; set; }
    }
}
