namespace JwtBasedAuthenticationAndAuthorization.Payloads
{
    public class BookCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
