using Microsoft.EntityFrameworkCore;
using CompoundingAPI.Models;

namespace CompoundingAPI.Data
{
    public class CompoundInterestAppDbContext : DbContext
    {
        public DbSet<CompoundInterestStore> Records { get; set; }

        public CompoundInterestAppDbContext(DbContextOptions<CompoundInterestAppDbContext> options) : base(options) { }
    }
}
