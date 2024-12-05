using Microsoft.EntityFrameworkCore;
using Rza_valeria.Models;
using Rza_valeria.Models;

namespace Rza_valeria.Services
{
    public class TicketbookingService
    {
        private readonly TlS2301890RzaContext _context;
        public TicketbookingService(TlS2301890RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Ticketbooking>> GetTicketbookingsAsync()
        {
            return await _context.Ticketbookings.ToListAsync();
        }
        public async Task AddTicketbookingAsync(Ticketbooking newTicketbooking)
        {
            await _context.Ticketbookings.AddAsync(newTicketbooking);
            await _context.SaveChangesAsync();
        }
    }
}
