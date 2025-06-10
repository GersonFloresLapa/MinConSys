using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class RumaEstado
    {
        public int IdRumaEstado { get; set; }
        public int IdRuma { get; set; }
        public int IdClase { get; set; }
        public int? IdProcedencia { get; set; }
        public int IdDeposito { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdDestino { get; set; }
        public decimal? TmhEstimado { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
