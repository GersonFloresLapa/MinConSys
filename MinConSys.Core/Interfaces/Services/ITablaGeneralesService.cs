using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ITablaGeneralesService
    {
        Task<List<TablaGeneralesDto>> ListarTablaGeneralesAsync();
        Task<TablaGenerales> ObtenerPorIdAsync(int id);
        Task<int> CrearTablaGeneralesAsync(TablaGenerales request);
        Task<bool> ActualizarTablaGeneralesAsync(TablaGenerales request);
        Task<bool> EliminarTablaGeneralesAsync(int id, string nombreUsuario);
        Task<List<TablaGeneralesCombo>> ObtenerPorTipoGeneralAsync(string tipoGeneral);
        Task<List<Ubigeo>> ObtenerUbigeosAsync();
    }
}
