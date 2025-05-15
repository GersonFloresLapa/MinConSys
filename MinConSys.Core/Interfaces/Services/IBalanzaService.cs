using MinConSys.Core.Models.Base;
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
    }

}
