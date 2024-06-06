using Dapper;
using PessoasApp.API.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;

namespace UsuariosApp.Infra.Data.Repositories
{

    public class UsuarioRepository : IUsuarioRepository
    {

        private string _connectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDProdutosApp;Integrated Security=True;";


        public void Add(Usuario usuario)
        {
            var senhaCriptografada = CryptoHelper.EncryptSHA1(usuario.Senha);
            var senhaConfirmacaoCriptografada = CryptoHelper.EncryptSHA1(usuario.SenhaConfirmacao);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"
                 INSERT INTO USUARIO(ID, NOME, EMAIL, SENHA, SENHACONFIRMACAO)
                 VALUES(@ID, @NOME, @EMAIL, @SENHA, @SENHACONFIRMACAO)

                 ", new
                {
                  @ID = usuario.Id,
                  @NOME = usuario.Nome,
                  @EMAIL = usuario.Email,
                  @SENHA = senhaCriptografada,
                  @SENHACONFIRMACAO = senhaConfirmacaoCriptografada
                });
            }

        }

        public void Delete(Usuario usuario)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"
                DELETE FROM USUARIO
                WHERE ID = @ID
                ", new
                {
                 @ID = usuario.Id
                });
            }
        }

        public List<Usuario> GetAll()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(@"
                SELECT * FROM USUARIO
                ORDER BY NOME

                ").ToList();
            }
        }

        public Usuario? GetByEmail(string email)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(@"
                SELECT * FROM USUARIO
                WHERE @EMAIL = EMAIL
                
                ", new
                {
                 @EMAIL = email

                }).FirstOrDefault();
            }
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(@"
                SELECT * FROM USUARIO
                WHERE @EMAIL = EMAIL AND @SENHA = SENHA
                
                ", new
                {
                    @EMAIL = email,
                    @SENHA = senha

                }).FirstOrDefault();
            }
        }

        public Usuario? GetById(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(@"
                SELECT * FROM USUARIO
                WHERE @ID = ID
                
                ", new
                {
                    @ID = id

                }).FirstOrDefault();
            }
        }

        public void Update(Usuario usuario)
        {
            using( var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"
                UPDATE USUARIO SET NOME = @NOME, EMAIL = @EMAIL, SENHA = @SENHA, SENHACONFIRMACAO = @SENHACONFIRMACAO
                WHERE ID = @ID
     
                ", new
                {

                    @ID = usuario.Id,
                    @NOME = usuario.Nome,
                    @EMAIL = usuario.Email,
                    @SENHA = usuario.Senha,
                    @SENHACONFIRMACAO = usuario.SenhaConfirmacao
                });
            }
        }
    }
}
