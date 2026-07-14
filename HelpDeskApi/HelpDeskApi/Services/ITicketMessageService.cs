using HelpDeskApi.Models;

namespace HelpDeskApi.Services
{
    public interface ITicketMessageService
    {
        Task<List<TicketMessage>> GetMessagesAsync(int ticketId);

        Task AddMessageAsync(TicketMessage message);
    }
}