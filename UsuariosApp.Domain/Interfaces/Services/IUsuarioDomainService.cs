using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        void CadastrarUsuario(Usuario usuario);
        bool AutenticarUsuario(string email, string senha);
        void EditarUsuario(Usuario usuario);
        void DeletarUsuario(Guid id);
        List<Usuario> ConsultarUsuarios();
        Usuario ConsultarUsuarioPorId(Guid id);

        Usuario ObterUsuarioPorEmail(string email);
    }
}
