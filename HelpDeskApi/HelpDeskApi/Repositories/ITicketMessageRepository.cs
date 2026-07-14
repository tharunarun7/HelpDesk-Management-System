using HelpDeskApi.Models;

namespace HelpDeskApi.Repositories
{
    public interface ITicketMessageRepository
    {
        Task<List<TicketMessage>> GetMessagesAsync(int ticketId);

        Task AddMessageAsync(TicketMessage message);
    }
}

