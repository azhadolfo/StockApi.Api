using StockApi.Api.Models;

namespace StockApi.Api.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}