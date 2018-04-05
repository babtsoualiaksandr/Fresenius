using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fresenius.Models;
using Fresenius.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fresenius.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Sparepart> Spareparts { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceSparepart> InvoiceSpareparts { get; set; }






        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);




            builder.Entity<Invoice>().ToTable("Invoice");
            builder.Entity<InvoiceSparepart>().ToTable("InvoiceSparepart");
            builder.Entity<Sparepart>().ToTable("Sparepart");

        }

        public DbSet<Fresenius.Data.IdentityCard> IdentityCard { get; set; }
    }
}
