using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UsuariosApp.API.Security
{
    public class TokenSecurity
    {
        #region Atributos

        public static string? SecurityKey => "7f1f6883-3f53-406a-963b-f3b2d957b737";
        public static int? ExpirationInHours => 1;

        #endregion

        /// <summary>
        /// Método para gerar o token jwt da API
        /// </summary>
        public static string GenerateToken(Guid contaBancariaId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecurityKey);

            //criando o token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //gravando no token a identificação do usuário (email)
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, contaBancariaId.ToString()) }),

                //data de expiração do token
                Expires = DateTime.UtcNow.AddHours(ExpirationInHours.Value),

                //assinatura antifalsificação do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            //retornando o token
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(accessToken);
        }
    }
}
