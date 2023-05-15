using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;

namespace ProyectoMentopoker.Controllers
{
    public class PartidasController : Controller
    {

        private RepositoryEstadisticas repo { get; set; }

        public PartidasController(RepositoryEstadisticas repo) {

            this.repo = repo;
        }

        public IActionResult Crud()
        {
            List<PartidaModel> partidas =  this.repo.GetAllPartidas();

            return View(partidas);
        }

        public IActionResult Update(string Partida_id)
        {
            PartidaModel partida = this.repo.FindPartida(Partida_id);
            return View(partida);

        }


        [HttpPost]
        public async Task<IActionResult> Update(PartidaModel partida)
        {
            await this.repo.UpdatePartida(partida.Partida_id, partida.Cash_Inicial, partida.Cash_Final, partida.Comentarios);

            return RedirectToAction("Crud");

        }


        public async Task<IActionResult> Delete(string Partida_id)
        {
            await this.repo.DeletePartida(Partida_id);
            return RedirectToAction("Crud");

        }


    }
}
