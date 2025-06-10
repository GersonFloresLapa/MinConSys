using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class TicketDto
    {
        public int IdTicket { get; set; }
        public string Empresa { get; set; }
        public string NroTicket { get; set; }
        public string Balanza { get; set; }
        public string TipoOperacion { get; set; }
        public string Clase { get; set; }
        public string Producto { get; set; }
        public string Procedencia { get; set; }
        public string Deposito { get; set; }
        public string Transportista { get; set; }
        public string EstadoContribuyente { get; set; }
        public string Chofer { get; set; }
        public string Brevete { get; set; }
        public string Tracto { get; set; }
        public string Remolque { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Proveedor { get; set; }
        public string GuiaRemision { get; set; }
        public DateTime FechaGuiaRemision { get; set; }
        public string GuiaTransporte { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaPesoBruto { get; set; }
        public Decimal PesoBruto { get; set; }
        public DateTime FechaPesoTara { get; set; }
        public Decimal PesoTara { get; set; }
        public Decimal PesoNeto { get; set; }
        public Decimal PesoAcreditacion { get; set; }
        public string Estado { get; set; }

    }
}
