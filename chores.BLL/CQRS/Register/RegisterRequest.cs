using MediatR;

namespace chores.BLL.CQRS.Register;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public string Name { get; set; }
}