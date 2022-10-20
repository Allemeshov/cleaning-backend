using chores.DAL.Misc;

namespace chores.DAL;

public class TokenSession : IdEntity
{
    public Guid Token { get; set; }

    public DateTime IssuedAt { get; set; }
    public TimeSpan Duration { get; set; }

    public DateTime ValidUntil => IssuedAt.Add(Duration);

    public Guid UserId { get; set; }

    public virtual User User { get; set; }
}