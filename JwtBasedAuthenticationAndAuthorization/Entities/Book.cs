namespace JwtBasedAuthenticationAndAuthorization.Entities
{
    public class Book : BaseEntity<long>
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
