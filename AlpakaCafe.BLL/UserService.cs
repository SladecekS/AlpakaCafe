using AlpakaCafe.DAL;
using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlpakaCafe.BLL
{
    public class UserService
    {
        private AlpakaCafeDbContext _context;

        public UserService(AlpakaCafeDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public User Get(int? id)
        {
            return _context.Users.FirstOrDefault(c => c.UserId == id);
        }
    }
}
