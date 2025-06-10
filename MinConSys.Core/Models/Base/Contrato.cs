using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public string CodigoContrato { get; set; }
        public int IdEmpresa { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string TipoContrato { get; set; }
        public int IdClase { get; set; }
        public int IdProducto { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
