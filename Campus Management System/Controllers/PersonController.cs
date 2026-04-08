using Microsoft.AspNetCore.Mvc;
namespace Campus_Management_System.Models
{

    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}