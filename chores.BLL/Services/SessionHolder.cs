namespace chores.BLL.Services;

public class SessionHolder : ISessionHolder
{
    public Guid TokenId { get; private set; }
    public Guid AccountId { get; private set; }

    public void Set(Guid token, Guid account)
    {
        TokenId = token;
        AccountId = account;
    }
}