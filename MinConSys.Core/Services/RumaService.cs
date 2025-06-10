using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class RumaService : IRumaService
    {
        private readonly IRumaRepository _registroRumaRepository;

        public RumaService(IRumaRepository registroRumaRepository)
        {
            _registroRumaRepository = registroRumaRepository;
        }
        public async Task<List<Ruma>> ListarRumasAsync()
        {
            return await _registroRumaRepository.GetAllRumasAsync();
        }
        public async Task<Ruma> ObtenerRumaPorIdAsync(int id)
        {
            return await _registroRumaRepository.GetRumaByIdAsync(id);
        }
        public async Task<int> CrearRumaAsync(RumaNuevaRequest request)
        {
            request.Ruma.Estado = "A";
            request.Estado.Estado = "A";
            request.Procesos.Estado = "A";
            return await _registroRumaRepository.AddRumaCompletaAsync(request);
        }
        public async Task<bool> ActualizarRumaAsync(Ruma ruma)
        {
            ruma.FechaModificacion = DateTime.Now;
            return await _registroRumaRepository.UpdateRumaAsync(ruma);
        }
        public async Task<bool> EliminarRumaAsync(int id, string usuario)
        {
            return await _registroRumaRepository.DeleteRumaAsync(id, usuario);
        }
        public async Task<List<RumaEstado>> ObtenerEstadosPorRumaAsync(int idRuma)
        {
            return await _registroRumaRepository.GetEstadosByRumaAsync(idRuma);
        }
        public async Task<bool> InsertarEstadoRumaAsync(RumaEstado estado)
        {
            return await _registroRumaRepository.AddEstadoRumaAsync(estado);
        }
        public async Task<bool> ActualizarEstadoRumaAsync(RumaEstado estado)
        {
            estado.FechaModificacion = DateTime.Now;
            return await _registroRumaRepository.UpdateEstadoRumaAsync(estado);
        }
        public async Task<bool> EliminarEstadoRumaAsync(int id, string usuario)
        {
            return await _registroRumaRepository.DeleteEstadoRumaAsync(id, usuario);
        }
        public async Task<List<RumaEstadoProceso>> ObtenerProcesosPorRumaAsync(int idRuma)
        {
            return await _registroRumaRepository.GetEstadosProcesoByRumaAsync(idRuma);
        }
        public async Task<bool> InsertarEstadoProcesoAsync(RumaEstadoProceso proceso)
        {
            return await _registroRumaRepository.AddEstadoProcesoAsync(proceso);
        }
        public async Task<bool> ActualizarProcesoRumaAsync(RumaEstadoProceso proceso)
        {
            proceso.FechaModificacion = DateTime.Now;
            return await _registroRumaRepository.UpdateProcesoRumaAsync(proceso);
        }
        public async Task<bool> EliminarProcesoRumaAsync(int id, string usuario)
        {
            return await _registroRumaRepository.DeleteProcesoRumaAsync(id, usuario);
        }
        public async Task<List<RumaDto>> ListarRumasByClaseAsync(string codigoClase)
        {
            return await _registroRumaRepository.GetAllRumasByClaseAsync(codigoClase);
        }
        public async Task<string> ObtenerCodigoRumaAsync(int idEmpresa, int idProducto)
        {
            return await _registroRumaRepository.GetCodigoRuma(idEmpresa, idProducto);
        }

        public async Task<int> ActualizarRumaYEstadoAsync(RumaNuevaRequest request)
        {
            return await _registroRumaRepository.UpdateRumaYEstadoAsync(request);
        }

        public async Task<int> ActualizarRumaInsertarEstadoAsync(RumaNuevaRequest request)
        {
            request.Estado.Estado = "A";
            request.Procesos.Estado = "A";
            return await _registroRumaRepository.UpdateRumaAddEstadoAsync(request);
        }
        public async Task<List<RumaTicketDto>> ListarTicketAsync(int idRumaEstado)
        {
            return await _registroRumaRepository.GetAllRumasTicketAsync(idRumaEstado);
        }

        public async Task InsertarTicketRumasAsync(List<RumaTicket> lista)
        {
             await _registroRumaRepository.AddTicketRumasAsync(lista);
        }

        public async Task EliminarTicketRumaAsync(int idRumaTicket)
        {
            await _registroRumaRepository.DeleteTicketRumaAsync(idRumaTicket);
        }
        public async Task<RumaEstado> ObtenerEstadoPorIdAsync(int id)
        {
            return await _registroRumaRepository.GetEstadosByIdAsync(id);
        }
    }
}
