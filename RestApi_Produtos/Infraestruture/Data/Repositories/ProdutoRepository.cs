using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Infraestruture.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext _contexto;

        public ProdutoRepository(ProdutoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Produto produto)
        {
            _contexto.Produto.Add(produto);
        }

        public void Commit()
        {
            _contexto.SaveChangesAsync();
        }

        public IList<Produto> ObterPorUsuario(int codigoUsuario)
        {
            return _contexto.Produto.ToList();
        }

        public void DeletarProdutos(Produto produto)
        {
             _contexto.Produto.Remove(produto);
        }

        public void AtualizarProdutos(Produto produto)
        {
            _contexto.Entry(produto).State = EntityState.Modified;
        }

    }
}
