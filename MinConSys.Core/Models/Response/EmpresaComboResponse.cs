using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Response
{
    public class EmpresaComboResponse
    {
        public int IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string DisplayText { get; set; }
        public override string ToString() => DisplayText;
    }
}
