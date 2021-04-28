using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
                Name = "Eventee",
                NormalizedName = "EVENTEE"
            }
            );
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "EventPlaner",
                NormalizedName = "EVENTPLANER"
            }
            );
        }

    }
}
