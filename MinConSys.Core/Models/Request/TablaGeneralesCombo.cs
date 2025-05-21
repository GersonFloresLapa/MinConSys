using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Request
{
    public class TablaGeneralesCombo
    {
       
        public string Codigo { get; set; }
        public string Valor{ get; set; }
        public override string ToString()
        {
            return Valor;
        }

    }
}
