using ClienteMentopoker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NugetMentopoker.Models;
using ProyectoMentopoker.Repositories;

namespace ProyectoMentopoker.Controllers
{
    public class PartidasController : Controller
    {

        private RepositoryEstadisticas repo { get; set; }
        private ServiceApiMentopoker service;

        public PartidasController(RepositoryEstadisticas repo, ServiceApiMentopoker service) {

            this.repo = repo;
            this.service = service;
        }

        public async Task<IActionResult> Crud()
        {
            string token =
             HttpContext.Session.GetString("TOKEN");
            //List<NugetMentopoker.Models.PartidaModel> partidas =  this.repo.GetAllPartidas();
            List<PartidaModel> partidas = await this.service.GetAllPartidasAsync(token);
            return View(partidas);
        }

        public async Task<IActionResult> Update(string Partida_id)
        {
            string token =
             HttpContext.Session.GetString("TOKEN");

            //NugetMentopoker.Models.PartidaModel partida = this.repo.FindPartida(Partida_id);
           PartidaModel partida = await this.service.FindPartidaAsync(token, Partida_id);
            return View(partida);

        }


        [HttpPost]
        public async Task<IActionResult> Update(PartidaModel partida)
        {
           

            string token =
             HttpContext.Session.GetString("TOKEN");

            await this.service.UpdatePartidaAsync(partida, token);
            //await this.repo.UpdatePartida(partida.Partida_id, partida.Cash_Inicial, partida.Cash_Final, partida.Comentarios);

            return RedirectToAction("Crud");

        }


        public async Task<IActionResult> Delete(string Partida_id)
        {
            string token =
             HttpContext.Session.GetString("TOKEN");

            await this.service.BorrarPartidasId(token, Partida_id);

            //await this.repo.DeletePartida(Partida_id);
            return RedirectToAction("Crud");

        }


    }
}
