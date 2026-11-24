using AlpakaCafe.BLL.Interfaces;
using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlpakaCafe.BLL.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly AlpakaCafeDbContext _context;

        public TimeSlotService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        // 1. Kreiranje novog termina
        public async Task CreateTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();
        }

        // 2. Vraćanje svih termina
        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _context.TimeSlots
                .Include(t => t.Reservations)
                .ToListAsync();
        }

        // 3. Vraćanje slobodnih termina
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync()
        {
            var currentDate = DateTime.Now;

            var timeSlots = await _context.TimeSlots
                .Include(t => t.Reservations)
                .Where(t =>
                    t.IsActive &&
                    t.StartDate > currentDate)
                .ToListAsync();

            return timeSlots
                .Where(t =>
                {
                    int reservedPlaces = t.Reservations
                        .Where(r => r.Status == "Confirmed")
                        .Sum(r => r.NumberOfPeople);

                    int availablePlaces = t.Capacity - reservedPlaces;

                    return availablePlaces > 0;
                })
                .ToList();
        }

        // 4. Izmena postojećeg termina
        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            var existingTimeSlot = await _context.TimeSlots
                .FindAsync(timeSlot.TimeSlotId);

            if (existingTimeSlot == null)
                throw new Exception("Termin ne postoji.");

            existingTimeSlot.StartDate = timeSlot.StartDate;
            existingTimeSlot.EndDate = timeSlot.EndDate;
            existingTimeSlot.Capacity = timeSlot.Capacity;
            existingTimeSlot.IsActive = timeSlot.IsActive;

            await _context.SaveChangesAsync();
        }

        // 5. Brisanje termina
        public async Task DeleteTimeSlotAsync(int id)
        {
            var timeSlot = await _context.TimeSlots
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.TimeSlotId == id);

            if (timeSlot == null)
                throw new Exception("Termin ne postoji.");

            if (timeSlot.Reservations.Any())
                throw new Exception("Termin ima rezervacije i ne može biti obrisan.");

            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();
        }

        // 6. Deaktivacija termina
        public async Task DeactivateTimeSlotAsync(int id)
        {
            var timeSlot = await _context.TimeSlots
                .FindAsync(id);

            if (timeSlot == null)
                throw new Exception("Termin ne postoji.");

            timeSlot.IsActive = false;

            await _context.SaveChangesAsync();
        }
    }
}
