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
    public interface IRumaService
    {
        Task<List<Ruma>> ListarRumasAsync();
        Task<Ruma> ObtenerRumaPorIdAsync(int id);
        Task<int> CrearRumaAsync(RumaNuevaRequest request);
        Task<bool> ActualizarRumaAsync(Ruma ruma);
        Task<bool> EliminarRumaAsync(int id, string usuario);
        Task<List<RumaEstado>> ObtenerEstadosPorRumaAsync(int idRuma);
        Task<bool> InsertarEstadoRumaAsync(RumaEstado estado);
        Task<bool> ActualizarEstadoRumaAsync(RumaEstado estado);
        Task<bool> EliminarEstadoRumaAsync(int id, string usuario);
        Task<List<RumaEstadoProceso>> ObtenerProcesosPorRumaAsync(int idRuma);
        Task<bool> InsertarEstadoProcesoAsync(RumaEstadoProceso proceso);
        Task<bool> ActualizarProcesoRumaAsync(RumaEstadoProceso proceso);
        Task<bool> EliminarProcesoRumaAsync(int id, string usuario);
        Task<List<RumaDto>> ListarRumasByClaseAsync(string codigoClase);
        Task<string> ObtenerCodigoRumaAsync(int idEmpresa, int idProducto);
        Task<int> ActualizarRumaYEstadoAsync(RumaNuevaRequest request);
        Task<int> ActualizarRumaInsertarEstadoAsync(RumaNuevaRequest request);
        Task<List<RumaTicketDto>> ListarTicketAsync(int idRumaEstado);
        Task InsertarTicketRumasAsync(List<RumaTicket> lista);
        Task EliminarTicketRumaAsync(int idTicketRuma);
        Task<RumaEstado> ObtenerEstadoPorIdAsync(int id);
    }
}
