using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManager.Application.Services;

public interface IAuthService
{
    string GenerateJwtToken(string userName);
}

public class AuthService : IAuthService
{

    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string userName)
    {
        var jwtSecret = _configuration["Jwt:Secret"];

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name, userName)
                }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // 3. Adicione o token como um cookie HttpOnly na resposta
        /*Response.Cookies.Append("jwt_token", tokenString, new CookieOptions
        {
            HttpOnly = true,         // ESSENCIAL: Impede acesso via JavaScript
            Secure = true,           // ESSENCIAL: Envia apenas via HTTPS
            SameSite = SameSiteMode.Lax, // Proteção contra CSRF. Use None se precisar de CORS cross-domain
                                         // Mas se usar None, 'Secure' DEVE ser true.
            Expires = DateTime.UtcNow.AddHours(1),       // Define a expiração do cookie
            IsEssential = true       // Marque como essencial se for fundamental para a funcionalidade
        });*/

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}