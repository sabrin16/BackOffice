using Microsoft.AspNetCore.Mvc;

namespace Presention.Controllers;

public class AuthController : Controller
{
    public IActionResult SignUp()
    {
        return View();
    }
}
