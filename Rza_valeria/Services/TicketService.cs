using Rza_valeria.Models;
using Microsoft.EntityFrameworkCore;

namespace Rza_valeria.Services

{
    public class TicketService
    {
        private readonly RzaContext _context;
        public TicketService(RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Attraction>> GetAttractionsAsync()
        {
            return await _context.Attractions.ToListAsync();
        }
    }
}
