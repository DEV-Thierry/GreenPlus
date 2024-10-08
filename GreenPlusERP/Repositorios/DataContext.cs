using GreenPlusERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=GreenPlus;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;");
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<userModel> Users { get; set; }
    }
}
