using chores.DAL.Misc;

namespace chores.DAL;

public class User : IdEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<TokenSession> TokenSessions { get; set; }
}