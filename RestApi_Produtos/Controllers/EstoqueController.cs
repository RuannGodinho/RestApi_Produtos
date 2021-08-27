using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Business.Repositories;
using RestApi_Produtos.Models.Estoque;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestApi_Produtos.Controllers
{
    [Route("api/v1/estoque")]
    [ApiController]
    [Authorize]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueController(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        /// <summary>
        /// Este serviço permite cadastrar produtos no estoque para o usuario autenticado.
        /// </summary>
        /// <returns>Retorna status 201 e dados do produto do usuario</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um produto no estoque")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]

        public async Task<IActionResult> Post(EstoqueViewModelInput estoqueViewModelInput)
        {
            Estoque estoque = new Estoque();
            estoque.Codigo_Estoque = estoqueViewModelInput.Codigo_Estoque;
            estoque.Movimentacao = estoqueViewModelInput.Movimentacao;
            estoque.Nome_Produto = estoqueViewModelInput.Nome_Produto;
            estoque.Quantidade = estoqueViewModelInput.Quantidade;
            estoque.Data = estoqueViewModelInput.Data;
            _estoqueRepository.Adicionar(estoque);
            _estoqueRepository.Commit();
            var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", estoqueViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todo o estoque.
        /// </summary>
        /// <returns>Retorna status ok e dados do estoque</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter o estoque")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Get()
        {
            var CodigoProduto = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var estoque = _estoqueRepository.ObterPorProduto(CodigoProduto)
                .Select(s => new EstoqueViewModelOutput()
                {
                    Codigo_Estoque = s.Codigo_Estoque,
                    Movimentacao = s.Movimentacao,
                    Quantidade = s.Quantidade,
                    Nome_Produto = s.Nome_Produto,
                    Data = s.Data

                });

            return Ok(estoque);
        }
    }
}
