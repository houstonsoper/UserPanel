using Microsoft.AspNetCore.Mvc;

namespace userpanel.api.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}