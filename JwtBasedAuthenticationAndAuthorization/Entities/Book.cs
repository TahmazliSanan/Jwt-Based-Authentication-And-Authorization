namespace JwtBasedAuthenticationAndAuthorization.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
