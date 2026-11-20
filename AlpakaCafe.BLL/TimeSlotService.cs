using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlpakaCafe.BLL
{
    public class TimeSlotService
    {
        private AlpakaCafeDbContext _context;

        public TimeSlotService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeSlot>> GetAll()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public TimeSlot Get(int? id)
        {
            return _context.TimeSlots.FirstOrDefault(c => c.TimeSlotId == id);
        }

        public async Task Create(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var timeSlot = Get(id);

            if (timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
            }
        }
    }
}
