using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IClaseRepository
    {
        Task<List<Clase>> GetAllClasesAsync();
        Task<Clase> GetClaseByIdAsync(int id);
        Task<int> AddClaseAsync(Clase clase);
        Task<bool> UpdateClaseAsync(Clase clase);
        Task<bool> DeleteClaseAsync(int id, string usuario);
    }
}
