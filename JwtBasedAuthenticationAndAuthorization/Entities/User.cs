namespace JwtBasedAuthenticationAndAuthorization.Entities
{
    public class User : BaseEntity<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? BirthDateTime { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
