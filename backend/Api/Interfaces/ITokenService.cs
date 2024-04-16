using Api.Models;

namespace Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
