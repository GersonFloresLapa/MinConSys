using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class RumaDto
    {
        public int IdRuma { get; set; }
        public int IdRumaEstado { get; set; }
        public string Contrato { get; set; }
        public string Ruma { get; set; }
        public string Clase { get; set; }
        public string Producto { get; set; }
        public string Empresa { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string Proveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public string Procedencia { get; set; }
        public string Deposito { get; set; }
        public decimal TmhEstimado { get; set; }
        public string Descripcion { get; set; }
    }
}
