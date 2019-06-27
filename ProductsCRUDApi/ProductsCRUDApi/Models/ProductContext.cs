using Microsoft.EntityFrameworkCore;
using ProductsCRUDApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Models
{
    public class ProductContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KORMAN\\SQLEXPRESS; Database=Northwind; Trusted_Connection=Yes;");
        }
        public DbSet<Product> Products { get; set; }
    }
}
