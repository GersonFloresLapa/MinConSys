using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }              // Ej: Cu
        public string NombreCompleto { get; set; }      // Ej: Cobre
        public decimal Precio { get; set; }
        public string Unidad { get; set; }               // Ej: USD/TM
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
