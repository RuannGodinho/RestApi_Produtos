using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Infraestruture.Data.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly ProdutoDbContext _contexto;

        public EstoqueRepository(ProdutoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Estoque estoque)
        {
            _contexto.Estoque.Add(estoque);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public IList<Estoque> ObterPorProduto(int nome_Produto)
        {
            return _contexto.Estoque.ToList();
        }
    }
}
