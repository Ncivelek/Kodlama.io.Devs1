﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(a =>
            {
                a.ToTable("Languages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<Technology>(x =>
            {
                x.ToTable("Technologies").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("Id");
                x.Property(p => p.LanguageId).HasColumnName("LanguageId");
                x.Property(p => p.Name).HasColumnName("Name");
                x.HasOne(p => p.Language);
            });


            Language[] languageEntitySeeds = { new(1, "C#"), new(2, "Java"), new(3, "Python") };
            modelBuilder.Entity<Language>().HasData(languageEntitySeeds);

            Technology[] technolgyEntitySeeds = { new(1, 1, "ASP.Net"), new(2, 1, "WPF"), new(3, 2, "Spring"), new(4, 2, "JSP") };
            modelBuilder.Entity<Technology>().HasData(technolgyEntitySeeds);
        }
    }
}
