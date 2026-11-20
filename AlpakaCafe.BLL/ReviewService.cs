using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlpakaCafe.BLL
{
    public class ReviewService
    {
        private AlpakaCafeDbContext _context;

        public ReviewService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }

        public Review Get(int? id)
        {
            return _context.Reviews.FirstOrDefault(c => c.ReviewId == id);
        }

        public async Task Create(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var review = Get(id);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
