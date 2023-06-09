﻿
using ClienteMentopoker.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NugetMentopoker.Models;

using ProyectoMentopoker.Repositories;
using System.Diagnostics;
using System.Security.Claims;

namespace ProyectoMentopoker.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private RepositoryLogin repoLogin;
        private ServiceApiMentopoker service;
        public LoginController(ILogger<LoginController> logger, RepositoryLogin repoLogin, ServiceApiMentopoker service)
        {
            this.repoLogin = repoLogin;
            _logger = logger;
            this.service = service;

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {

            //UsuarioModel usuario = this.repoLogin.Login(email, password);

            UsuarioRequest usuario = await this.service.GetTokenAsync(email, password);

            if (usuario != null)
            {
                //string token =
                //await this.service.GetTokenAsync(email, password);

                if (usuario.Token == null)
                {
                    ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("TOKEN", usuario.Token);

                    ClaimsIdentity identity =

                     new ClaimsIdentity

                     (CookieAuthenticationDefaults.AuthenticationScheme,

                     ClaimTypes.Email, ClaimTypes.Role);

                    Claim claimEmail = new Claim(ClaimTypes.Name, usuario.Email);
                    Claim claimRol = new Claim(ClaimTypes.Role, usuario.Rol);
                    Claim claimID = new Claim("ID", usuario.Usuario_id.ToString());
                    Claim clamNombre = new Claim("NOMBRE", usuario.Nombre.ToString());

                    identity.AddClaim(claimEmail);
                    identity.AddClaim(claimRol);
                    identity.AddClaim(claimID);
                    identity.AddClaim(clamNombre);

                    ClaimsPrincipal userPrincipal =

                    new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync

                    (CookieAuthenticationDefaults.AuthenticationScheme

                    , userPrincipal);

                    //return RedirectToAction("PerfilUsuarios", "Perfil");



                    return RedirectToAction("Index", "Home");
                }
            }

            else

            {

                ViewData["MENSAJE"] = "Usuario/Password incorrectos";

                return View();

            }

        }



        public IActionResult ErrorAcceso()

        {

            return View();

        }



        public async Task<IActionResult> LogOut()

        {

            await HttpContext.SignOutAsync

            (CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");

        }

    }



}