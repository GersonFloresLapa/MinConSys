using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class VehiculoDto
    {
        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int? Anio { get; set; }
        public string TipoVehiculoCodigo { get; set; }
        public decimal? CapacidadToneladas { get; set; }
        public string Transportista { get; set; }
        
    }
}
