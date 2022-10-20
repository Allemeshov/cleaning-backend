using MediatR;

namespace chores.BLL.CQRS.GetUserById;

public class GetUserByIdRequest : IRequest<GetUserByIdResponse>
{
    public Guid Id { get; set; }

    public GetUserByIdRequest(Guid id)
    {
        Id = id;
    }
}