using Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CustomerItem> CustomerItem { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(x =>
            {
                x.ToTable("Customers");
            });

            modelBuilder.Entity<Item>(x =>
            {
                x.ToTable("Items");
            });

            modelBuilder.Entity<CustomerItem>(x =>
            {
                x.ToTable("CustomersItems");
            });

            modelBuilder.Entity<Category>(x =>
            {
                x.ToTable("Categories");
            });

        }


    }
}
