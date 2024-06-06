using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
        List<Usuario> GetAll();
        Usuario? GetById(Guid id);
        Usuario? GetByEmailAndSenha(string email, string senha);
        Usuario? GetByEmail(string email);
    }
}
