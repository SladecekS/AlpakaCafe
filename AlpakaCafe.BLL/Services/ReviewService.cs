using AlpakaCafe.BLL.Interfaces;
using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AlpakaCafe.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AlpakaCafeDbContext _context;

        public ReviewService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        // 1. Dodavanje recenzije
        public async Task AddReviewAsync(
            int userId,
            int reservationId,
            int rating,
            string comment)
        {
            // Provera ocene
            if (rating < 1 || rating > 5)
                throw new Exception("Ocena mora biti između 1 i 5.");

            // Pronalazimo rezervaciju
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r =>
                    r.ReservationId == reservationId);

            if (reservation == null)
                throw new Exception("Rezervacija ne postoji.");

            // Provera da li rezervacija pripada korisniku
            if (reservation.UserId != userId)
                throw new Exception(
                    "Ne možete oceniti tuđu rezervaciju.");

            // Provera da li je obilazak završen
            var timeSlot = await _context.TimeSlots
                .FirstOrDefaultAsync(t =>
                    t.TimeSlotId == reservation.TimeSlotId);

            if (timeSlot == null)
                throw new Exception("Termin ne postoji.");

            if (timeSlot.EndDate > DateTime.Now)
                throw new Exception(
                    "Ne možete ostaviti recenziju pre završetka obilaska.");

            // Provera da li već postoji recenzija
            bool alreadyReviewed = await _context.Reviews
                .AnyAsync(r =>
                    r.ReservationId == reservationId);

            if (alreadyReviewed)
                throw new Exception(
                    "Za ovu rezervaciju već postoji recenzija.");

            // Kreiranje recenzije
            var review = new Review
            {
                UserId = userId,
                ReservationId = reservationId,
                Rating = rating,
                Comment = comment,
                Created = DateTime.Now
            };

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();
        }

        // 2. Vraćanje svih recenzija
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Reservation)
                .ToListAsync();
        }

        // 3. Vraćanje recenzije po ID-u
        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(r =>
                    r.ReviewId == id);
        }
    }
}
