
using HelpDeskApi.Data;
using HelpDeskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskApi.Repositories
{
    public class TicketMessageRepository : ITicketMessageRepository
    {
        private readonly HelpDeskDbContext _context;

        public TicketMessageRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketMessage>> GetMessagesAsync(int ticketId)
        {
            return await _context.TicketMessages
                .Where(x => x.TicketId == ticketId)
                .OrderBy(x => x.SentAt)
                .ToListAsync();
        }

        public async Task AddMessageAsync(TicketMessage message)
        {
            Console.WriteLine("===== REPOSITORY HIT =====");

            _context.TicketMessages.Add(message);

            await _context.SaveChangesAsync();
        }
    }
}
