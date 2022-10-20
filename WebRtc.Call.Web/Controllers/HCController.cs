using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebRtc.Call.Web.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class HCController : Controller
{
    [HttpGet]
    [SwaggerOperation("для внутреннего использования")]
    public async Task<IActionResult> Check()
    {
        return Ok(1);
    }
}