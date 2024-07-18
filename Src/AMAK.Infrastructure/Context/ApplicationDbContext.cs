using AMAK.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Infrastructure.Context {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) {
        }

        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        }

    }
}