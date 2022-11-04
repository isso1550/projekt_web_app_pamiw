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

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Person>()
                    .HasKey("Id");
                modelBuilder.Entity<Person>()
                    .Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=FirmaKurierska; Integrated Security=True";
                optionsBuilder.UseSqlServer(connString);
            }
        }
}
