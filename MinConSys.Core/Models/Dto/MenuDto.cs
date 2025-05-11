using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class MenuDto
    {
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string NombreInterno { get; set; }
        public int? PadreId { get; set; }
        public int Orden { get; set; }
        public List<MenuDto> Hijos { get; set; } = new List<MenuDto>();
    }
}
