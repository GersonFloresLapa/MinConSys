using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ILocalidadService
    {
        Task<List<LocalidadDto>> ListarLocalidadesAsync();
        Task<Localidad> ObtenerPorIdAsync(int id);
        Task<int> CrearLocalidadAsync(Localidad request);
        Task<bool> ActualizarLocalidadAsync(Localidad request);
        Task<bool> EliminarLocalidadAsync(int id, string nombreUsuario);
        Task<List<ComboItem>> ListarLocalidadesTiposAsync(string tipo);
    }
}
