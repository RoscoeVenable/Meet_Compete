using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Meet_and_Copmete_Capstone.Models;

namespace Meet_and_Copmete_Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Id = "41612494-a3c5-4c42-a702-c98991751260",
                Name = "Eventee",
                NormalizedName = "EVENTEE",
                ConcurrencyStamp = "94dc8e47-cdbb-4161-8973-945fbda3ebff"
            }
            );
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Id = "dd17ccfa-7f18-4473-a780-35c1fa708cdb",
                Name = "EventPlaner",
                NormalizedName = "EVENTPLANER",
                ConcurrencyStamp = "dd841d8d - 6949 - 4e5d - 83bf - 38e4ea6a8158"
            }
            );

        }
        public DbSet<Meet_and_Copmete_Capstone.Models.Eventee> Eventee { get; set; }
        public DbSet<Meet_and_Copmete_Capstone.Models.Event> Event { get; set; }
        public DbSet<Meet_and_Copmete_Capstone.Models.EventPlaner> EventPlaner { get; set; }
        public DbSet<Meet_and_Copmete_Capstone.Models.Invite> Invite { get; set; }

    }
}
