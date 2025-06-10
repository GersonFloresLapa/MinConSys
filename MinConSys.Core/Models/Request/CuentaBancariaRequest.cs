using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Request
{
    public class CuentaBancariaRequest
    {

        public int? IdCuenta { get; set; } // Null cuando sea insertar
        public string CodigoTipoEntidad { get; set; }
        public int IdEntidad { get; set; }
        public string CodigoBanco { get; set; }
        public string Moneda { get; set; }
        public string NroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public CuentaBancaria CuentaBancaria { get; set; }
    }
}