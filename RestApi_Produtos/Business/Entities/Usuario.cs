using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Business.Entities
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
