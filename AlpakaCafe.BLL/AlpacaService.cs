using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlpakaCafe.BLL
{
    public class AlpacaService
    {
        private AlpakaCafeDbContext _context;

        public AlpacaService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Alpaca>> GetAll()
        {
            return await _context.Alpacas.ToListAsync();
        }

        public Alpaca Get(int? id) 
        { 
            return _context.Alpacas.FirstOrDefault(c => c.AlpacaId == id);
        }


    }
}
