using System.IdentityModel.Tokens.Jwt;

namespace CartingService.WebApi.Middlewares
{
    public class TokenLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenLoggingMiddleware> _logger;

        public TokenLoggingMiddleware(RequestDelegate next, ILogger<TokenLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract the Authorization header from the incoming HTTP request
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var tokenStr = authHeader.Substring("Bearer ".Length).Trim();
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    if (handler.CanReadToken(tokenStr))
                    {
                        var jwtToken = handler.ReadJwtToken(tokenStr);
                        // Extraer información útil del token para registrarla
                        var user = jwtToken.Subject ?? "Unknown User";
                        var roles = string.Join(", ", jwtToken.Claims.Where(c => c.Type == "role" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(c => c.Value));
                        var permissions = string.Join(", ", jwtToken.Claims.Where(c => c.Type == "permission").Select(c => c.Value));

                        _logger.LogInformation("--- IDENTITY ACCESS TOKEN DETAILS ---");
                        _logger.LogInformation("User (Sub): {User}", user);
                        _logger.LogInformation("Roles: {Roles}", string.IsNullOrEmpty(roles) ? "None" : roles);
                        _logger.LogInformation("Permissions: {Permissions}", string.IsNullOrEmpty(permissions) ? "None" : permissions);
                        _logger.LogInformation("-------------------------------------");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error reading identity token claims inside logging middleware.");
                }
            }
            else
            {
                _logger.LogWarning("Request received without a valid Bearer token header.");
            }

            await _next(context);
        }
    }
}
