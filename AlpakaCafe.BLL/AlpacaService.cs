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

        // Prikaz svih alpaka
        public async Task<List<Alpaca>> GetAll()
        {
            return await _context.Alpacas.ToListAsync();
        }

        // Pronalazak alpake po ID-u
        public Alpaca Get(int? id)
        {
            return _context.Alpacas.FirstOrDefault(c => c.AlpacaId == id);
        }

        // Dodavanje nove alpake
        public async Task Create(Alpaca alpaca)
        {
            _context.Alpacas.Add(alpaca);
            await _context.SaveChangesAsync();
        }

        // Izmena postojeće alpake
        public async Task Update(Alpaca alpaca)
        {
            _context.Alpacas.Update(alpaca);
            await _context.SaveChangesAsync();
        }

        // Brisanje alpake
        public async Task Delete(int id)
        {
            var alpaca =  Get(id);

            if (alpaca != null)
            {
                _context.Alpacas.Remove(alpaca);
                await _context.SaveChangesAsync();
            }
        }


    }

}
