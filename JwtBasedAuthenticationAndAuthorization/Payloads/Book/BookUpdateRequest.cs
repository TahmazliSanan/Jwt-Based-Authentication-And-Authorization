namespace JwtBasedAuthenticationAndAuthorization.Payloads.Book
{
    public class BookUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
