
namespace ePizzahub.Domain.Models
{
    public class UserTokenDomain
    {
        public int Id { get; set; }
        public string AccessToken { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;

        public int UserId { get; set; }
        
        public DateTime RefreshTokenExpiryTime { get; set; }
        
    }
}
