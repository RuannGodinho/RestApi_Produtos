﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_Produtos.Models.Estoque
{
    public class EstoqueViewModelOutput
    {
        public int Codigo_Estoque { get; set; }
        public string Movimentacao { get; set; }
        public string Nome_Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
    }
}
