using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class CuentaBancariaDto
    {
        public int IdCuenta { get; set; }
        public string CodigoBanco { get; set; }  
        public string Moneda { get; set; }
        public string TipoCuenta { get; set; }
        public string NroCuenta { get; set; }
        public string Estado { get; set; }
    }
}
