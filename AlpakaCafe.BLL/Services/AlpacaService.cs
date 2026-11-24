using AlpakaCafe.BLL.Interfaces;
using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlpakaCafe.BLL.Services
{
    public class AlpacaService : IAlpacaService
    {
        private readonly AlpakaCafeDbContext _context;

        public AlpacaService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        // 1. Dodavanje alpake
        public async Task AddAlpacaAsync(Alpaca alpaca)
        {
            if (alpaca == null)
                throw new Exception("Podaci o alpaki nisu prosleđeni.");

            _context.Alpacas.Add(alpaca);

            await _context.SaveChangesAsync();
        }

        // 2. Vraćanje svih alpaka
        public async Task<IEnumerable<Alpaca>> GetAllAlpacasAsync()
        {
            return await _context.Alpacas.ToListAsync();
        }

        // 3. Vraćanje samo aktivnih alpaka
        public async Task<IEnumerable<Alpaca>> GetActiveAlpacasAsync()
        {
            return await _context.Alpacas
                .Where(a => a.IsActive)
                .ToListAsync();
        }

        // 4. Pronalazak alpake po ID-u
        public async Task<Alpaca?> GetAlpacaByIdAsync(int id)
        {
            return await _context.Alpacas
                .FirstOrDefaultAsync(a => a.AlpacaId == id);
        }

        // 5. Izmena alpake
        public async Task UpdateAlpacaAsync(Alpaca alpaca)
        {
            var existingAlpaca = await _context.Alpacas
                .FindAsync(alpaca.AlpacaId);

            if (existingAlpaca == null)
                throw new Exception("Alpaka ne postoji.");

            existingAlpaca.Name = alpaca.Name;
            existingAlpaca.Description = alpaca.Description;
            existingAlpaca.BirthDate = alpaca.BirthDate;
            existingAlpaca.ImageUrl = alpaca.ImageUrl;
            existingAlpaca.IsActive = alpaca.IsActive;

            await _context.SaveChangesAsync();
        }

        // 6. Brisanje alpake
        public async Task DeleteAlpacaAsync(int id)
        {
            var alpaca = await _context.Alpacas.FindAsync(id);

            if (alpaca == null)
                throw new Exception("Alpaka ne postoji.");

            alpaca.IsActive = false;

            await _context.SaveChangesAsync();
        }
    }
}
