using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestApi_Produtos.Business.Entities;
using RestApi_Produtos.Business.Repositories;
using RestApi_Produtos.Filters;
using RestApi_Produtos.Infraestruture.Data;
using RestApi_Produtos.Models;
using RestApi_Produtos.Models.Usuarios;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestApi_Produtos.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// Este serviço permite autenticar um usuario cadastrado e ativo.
        /// </summary>
        /// <returns>Retorna status Ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "campos obrigatorios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrorGenericoViewModel))]
        [HttpPost]
        [Route("Logar")]
        [ValidacaoModelStateCustomizado]

        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(Settings.Secret);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
            new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
            new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)

            };
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuario nao existente.
        /// </summary>
        /// <param name ="loginViewModelInput">View model do registro de login</param>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "campos obrigatorios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrorGenericoViewModel))]
        [HttpPost]
        [Route("Registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput loginViewModelInput)
        {

            var usuario = new Usuario();
            usuario.Login = loginViewModelInput.Login;
            usuario.Senha = loginViewModelInput.Senha;
            usuario.Email = loginViewModelInput.Email;
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();
            return Created("", loginViewModelInput);
        }
    }
}
