using HelpDeskApi.Models;

namespace HelpDeskApi.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<List<Ticket>> GetTicketsByCreatedByAsync(int createdBy);

        Task<Ticket?> GetTicketByIdAsync(int id);

        Task AddTicketAsync(Ticket ticket);

        Task UpdateTicketAsync(Ticket ticket);

        Task DeleteTicketAsync(int id);
    }
}
