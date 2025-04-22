using System.Security.Claims;

namespace stockMarket.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            Console.WriteLine("Claims in user:");
            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }
            return user.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty;
        }
    }
}
