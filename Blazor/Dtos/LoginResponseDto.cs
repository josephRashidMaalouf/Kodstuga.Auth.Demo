namespace Blazor.Dtos
{
    public class LoginResponseDto
    {
        public string tokenType { get; set; }
        public string accessToken { get; set; }
        public int expiresIn { get; set; }
        public string refreshToken { get; set; }

    }
}
