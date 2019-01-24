using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace DAL
{
    public class ApiContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Midia> Midias { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region unique key
            modelBuilder.Entity<Cliente>().HasIndex(u => u.CPF).IsUnique();
            modelBuilder.Entity<Midia>().HasIndex(u => u.Descricao).IsUnique();
            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
