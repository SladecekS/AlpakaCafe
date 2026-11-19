using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlpakaCafe.BLL
{
    public class ReservationService
    {
        private AlpakaCafeDbContext _context;

        public ReservationService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _context.Reservations.ToListAsync();
        }

        public Reservation Get(int? id)
        {
            return _context.Reservations.FirstOrDefault(c => c.ReservationId == id);
        }
    }
}
