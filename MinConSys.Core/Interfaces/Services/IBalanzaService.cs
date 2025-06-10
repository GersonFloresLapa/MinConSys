using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IBalanzaService
    {
        Task<List<Balanza>> ListarBalanzasAsync();
        Task<Balanza> ObtenerPorIdAsync(int id);
        Task<int> CrearBalanzaAsync(Balanza balanza);
        Task<bool> ActualizarBalanzaAsync(Balanza balanza);
        Task<bool> EliminarBalanzaAsync(int id, string usuario);
        Task<List<ComboItem>> ListarBalanzaCboAsync();
    }

}
