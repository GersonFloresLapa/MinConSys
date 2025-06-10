using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface ILeyRepository
    {
        Task<List<Ley>> GetAllLeyAsync();
        Task<Ley> GetLeyByIdAsync(int id);
        Task<int> AddLeyAsync(LeyRequest leyInsert);
        Task<bool> UpdateLeyAsync(LeyRequest leyUpdate);
        Task<bool> DeleteLeyAsync(int id, string usuario);
    }
}
