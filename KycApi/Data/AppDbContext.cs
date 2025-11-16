using Microsoft.EntityFrameworkCore;
using KycApi.Models;

namespace KycApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<KycApplication> KycApplications { get; set; } = null!;

    }
}
