using Microsoft.AspNetCore.Mvc;

namespace SalaryManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}