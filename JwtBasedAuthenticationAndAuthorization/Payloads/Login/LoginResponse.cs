namespace JwtBasedAuthenticationAndAuthorization.Payloads.Login
{
    public class LoginResponse
    {
        public string Message { get; set; } = string.Empty;
        public long UserId { get; set; }
        public string Jwt { get; set; } = string.Empty;
    }
}
