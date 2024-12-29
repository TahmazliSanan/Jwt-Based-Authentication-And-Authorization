namespace JwtBasedAuthenticationAndAuthorization.Entities
{
    public abstract class BaseEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; } = default!;
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
