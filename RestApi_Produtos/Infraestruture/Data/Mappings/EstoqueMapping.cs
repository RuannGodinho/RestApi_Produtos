using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApi_Produtos.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Infraestruture.Data.Mappings
{
    public class EstoqueMapping : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("TB_Estoque");
            builder.HasKey(p => p.Codigo_Estoque);
            builder.Property(p => p.Codigo_Estoque).ValueGeneratedOnAdd();
            builder.Property(p => p.Movimentacao);
            builder.Property(p => p.Nome_Produto);
            builder.Property(p => p.Quantidade);
            builder.Property(p => p.Data);
        }
    }
}
