using stockModel.Models;

namespace stockMarket.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}