using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Infraestruture.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProdutoDbContext _contexto;

        public UsuarioRepository(ProdutoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public Usuario ObterUsuario(string login)
        {
            return _contexto.Usuario.FirstOrDefault(u => u.Login == login);
        }
    }
}
