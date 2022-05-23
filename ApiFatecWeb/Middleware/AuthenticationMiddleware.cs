using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiFatecWeb.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            if (path.Contains("login") || path.Contains("register") || path.Contains("swagger"))
            {
                await _next(context);
                return;
            }

            //Reading the AuthHeader which is signed with JWT
            string? token = context.Request.Headers["Token"];


            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("!HA8MYvLCe7KnTiDhs%GRiLz#S!ceq9XP*k#z8");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type.Contains("userdata")).Value;
                var userName = jwtToken.Claims.First(x => x.Type== "name").Value;
                var userRoleId = jwtToken.Claims.First(x => x.Type== "role").Value;

                // if validation is successful then return UserId from JWT token 
                // Identity Principal
                var claims = new[]
                {
                    new Claim("userId", userId),
                    new Claim("userName", userName),
                    new Claim("roleId", userRoleId),
                };
                var identity = new ClaimsIdentity(claims, "basic");
                context.User = new ClaimsPrincipal(identity);
            }
            //Pass to the next middleware
            await _next(context);
        }
    }
}
