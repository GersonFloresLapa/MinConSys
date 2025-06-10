using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface ITablaGeneralesRepository
    {
        Task<List<TablaGenerales>> GetAllTablaGeneralesAsync();
        Task<TablaGenerales> GetTablaGeneralesByIdAsync(int id);
        Task<int> AddTablaGeneralesAsync(TablaGenerales tablaGenerales);
        Task<bool> UpdateTablaGeneralesAsync(TablaGenerales tablaGenerales);
        Task<bool> DeleteTablaGeneralesAsync(int id, string usuario);
        Task<List<TablaGeneralesCombo>> GetAllTablaGeneralesByTipoGeneralAsync(string tipoGeneral);
        Task<List<Ubigeo>> GetAllUbigeosAsync();
    }
}
