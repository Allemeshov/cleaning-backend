using MediatR;

namespace chores.BLL.CQRS.Login;

public class LoginRequest : IRequest<LoginResponse>
{
    public string Login { get; set; }

    public string Password { get; set; }
}