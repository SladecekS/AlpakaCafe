using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

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


        public async Task Create(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        // Izmena postojeće alpake
        public async Task Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        // Brisanje alpake
        public async Task Delete(int id)
        {
            var reservation = Get(id);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
