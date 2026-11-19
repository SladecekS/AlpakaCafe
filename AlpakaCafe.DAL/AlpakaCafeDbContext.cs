using AlpakaCafe.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AlpakaCafe.DAL
{
    public class AlpakaCafeDbContext : DbContext
    {

        public AlpakaCafeDbContext(DbContextOptions<AlpakaCafeDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Alpaca> Alpacas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alpaca>();
        }

    }
}
