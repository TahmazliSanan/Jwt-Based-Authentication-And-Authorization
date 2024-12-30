namespace JwtBasedAuthenticationAndAuthorization.Payloads.User
{
    public class UserResponse
    {
        public string Message { get; set; } = string.Empty;
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? BirthDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public List<string> Roles { get; set; } = new();
        public DateTime? ModifiedDateTime { get; set; }
    }
}
