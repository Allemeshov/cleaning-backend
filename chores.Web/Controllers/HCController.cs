using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace chores.Web.Controllers;

public class HCController : TemplateController
{
    [HttpGet]
    [SwaggerOperation("для внутреннего использования")]
    public async Task<IActionResult> Check()
    {
        return Ok(1);
    }
}