using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinConSys.Core.Models.Request;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ILeyService
    {
        Task<List<Ley>> ListarLeyesAsync();
        Task<Ley> ObtenerPorIdAsync(int id);
        Task<int> CrearLeyAsync(LeyRequest leyNueva);
        Task<bool> ActualizarLeyAsync(LeyRequest leyActualziar);
        Task<bool> EliminarLeyAsync(int id, string usuario);
    }
}
