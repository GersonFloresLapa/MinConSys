using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Ruma
    {
        public int IdRuma { get; set; }
        public int IdContrato { get; set; }
        public string CodigoRuma { get; set; }
        public string Descripcion { get; set; }
        public int IdProducto { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
