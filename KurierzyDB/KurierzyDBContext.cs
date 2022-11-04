﻿using static KurierzyDB.KurierzyDBContext;
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
             

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Person>()
                    .HasKey("Id");

                modelBuilder.Entity<Deliverer>()
                     .HasKey("PersonId");


           
        }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=FirmaKurierska; Integrated Security=True";
                optionsBuilder.UseSqlServer(connString);
            }
        }
}
