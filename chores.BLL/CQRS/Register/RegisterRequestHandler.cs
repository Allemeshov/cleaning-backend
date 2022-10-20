using chores.DAL;
using chores.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace chores.BLL.CQRS.Register;

public class RegisterRequestHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly IRepository<User> _userRepository;

    public RegisterRequestHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAll()
            .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken: cancellationToken);

        if (user is not null)
        {
            throw new BusinessException("Login already in use");
        }

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Login = request.Login,
            Password = request.Password,
            Name = request.Name,
            Phone = request.Phone
        };

        await _userRepository.Add(newUser);

        return new RegisterResponse()
        {
            UserId = newUser.Id.ToString()
        };
    }
}