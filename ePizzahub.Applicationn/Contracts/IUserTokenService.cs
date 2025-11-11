using ePizzahub.Applicationn.DTOs.Request;

namespace ePizzahub.Applicationn.Contracts
{
    public interface IUserTokenService
    {
        Task<int> PersistToken(UserTokenRequestDto userTokenRequest);
    }
}
