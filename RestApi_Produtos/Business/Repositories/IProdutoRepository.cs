using RestApi_Produtos.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Business.Repositories
{
    public interface IProdutoRepository
    {
        public void AtualizarProdutos(Produto produto);
        void DeletarProdutos(Produto id);
        void Adicionar(Produto produto);
        void Commit();
        IList<Produto> ObterPorUsuario(int codigoUsuario);
    }
}
