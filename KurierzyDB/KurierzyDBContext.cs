using static KurierzyDB.KurierzyDBContext;
using KurierzyDomain;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace KurierzyDB
{
        public class KurierzyDBContext : DbContext
        {
            public DbSet<Person> Persons { get; set; }
            public DbSet<Deliverer> Deliverers { get; set; }

            public DbSet<Role> Roles { get; set; }

            public DbSet<Office> Offices { get; set; }
            public DbSet<OfficeWorker> OfficeWorkers { get; set; }
            public DbSet<Package> Packages { get; set; }
            public DbSet<Van> Vans { get; set; }
             

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Person>()
                    .HasKey("Id");
                modelBuilder.Entity<Person>()
                    .Property(p => p.Email)
                    .IsRequired();
            modelBuilder.Entity<Person>()
                .HasIndex(p => new { p.Id, p.RoleId });

                modelBuilder.Entity<Deliverer>()
                     .HasKey("PersonId");

            modelBuilder.Entity<Office>()
                .HasKey(o => new {o.Id});


            modelBuilder.Entity<OfficeWorker>()
                .HasKey("PersonId");
            modelBuilder.Entity<OfficeWorker>()
                .HasOne(ow => ow.Office).WithOne(of => of.Manager);

            modelBuilder.Entity<Package>()
                 .HasKey("Id");
               
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Deliverer)
                .WithMany(d => d.Packages)
                .IsRequired()
                .HasForeignKey(p => p.DelivererId);

            modelBuilder.Entity<Van>()
                .HasMany(v => v.Drivers)
                .WithMany(d => d.Vans);
        }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=FirmaKurierska; Integrated Security=True";
                optionsBuilder.UseSqlServer(connString);
            }
        }
}
