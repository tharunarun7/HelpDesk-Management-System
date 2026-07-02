using HelpDeskApi.Models;
using HelpDeskApi.Repositories;

namespace HelpDeskApi.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }
        public async Task<List<Ticket>> GetTicketsByCreatedByAsync(int createdBy)
        {
            return await _ticketRepository.GetTicketsByCreatedByAsync(createdBy);
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _ticketRepository.GetTicketByIdAsync(id);
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _ticketRepository.AddTicketAsync(ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await _ticketRepository.UpdateTicketAsync(ticket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _ticketRepository.DeleteTicketAsync(id);
        }
    }
}
