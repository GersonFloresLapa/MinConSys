using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface ILocalidadRepository
    {
        Task<List<LocalidadDto>> GetAllLocalidadesAsync();
        Task<Localidad> GetLocalidadByIdAsync(int id);
        Task<int> AddLocalidadAsync(Localidad localidad);
        Task<bool> UpdateLocalidadAsync(Localidad localidad);
        Task<bool> DeleteLocalidadAsync(int id, string usuario);
        Task<List<Localidad>> GetLocalidadByTipoAsync(string tipo);
    }
}
