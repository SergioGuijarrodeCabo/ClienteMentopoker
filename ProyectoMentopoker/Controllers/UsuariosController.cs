using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Repositories;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Filters;

namespace ProyectoMentopoker.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryEstadisticas repoPartidas;
        private RepositoryLogin repoLogin;

        public UsuariosController(RepositoryLogin repoLogin, RepositoryEstadisticas repoPartidas)
        {
            this.repoLogin = repoLogin;
            this.repoPartidas = repoPartidas;

        }




        [AuthorizeUsers]
        public IActionResult Crud()
        {
            List<UsuarioModel> usuarios = this.repoLogin.GetUsuarios();
            return View(usuarios);
        }



        public IActionResult Insert()
        {     
            return View();

        }



        [HttpPost]
        public async Task<IActionResult> Insert(string Email, string Pass, string Nombre, string Rol)
        {
            await this.repoLogin.RegisterUsuario(Email, Pass, Nombre , Rol);

            //return RedirectToAction("Crud");
            return View();
        }

       

        public IActionResult Update(string Usuario_id)
        {
            UsuarioModel usuario = this.repoLogin.FindUsuario(Usuario_id);
            return View(usuario);

        }


        [HttpPost]
        public async Task<IActionResult> Update(string Usuario_id, string Email, string Nombre, string Rol)
        {
            await this.repoLogin.UpdateUsuario(Usuario_id, Email, Nombre, Rol);

            return RedirectToAction("Crud");

        }


        public async Task<IActionResult> Delete(string Usuario_id)
        {
            await this.repoPartidas.BorrarPartidas(Usuario_id);
             this.repoLogin.DeleteUsuario(Usuario_id);
            return RedirectToAction("Crud");

        }


    }
}
