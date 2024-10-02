using Microsoft.AspNetCore.Mvc;

namespace CustomerSolution.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}