using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Request
{
    public class LeyRequest
    {
        public Ley Ley { get; set; }
        public List<LeyTicket> Tickets { get; set; }
        public List<LeyContenido> LeyContenidos { get; set; }
        public bool EsMineral { get; set; }
    }
}
