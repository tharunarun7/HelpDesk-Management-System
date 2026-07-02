using HelpDeskApi.Models;

namespace HelpDeskApi.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<List<Ticket>> GetTicketsByCreatedByAsync(int createdBy);

        Task<Ticket?> GetTicketByIdAsync(int id);

        Task AddTicketAsync(Ticket ticket);

        Task UpdateTicketAsync(Ticket ticket);

        Task DeleteTicketAsync(int id);
    }
}
