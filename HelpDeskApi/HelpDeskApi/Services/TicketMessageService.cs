using HelpDeskApi.Models;
using HelpDeskApi.Repositories;

namespace HelpDeskApi.Services
{
    public class TicketMessageService : ITicketMessageService
    {
        private readonly ITicketMessageRepository _repository;

        public TicketMessageService(ITicketMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TicketMessage>> GetMessagesAsync(int ticketId)
        {
            return await _repository.GetMessagesAsync(ticketId);
        }

        public async Task AddMessageAsync(TicketMessage message)
        {
            await _repository.AddMessageAsync(message);
        }
    }
}
