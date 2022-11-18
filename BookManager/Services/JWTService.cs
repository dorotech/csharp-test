// using System;
// using System.Collections.Generic;
// using System.Security.Claims;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using VegeFood.Models.JWTModels;
// using Microsoft.Extensions.Options;

// namespace VegeFood.Services.JWTServices
// {
//     public class JWTService: IAuthService
//     {
//         public string SecretKey { get; set; }
//         public JWTService(string secretKey)
//         {
//             SecretKey = secretKey;
//         }

//         public bool IsTokenValid(string token)
//         {
//             if (string.IsNullOrEmpty(token))
//                 throw new ArgumentException("Given token is null or empty.");
//             TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
//             JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
//             try
//             {
//                 ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
//                 return true;
//             }
//             catch (Exception)
//             {
//                 return false;
//             }
//         }

//         public string GenerateToken(IAuthContainerModel model)
//         {
//             if (model == null || model.Claims == null || model.Claims.Length == 0)
//                 throw new ArgumentException("Arguments to create token are not valid.");

//             SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(model.Claims),
//                 Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
//                 SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), model.SecurityAlgorithm)
//             };

//             JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
//             SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
//             string token = jwtSecurityTokenHandler.WriteToken(securityToken);
//             return token;
//         }

//         public IEnumerable<Claim> GetTokenClaims(string token)
//         {
//             if (string.IsNullOrEmpty(token))
//                 throw new ArgumentException("Given token is null or empty.");

//             TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

//             JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
//             try
//             {
//                 ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
//                 return tokenValid.Claims;
//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//         }

//         private SecurityKey GetSymmetricSecurityKey()
//         {
//             byte[] symmetricKey = Convert.FromBase64String(SecretKey);
//             return new SymmetricSecurityKey(symmetricKey);
//         }

//         private TokenValidationParameters GetTokenValidationParameters()
//         {
//             return new TokenValidationParameters()
//             {
//                 ValidateIssuer = false,
//                 ValidateAudience = false,
//                 IssuerSigningKey = GetSymmetricSecurityKey()
//             };
//         }
//     }

//     public static class JWTHelper
//     {
//         public static JWTContainerModel GetJWTContainerModel(string username, string password, IOptions<JWTConfig> config)
//         {
//             return new JWTContainerModel(config)
//             {
//                 Claims = new Claim[]
//                 {
//                     new Claim("Password", password),
//                     new Claim(ClaimTypes.Name, username)
//                 }
//             };
//         }
//     }
// }