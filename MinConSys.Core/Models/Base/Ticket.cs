using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public string NroTicket { get; set; } 
        public int IdBalanza { get; set; }
        public string TipoOperacion { get; set; }
        public string Clase { get; set; }
        public string Producto { get; set; }
        public int IdLocalidad { get; set; }
        public int IdTransportista { get; set; }
        public int IdVehiculo { get; set; }
        public int IdVehiculo2 { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string GuiaRemision { get; set; }
        public DateTime FechaGuiaRemision { get; set; }
        public string GuiaTransporte { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaPesoBruto { get; set; }
        public decimal PesoBruto { get; set; }
        public DateTime FechaPesoTara { get; set; }
        public decimal PesoTara { get; set; }
        public decimal PesoNeto { get; set; }
        public decimal PesoAcreditacion { get; set; }
        public char Estado { get; set; } = 'A';
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
