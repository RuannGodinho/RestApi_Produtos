using Microsoft.EntityFrameworkCore;
using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Infraestruture.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Infraestruture.Data
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new EstoqueMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Produto> Produto { get; set; }
    }
}
