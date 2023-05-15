using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Repositories;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Filters;
using ClienteMentopoker.Services;
using ClienteMentopoker.Models;

namespace ProyectoMentopoker.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryEstadisticas repoPartidas;
        private RepositoryLogin repoLogin;
        private ServiceApiMentopoker service;

        public UsuariosController(RepositoryLogin repoLogin, RepositoryEstadisticas repoPartidas, ServiceApiMentopoker service)
        {
            this.repoLogin = repoLogin;
            this.repoPartidas = repoPartidas;
            this.service = service;
        }




        [AuthorizeUsers]
        public async Task<IActionResult> Crud()
        {
            string token =
             HttpContext.Session.GetString("TOKEN");

            
            //List<UsuarioModel> usuarios = this.repoLogin.GetUsuarios();
            List<NugetMentopoker.Models.UsuarioModel> usuarios = await this.service.GetUsuariosAsync(token);
            return View(usuarios);
        }





        public IActionResult Insert()
        {     
            return View();

        }



        [HttpPost]
        public async Task<IActionResult> Insert(string Email, string Pass, string Nombre, string Rol)
        {
            UsuarioRequest usuario = new UsuarioRequest();

            usuario.Email = Email;
            usuario.Pass = Pass;
            usuario.Nombre = Nombre;
            usuario.Rol = Rol;

            //await this.repoLogin.RegisterUsuario(Email, Pass, Nombre , Rol);
            await this.service.RegisterUsuarioAsync(usuario);
            //return RedirectToAction("Crud");
            return View();
        }

       

        public IActionResult Update(string Usuario_id)
        {
            UsuarioModel usuario = this.repoLogin.FindUsuario(Usuario_id);
            return View(usuario);

        }


        //[HttpPost]
        //public async Task<IActionResult> Update(string Usuario_id, string Email, string Nombre, string Rol)
        //{
        //    UsuarioRequest usuario = new UsuarioRequest();

        //    usuario.Usuario_id = Usuario_id;
        //    usuario.Email = Email;
        //    usuario.Nombre = Nombre;
        //    usuario.Rol = Rol;

        //    string token =
        //    HttpContext.Session.GetString("TOKEN");

        //    //await this.repoLogin.UpdateUsuario(Usuario_id, Email, Nombre, Rol);
        //    await this.service.UpdateUsuarioAsync(usuario, token);
        //    return RedirectToAction("Crud");

        //}

        [HttpPost]
        public async Task<IActionResult> Update(string Usuario_id, string Email, string Nombre, string Rol)
        {
            UsuarioRequest usuario = new UsuarioRequest
            {
                Usuario_id = Usuario_id,
                Email = Email,
                Nombre = Nombre,
                Rol = Rol,
                Pass = null,
                Token = null
            };

            string token = HttpContext.Session.GetString("TOKEN");

            //await this.service.UpdateUsuarioAsync(usuario, token);
            await this.repoLogin.UpdateUsuario(Usuario_id, Email, Nombre, Rol);
            return RedirectToAction("Crud");
        }

        public async Task<IActionResult> Delete(string Usuario_id)
        {
            UsuarioRequest usuario = new UsuarioRequest
            {
                Usuario_id = Usuario_id,
                Email = null,
                Pass = null,
                Nombre = null,
                Rol = null,
                Token = null
            };

            string token = HttpContext.Session.GetString("TOKEN");

            //await this.service.BorrarPartidasUsuarioAsync(token, Usuario_id);
            //await this.service.DeleteUsuarioAsync(usuario, token);
            await this.repoPartidas.BorrarPartidas(Usuario_id);
            this.repoLogin.DeleteUsuario(Usuario_id);
            return RedirectToAction("Crud");
        }




        //public async Task<IActionResult> Delete(string Usuario_id)
        //{
        //    UsuarioRequest usuario = new UsuarioRequest();
        //    usuario.Usuario_id = Usuario_id;
        //    string token =  HttpContext.Session.GetString("TOKEN");

        //    await this.service.BorrarPartidasUsuarioAsync(token, Usuario_id);
        //    await this.service.DeleteUsuarioAsync(usuario, token);


        //    // await this.repoPartidas.BorrarPartidas(Usuario_id);
        //    //  this.repoLogin.DeleteUsuario(Usuario_id);
        //    return RedirectToAction("Crud");

        //}


    }
}
