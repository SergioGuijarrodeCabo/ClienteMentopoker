using Microsoft.AspNetCore.Mvc;

namespace ProyectoMentopoker.Controllers
{
    public class Perfil : Controller
    {
        public IActionResult PerfilUsuarios()
        {
            return View();
        }
    }
}
