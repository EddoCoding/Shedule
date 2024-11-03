using Microsoft.EntityFrameworkCore;
using Shedule.Models;

namespace Shedule.Common
{
    public class DataContext : DbContext
    {
        public DbSet<Item> SheduleDayOfWeek { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Call> Calls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={Path.Combine(FileSystem.AppDataDirectory, "myShedule.db")}");
    }
}