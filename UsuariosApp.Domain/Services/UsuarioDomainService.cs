using PessoasApp.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool AutenticarUsuario(string email, string senha)
        {
            var consultarAutenticacao = _usuarioRepository.GetByEmailAndSenha(email, senha);

            var usuario = _usuarioRepository.GetByEmailAndSenha(email, CryptoHelper.EncryptSHA1(senha));

            if (consultarAutenticacao == null)
            {
                throw new ArgumentException("Usuário não encontrado. Por favor, verifique o email e a senha.");
            }

            return true;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            if (_usuarioRepository.GetByEmail(usuario.Email) != null)
                throw new ArgumentException("Email de usuário já cadastrado. Por favor, tente outro. ");

            usuario.Senha = CryptoHelper.EncryptSHA1(usuario.Senha);

            _usuarioRepository.Add(usuario);

        }

        public Usuario ConsultarUsuarioPorId(Guid id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                throw new ApplicationException("ID inexistente. Por favor, digite novamente");

            return _usuarioRepository.GetById(id);
        }

        public List<Usuario> ConsultarUsuarios()
        {
            return _usuarioRepository.GetAll();
        }

        public Usuario ObterUsuarioPorEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email); 
        }

        public void DeletarUsuario(Guid id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                throw new ApplicationException("ID inexistente. Por favor, digite novamente");


            usuario.Senha = CryptoHelper.EncryptSHA1(usuario.Senha);

            _usuarioRepository.Delete(usuario);
        }

        public void EditarUsuario(Usuario usuario)
        {
            var consultaPorId = _usuarioRepository.GetById(usuario.Id);

            if (consultaPorId == null)
                throw new ApplicationException("ID inexistente. Por favor, digite novamente");

            _usuarioRepository.Update(usuario);
        }
    }
}