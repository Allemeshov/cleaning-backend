using chores.BLL.CQRS.GetUserById;
using chores.BLL.CQRS.Login;
using chores.BLL.CQRS.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chores.Web.Controllers;

[Controller]
[Route("shared-api/[action]")]
public class SharedController : Controller
{
    private IMediator _mediator;

    public SharedController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetUserById(Guid id)
    {
        var response = await _mediator.Send(new GetUserByIdRequest(id));

        return Ok(response);
    }
}