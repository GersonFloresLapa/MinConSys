using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ITicketRepository
    {
        Task<List<TicketDto>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<int> AddTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int id, string usuario);
        Task<List<TicketRumaDto>> GetAllTicketsForRumaAsync(int idProveedor, int idClase, int idProducto);
    }
}
