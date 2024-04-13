

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NTI.Application.AppContext;

public interface IApplicationContext
{
    public string Token { get; set; }
    public string Email { get; set; }

}

public class ApplicationContext : IApplicationContext
{
    public  string Token { get; set; }

    public  string Email { get; set; }

    public ApplicationContext(IHttpContextAccessor contextAccessor)
    {
        var headers = contextAccessor?.HttpContext?.Request?.Headers;

        if (headers != null)
        {
            string? token = headers?["Authorization"];
            token = token?.ToString().Replace("Bearer", string.Empty)
                .Replace("bearer", string.Empty)?.Trim();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                if (jwt != null)
                {
                    try
                    {
                        var email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                        Token = string.IsNullOrEmpty(token) ? string.Empty : token;
                        Email = string.IsNullOrEmpty(email) ? string.Empty : email;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Invalid parse token :{ex?.Message ?? ex?.InnerException?.Message}");
                    }
                }
            }
        }

    }
}