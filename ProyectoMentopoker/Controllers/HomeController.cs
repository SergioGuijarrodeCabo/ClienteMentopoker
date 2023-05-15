using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Filters;

namespace ProyectoMentopoker.Controllers
{
    public class HomeController : Controller
    {

        [AuthorizeUsers]
        public IActionResult Index()
        {
            return View();
        }
    }
}
