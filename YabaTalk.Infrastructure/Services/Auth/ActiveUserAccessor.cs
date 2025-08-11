using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens; 
using YabaTalk.Application.interfaces;

namespace YabaTalk.Infrastructure.Services.Auth
{
    public sealed class ActiveUserAccessor : IActiveUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public ActiveUserAccessor(IHttpContextAccessor accessor) => _accessor = accessor;

        public bool IsAuthenticated =>
            _accessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public string? UserId =>
            GetClaim(new[] { JwtRegisteredClaimNames.Sub, ClaimTypes.NameIdentifier, "uid" });

        public string? Phone =>
            GetClaim(new[] { ClaimTypes.MobilePhone, "phone_number", "phone" });

        private string? GetClaim(IEnumerable<string> types)
        {
            var user = _accessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated != true) return null;

            foreach (var t in types)
            {
                var v = user.FindFirst(t)?.Value;
                if (!string.IsNullOrWhiteSpace(v)) return v;
            }
            return null;
        }
    }
}
