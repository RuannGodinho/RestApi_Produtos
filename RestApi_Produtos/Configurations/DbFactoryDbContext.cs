using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RestApi_Produtos.Infraestruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<ProdutoDbContext>
    {
        public ProdutoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProdutoDbContext>();
            optionsBuilder.UseSqlServer(connectionString: @"Server=(localdb)\mssqllocaldb;Database=Estoque;Integrated Security=True");
            ProdutoDbContext contexto = new ProdutoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
