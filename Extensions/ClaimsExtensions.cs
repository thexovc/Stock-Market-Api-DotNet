using System.Security.Claims;

namespace stockMarket.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
