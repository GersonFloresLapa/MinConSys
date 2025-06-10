using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionFiscal { get; set; }
        public string DireccionComercial { get; set; }
        public string Ubigeo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionContribuyente { get; set; }
        public string PartidaElectronica { get; set; }
        public string ZonaPartidaElectronica { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
