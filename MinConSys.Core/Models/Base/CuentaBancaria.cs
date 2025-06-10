using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class CuentaBancaria
    {
        public int IdCuenta { get; set; }
        public string CodigoTipoEntidad { get; set; } = string.Empty;
        public int IdEntidad { get; set; }
        public string CodigoBanco { get; set; }
        public string Moneda { get; set; }
        public string NroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public char Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
