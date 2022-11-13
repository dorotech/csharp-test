using APIBook.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIBook
{
    public class JwtAuth
    {
        private readonly string _key;


        public JwtAuth(string key)
        {
            _key = key;
        }

        public string Authenticate(string username, string password)
        {
            User user_loggin = UserRepository.Get(username, password); // Pegar o usuario do pseudo repositorio.


            JwtSecurityTokenHandler token_handle = new JwtSecurityTokenHandler();

            var token_key = Encoding.ASCII.GetBytes(_key);

            SecurityTokenDescriptor token_descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user_loggin.Username), // objetos utilizado para formar a token unica do usuario.
                    new Claim(ClaimTypes.Role, user_loggin.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // limita a token por uma hora
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(token_key),
                    SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = token_handle.CreateToken(token_descriptor);

            return token_handle.WriteToken(token); // trasnformar class da token em string
        }
    }
}
