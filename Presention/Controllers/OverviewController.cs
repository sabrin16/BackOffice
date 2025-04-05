using Microsoft.AspNetCore.Mvc;

namespace Presention.Controllers;

public class OverviewController : Controller
{
    [Route("admin/overview")]

    public IActionResult Index()
    {
        return View();
    }
}
