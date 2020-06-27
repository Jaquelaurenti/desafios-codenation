using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Models
{
    public class CodenationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Acceleration> Accelerations { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Challenge> Challenges { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Codenation;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(c => c.Candidates).WithOne(e => e.User).IsRequired();
            modelBuilder.Entity<User>().HasMany(c => c.Submissions).WithOne(e => e.User).IsRequired();
            modelBuilder.Entity<Challenge>().HasMany(c => c.Submissions).WithOne(e => e.Challenge).IsRequired();
            modelBuilder.Entity<Challenge>().HasMany(c => c.Accelerations).WithOne(e => e.Challenge).IsRequired();
            modelBuilder.Entity<Company>().HasMany(c => c.Candidates).WithOne(e => e.Company).IsRequired();
            modelBuilder.Entity<Acceleration>().HasMany(c => c.Candidates).WithOne(e => e.Acceleration).IsRequired();

            modelBuilder.Entity<Candidate>().HasKey(c => new { c.UserId, c.AccelerationId, c.CompanyId });
            modelBuilder.Entity<Submission>().HasKey(c => new { c.UserId, c.ChallengeId }); 
        }
    }
}