using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Business.Repositories;
using RestApi_Produtos.Models.Produtos;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestApi_Produtos.Controllers
{
    [Route("api/v1/produtos")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        /// <summary>
        /// Este serviço permite cadastrar produtos para o usuario autenticado.
        /// </summary>
        /// <returns>Retorna status 201 e dados do produto do usuario</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um produto")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]

        public async Task<IActionResult> Post(ProdutoViewModelInput produtoViewModelInput)
        {
            Produto produto = new Produto();
            produto.Nome = produtoViewModelInput.Nome;
            produto.Descricao = produtoViewModelInput.Descricao;
            produto.Valor = produtoViewModelInput.Valor;
            _produtoRepository.Adicionar(produto);
            _produtoRepository.Commit();
            var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", produtoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os produtos ativos.
        /// </summary>
        /// <returns>Retorna status ok e dados dos produtos</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter os produtos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Get()
        {
            var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var produtos = _produtoRepository.ObterPorUsuario(CodigoUsuario)
                .Select(s => new ProdutoViewModelOutput()
                {
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Valor = s.Valor,
                });

            return Ok(produtos);
        }

        /// <summary>
        /// Este serviço permite atualizar um produto ativo.
        /// </summary>
        /// <returns>Retorna status ok e dados dos produtos</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao atualizar o produtos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutos(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _produtoRepository.AtualizarProdutos(produto);

            try
            {
                _produtoRepository.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();



        }

        private bool ProdutoExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
