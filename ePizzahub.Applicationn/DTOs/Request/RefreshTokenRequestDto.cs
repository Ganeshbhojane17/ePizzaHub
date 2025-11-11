namespace ePizzahub.Applicationn.DTOs.Request
{
    public class RefreshTokenRequestDto
    {
        public string AccessToken { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;
    }
}
