using Microsoft.AspNetCore.Mvc;

namespace Campus_Management_System.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
