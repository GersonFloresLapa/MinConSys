using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketDto>> ListarTicketsAsync()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }

        public async Task<Ticket> ObtenerPorIdAsync(int id)
        {
            return await _ticketRepository.GetTicketByIdAsync(id);
        }

        public async Task<int> CrearTicketAsync(Ticket ticket)
        {
            ticket.FechaCreacion = DateTime.Now;
            ticket.Estado = 'A';
            return await _ticketRepository.AddTicketAsync(ticket);
        }

        public async Task<bool> ActualizarTicketAsync(Ticket ticket)
        {
            ticket.FechaModificacion = DateTime.Now;
            return await _ticketRepository.UpdateTicketAsync(ticket);
        }

        public async Task<bool> EliminarTicketAsync(int id, string usuario)
        {
            return await _ticketRepository.DeleteTicketAsync(id, usuario);
        }

        public async Task<List<TicketRumaDto>> ListarTickestParaRumaAsync(int idProveedor, int idClase, int idProducto)
        {
            return await _ticketRepository.GetAllTicketsForRumaAsync(idProveedor, idClase, idProducto);
        }
    }
}
