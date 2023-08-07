using Microsoft.AspNetCore.Mvc;

namespace SignalRSqlServer.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
