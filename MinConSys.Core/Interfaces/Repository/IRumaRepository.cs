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
    public interface IRumaRepository
    {
        Task<int> AddRumaCompletaAsync(RumaNuevaRequest request);
        Task<Ruma> GetRumaByIdAsync(int id);
        Task<List<Ruma>> GetAllRumasAsync();
        Task<bool> UpdateRumaAsync(Ruma ruma);
        Task<bool> DeleteRumaAsync(int id, string usuario);
        Task<List<RumaEstado>> GetEstadosByRumaAsync(int idRuma);
        Task<bool> AddEstadoRumaAsync(RumaEstado estado);
        Task<bool> UpdateEstadoRumaAsync(RumaEstado estado);
        Task<bool> DeleteEstadoRumaAsync(int idEstado, string usuario);
        Task<List<RumaEstadoProceso>> GetEstadosProcesoByRumaAsync(int idRuma);
        Task<bool> AddEstadoProcesoAsync(RumaEstadoProceso proceso);
        Task<bool> UpdateProcesoRumaAsync(RumaEstadoProceso proceso);
        Task<bool> DeleteProcesoRumaAsync(int idProceso, string usuario);
        Task<List<RumaDto>> GetAllRumasByClaseAsync(string codigoClase);
        Task<string> GetCodigoRuma(int idEmpresa, int idProducto);
        Task<int> UpdateRumaYEstadoAsync(RumaNuevaRequest request);
        Task<int> UpdateRumaAddEstadoAsync(RumaNuevaRequest request);
        Task<List<RumaTicketDto>> GetAllRumasTicketAsync(int idRumaEstado);
        Task AddTicketRumasAsync(List<RumaTicket> lista);
        Task DeleteTicketRumaAsync(int idRumaTicket);
        Task<RumaEstado> GetEstadosByIdAsync(int id);
    }
}
