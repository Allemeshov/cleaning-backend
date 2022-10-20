using chores.DAL;
using chores.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chores.BLL.CQRS.GetUserById;

public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IRepository<User> _userRepository;

    public GetUserByIdRequestHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAll()
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user is null)
        {
            throw new BusinessException("User was not found");
        }

        return new GetUserByIdResponse()
        {
            Login = user.Login,
            Name = user.Name,
            Phone = user.Phone
        };
    }
}