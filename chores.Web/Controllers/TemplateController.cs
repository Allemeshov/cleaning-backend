using Microsoft.AspNetCore.Mvc;

namespace chores.Web.Controllers;

[Controller]
[Route("[controller]/[action]")]
public abstract class TemplateController : Controller
{
}