using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PessoasApp.API.Helpers;
using System.Reflection;
using System.Reflection.Metadata;
using UsuariosApp.API.DTOs;
using UsuariosApp.API.Security;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public UsuariosController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Post(UsuarioPostDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = CryptoHelper.EncryptSHA1(dto.Senha),
                    SenhaConfirmacao = dto.SenhaConfirmacao

                };

                _usuarioDomainService.CadastrarUsuario(usuario);

                return StatusCode(201, new
                {
                    Message = "Usuário cadastrado com sucesso", usuario
                });
            }
            catch(ApplicationException e)
            {
                return StatusCode(400, new
                {
                    e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }
        [HttpPost]
        [Route("autenticar")]
        public IActionResult Autenticar(AutenticarUsuarioPostDTO dto)
        {
            try
            {
                bool autenticacao = _usuarioDomainService.AutenticarUsuario(dto.Email, CryptoHelper.EncryptSHA1(dto.Senha));

                if (!autenticacao)
                {
                    return StatusCode(401, new { Message = "Usuário ou senha inválidos." });
                }

                var usuario = _usuarioDomainService.ObterUsuarioPorEmail(dto.Email); 

                if (usuario == null)
                {
                    return StatusCode(500, new { Message = "Erro interno. Usuário não encontrado." });
                }

                var token = TokenSecurity.GenerateToken(usuario.Id);

                var resposta = new AutenticarUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    AccessToken = token,
                    Email = usuario.Email,
                    // Senha não deve ser retornada
                };

                return Ok(resposta);
            }
            catch (ArgumentException e)
            {
                return StatusCode(401, new { Message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Erro interno durante a autenticação." });
            }
        }


        [HttpPut]
        public IActionResult Put(UsuarioPutModel dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = dto.Id,
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = CryptoHelper.EncryptSHA1(dto.Senha),
                    SenhaConfirmacao = dto.SenhaConfirmacao
                };

                _usuarioDomainService.EditarUsuario(usuario);

                return StatusCode(200, new
                {
                    Message = "Usuário atualizado com sucesso!", usuario
                });
            }
            catch(ArgumentException e)
            {
                return StatusCode(400, new
                {
                    e.Message
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var usuario = _usuarioDomainService.ConsultarUsuarioPorId(id);

                _usuarioDomainService.DeletarUsuario(id);

                return StatusCode(200, new
                {
                    Message = "Usuário excluído com sucesso!", usuario
                });
            }
            catch(ArgumentException e)
            {
                return StatusCode(400, new
                {
                    e.Message
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }

        [HttpGet]
        public IActionResult Get() 
        {
            try
            {
               var usuarios = _usuarioDomainService.ConsultarUsuarios();

                return StatusCode(200, usuarios);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, new
                {
                    e.Message
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
               var usuario = _usuarioDomainService.ConsultarUsuarioPorId(id);

                return StatusCode(200, usuario);
            }
            catch (ArgumentException e)
            {

                return StatusCode(400, new
                {
                    e.Message
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    e.Message
                });
            }
        }
    }

}
