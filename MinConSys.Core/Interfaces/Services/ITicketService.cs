using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ITicketService
    {
        Task<List<TicketDto>> ListarTicketsAsync();
        Task<Ticket> ObtenerPorIdAsync(int id);
        Task<int> CrearTicketAsync(Ticket ticket);
        Task<bool> ActualizarTicketAsync(Ticket ticket);
        Task<bool> EliminarTicketAsync(int id, string usuario);
    }
}
