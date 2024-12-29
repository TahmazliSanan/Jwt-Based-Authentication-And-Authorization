namespace JwtBasedAuthenticationAndAuthorization.Payloads
{
    public class BookResponse
    {
        public string Message { get; set; } = string.Empty;
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
