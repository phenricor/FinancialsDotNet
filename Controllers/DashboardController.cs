using Microsoft.AspNetCore.Mvc;

namespace Financials.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}