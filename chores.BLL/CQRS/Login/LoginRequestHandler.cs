using chores.DAL;
using chores.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chores.BLL.CQRS.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<TokenSession> _tokenSessionRepository;

    public LoginRequestHandler(IRepository<User> userRepository, IRepository<TokenSession> tokenSessionRepository)
    {
        _userRepository = userRepository;
        _tokenSessionRepository = tokenSessionRepository;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAll()
            .FirstOrDefaultAsync(a => a.Login == request.Login, cancellationToken);

        if (user is null)
        {
            throw new BusinessException("Account was not found");
        }

        if (user.Password != request.Password)
        {
            throw new BusinessException("Invalid password");
        }

        var tokenSession = new TokenSession()
        {
            Id = Guid.NewGuid(),
            Token = Guid.NewGuid(),
            UserId = user.Id,
            IssuedAt = DateTime.UtcNow,
            Duration = TimeSpan.FromDays(1)
        };

        await _tokenSessionRepository.Add(tokenSession);

        return new LoginResponse()
        {
            Token = tokenSession.Token.ToString()
        };
    }
}