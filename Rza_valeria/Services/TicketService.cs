using Microsoft.EntityFrameworkCore;
using Rza_valeria.Models;


namespace Rza_valeria.Services
{
    public class TicketService
    {
        private readonly RzaContext _context;
        public TicketService(RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
        public async Task AddTicketAsync(Ticket newTicket)
        {
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();
        }
    }
}
