using ContasApp.Data.Configuration;
using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL8010.site4now.net;Initial Catalog=db_aaa90d_bdcontasapp;User Id=db_aaa90d_bdcontasapp_admin;Password=contas@BD@2023");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new ContaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }

        public DbSet<Categoria>? Categoria { get; set; }
        public DbSet<Usuario>? Usuario { get; set; }
        public DbSet<Conta>? Conta { get; set; }
    }
}
