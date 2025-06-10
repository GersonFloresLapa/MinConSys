using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class EmpresaDto
    {
        public int IdEmpresa { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionFiscal { get; set; }
        public string DireccionComercial { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionContribuyente { get; set; }
        public string PartidaElectronica { get; set; }
        public string ZonaPartidaElectronica { get; set; }
    }
}
