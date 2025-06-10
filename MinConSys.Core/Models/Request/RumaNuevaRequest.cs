using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Request
{
    public class RumaNuevaRequest
    {
        public Ruma Ruma { get; set; }
        public RumaEstado Estado { get; set; }
        public RumaEstadoProceso Procesos { get; set; }
        public bool EsMineral { get; set; }
        public bool RumaYaExiste { get; set; }
    }
}
