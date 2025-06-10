using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class RumaTicketDto
    {
        public int IdRumaTicket { get; set; }
        public int IdTicket { get; set; }
        public string NroTicket { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public decimal PesoBruto { get; set; }
        public decimal PesoTara { get; set; }
        public decimal PesoNeto { get; set; }
        public string Tracto { get; set; }
        public string Remolque { get; set; }
        public string Procedencia { get; set; }
        public string GuiaRemision { get; set; }
        public string GuiaTransporte { get; set; }
        public string Balanza { get; set; }
        public string Transportista { get; set; }
        public string AdjuntTK { get; set; }
        public string AdjuntoGR { get; set; }
        public string AdjuntoGT { get; set; }
    }
}
