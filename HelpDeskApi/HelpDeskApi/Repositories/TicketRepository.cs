using HelpDeskApi.Data;
using HelpDeskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskApi.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HelpDeskDbContext _context;

        public TicketRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            var tickets = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                ticket.CreatedByUsername = ticket.CreatedByUser?.Username;
            }

            return tickets;
        }
        public async Task<List<Ticket>> GetTicketsByCreatedByAsync(int createdBy)
        {
            var tickets = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Where(t => t.CreatedBy == createdBy)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                ticket.CreatedByUsername = ticket.CreatedByUser?.Username;
            }

            return tickets;
        }
        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket != null)
            {
                ticket.CreatedByUsername = ticket.CreatedByUser?.Username;
            }

            return ticket;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            var existingTicket = await _context.Tickets.FindAsync(ticket.Id);

            if (existingTicket == null)
                return;

            existingTicket.Title = ticket.Title;
            existingTicket.Description = ticket.Description;
            existingTicket.Status = ticket.Status;
            existingTicket.Priority = ticket.Priority;
            existingTicket.AssignedTo = ticket.AssignedTo;
            existingTicket.ScreenshotPath = ticket.ScreenshotPath;

            // Admin response
            existingTicket.AdminResponse = ticket.AdminResponse;

            // Save response date automatically
            if (!string.IsNullOrWhiteSpace(ticket.AdminResponse))
            {
                existingTicket.ResponseDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
