using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IBalanzaRepository
    {
        Task<List<Balanza>> GetAllBalanzasAsync();
        Task<Balanza> GetBalanzaByIdAsync(int id);
        Task<int> AddBalanzaAsync(Balanza balanza);
        Task<bool> UpdateBalanzaAsync(Balanza balanza);
        Task<bool> DeleteBalanzaAsync(int id, string usuario);
        Task<List<Balanza>> GetBalanzaCboAsync();
    }
}
