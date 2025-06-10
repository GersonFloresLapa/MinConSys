using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Request
{
    public class EmpresaRequest
    {
        public Empresa Empresa { get; set; }
        public List<TipoEmpresa> TipoEmpresas { get; set; }
    }
}
