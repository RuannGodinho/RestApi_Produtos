using RestApi_Produtos.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Business.Repositories
{
    public interface IEstoqueRepository
    {
        void Adicionar(Estoque estoque);
        void Commit();
        IList<Estoque> ObterPorProduto(int codigoProduto);
    }
}
