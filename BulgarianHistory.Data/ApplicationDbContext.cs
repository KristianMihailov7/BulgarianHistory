using BulgarianHistory.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulgarianHistory.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Era> Eras { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<FamousPerson> FamousPeople { get; set; }
        public DbSet<Fact> Facts { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<EventCity> EventCities { get; set; }
        public DbSet<EventFamousPerson> EventFamousPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: Event <-> City
            modelBuilder.Entity<EventCity>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCities)
                .HasForeignKey(ec => ec.EventId)
                .OnDelete(DeleteBehavior.Cascade); // OK

            modelBuilder.Entity<EventCity>()
                .HasOne(ec => ec.City)
                .WithMany(c => c.EventCities)
                .HasForeignKey(ec => ec.CityId)
                .OnDelete(DeleteBehavior.Cascade); // OK

            // Many-to-Many: Event <-> FamousPerson
            modelBuilder.Entity<EventFamousPerson>()
                .HasOne(efp => efp.Event)
                .WithMany(e => e.EventFamousPeople)
                .HasForeignKey(efp => efp.EventId)
                .OnDelete(DeleteBehavior.Restrict); // FIX: NO CASCADE

            modelBuilder.Entity<EventFamousPerson>()
                .HasOne(efp => efp.FamousPerson)
                .WithMany(fp => fp.EventFamousPeople)
                .HasForeignKey(efp => efp.FamousPersonId)
                .OnDelete(DeleteBehavior.Restrict); // FIX: NO CASCADE

            // Уникални индекси
            modelBuilder.Entity<Era>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<FamousPerson>().HasIndex(fp => fp.Name).IsUnique();
        }
    }
}