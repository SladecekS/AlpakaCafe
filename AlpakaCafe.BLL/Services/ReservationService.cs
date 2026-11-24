using AlpakaCafe.BLL.Interfaces;
using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AlpakaCafe.BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AlpakaCafeDbContext _context;

        public ReservationService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        // 1. Kreiranje rezervacije
        public async Task<Reservation> CreateReservationAsync(
            int userId,
            int timeSlotId,
            int numberOfPeople)
        {
            if (numberOfPeople <= 0)
                throw new Exception("Broj osoba mora biti veći od nule.");

            // Transakcija štiti proveru kapaciteta i upis rezervacije.
            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    IsolationLevel.Serializable);

            // Pronalazimo termin i njegove rezervacije.
            var timeSlot = await _context.TimeSlots
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.TimeSlotId == timeSlotId);

            if (timeSlot == null)
                throw new Exception("Termin ne postoji.");

            // Provera da li je termin aktivan.
            if (!timeSlot.IsActive)
                throw new Exception("Termin nije aktivan.");

            // Provera da li je termin u budućnosti.
            if (timeSlot.StartDate <= DateTime.Now)
                throw new Exception("Nije moguće rezervisati termin koji je počeo.");

            // Provera da li korisnik već ima rezervaciju.
            bool alreadyReserved = timeSlot.Reservations
                .Any(r => r.UserId == userId &&
                          r.Status != "Cancelled");

            if (alreadyReserved)
                throw new Exception(
                    "Već imate rezervaciju za ovaj termin.");

            // Računanje zauzetih mesta.
            int reservedPlaces = timeSlot.Reservations
                .Where(r => r.Status == "Confirmed")
                .Sum(r => r.NumberOfPeople);

            int availablePlaces = timeSlot.Capacity - reservedPlaces;

            // Provera kapaciteta.
            if (numberOfPeople > availablePlaces)
                throw new Exception(
                    "Nema dovoljno slobodnih mesta za ovaj termin.");

            // Kreiranje rezervacije.
            var reservation = new Reservation
            {
                UserId = userId,
                TimeSlotId = timeSlotId,
                NumberOfPeople = numberOfPeople,
                ReservationDate = DateTime.Now,
                Status = "Confirmed"
            };

            _context.Reservations.Add(reservation);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return reservation;
        }

        // 2. Pronalazak rezervacije po ID-u
        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.TimeSlot)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        // 3. Prikaz rezervacija određenog korisnika
        public async Task<IEnumerable<Reservation>>
            GetUserReservationsAsync(int userId)
        {
            return await _context.Reservations
                .Include(r => r.TimeSlot)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        // 4. Prikaz svih rezervacija
        public async Task<IEnumerable<Reservation>>
            GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.TimeSlot)
                .ToListAsync();
        }

        // 5. Otkazivanje rezervacije
        public async Task CancelReservationAsync(
            int reservationId,
            int userId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r =>
                    r.ReservationId == reservationId);

            if (reservation == null)
                throw new Exception("Rezervacija ne postoji.");

            // Provera vlasništva nad rezervacijom.
            if (reservation.UserId != userId)
                throw new Exception(
                    "Nemate dozvolu da otkažete ovu rezervaciju.");

            if (reservation.Status == "Cancelled")
                throw new Exception(
                    "Rezervacija je već otkazana.");

            reservation.Status = "Cancelled";

            await _context.SaveChangesAsync();
        }
    }
}
