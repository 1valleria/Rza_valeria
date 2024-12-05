using Rza_valeria.Models;
using Microsoft.EntityFrameworkCore;

namespace Rza_valeria.Services

{
    public class AttractionService
    {
        private readonly TlS2301890RzaContext _context;
        public AttractionService(TlS2301890RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Attraction>> GetAttractionsAsync()
        {
            return await _context.Attractions.ToListAsync();
        }
    }
}
