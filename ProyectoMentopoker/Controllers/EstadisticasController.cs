using ClienteMentopoker.Models;
using ClienteMentopoker.Services;
using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Filters;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;

namespace ProyectoMentopoker.Controllers
{
    public class EstadisticasController : Controller
    {
        private RepositoryTablas repoTablas;
        private RepositoryEstadisticas repoStats;
        private ServiceApiMentopoker service;

        public EstadisticasController(RepositoryEstadisticas repoStats, ServiceApiMentopoker service)
        {
            this.repoTablas = new RepositoryTablas();
            this.repoStats = repoStats;
            this.service = service;
        }


        [AuthorizeUsers]
        public IActionResult VerPartidas()
        {
            //var usuario_id = HttpContext.Session.GetString("ID");

            //if (usuario_id == null)
            //{
            //    usuario_id = "1";
            //}
            //ConjuntoPartidasUsuario conjunto = this.repoStats.GetPartidas(int.Parse(usuario_id), "partidas");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerPartidasAsync(DateTime? fechaInicio = null, DateTime? fechaFinal = null)
        {
            


            //var usuario_id = HttpContext.Session.GetString("ID");
            var usuario_id = User.FindFirst("ID")?.Value;
            if (usuario_id == null)
            {
                usuario_id = "1";
            }

            NugetMentopoker.Models.PartidasRequest partidas = new NugetMentopoker.Models.PartidasRequest
            {
                UsuarioId = usuario_id,
                FechaInicio = fechaInicio,
                FechaFinal = fechaFinal
            };

            string token =
            HttpContext.Session.GetString("TOKEN");

            NugetMentopoker.Models.EstadisticasPartidas stats = await this.service.GetEstadisticasPartidasAsync(partidas, token);

            //EstadisticasPartidas stats = this.repoStats.GetEstadisticasPartidas(int.Parse(usuario_id), "partidas", fechaInicio, fechaFinal);

            return View(stats);

        }


        [AuthorizeUsers]
        public IActionResult VerJugadas()
        {
            //var usuario_id = HttpContext.Session.GetString("ID");

            //if (usuario_id == null)
            //{
            //    usuario_id = "1";
            //}
            //ConjuntoPartidasUsuario conjunto = this.repoStats.GetPartidas(int.Parse(usuario_id), "jugadas");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerJugadas(DateTime? fechaInicio = null, DateTime? fechaFinal = null, string? cell_id = null, int? condicion = null, double? cantidadJugada = null)
        {


            string peticion = "";
            var usuario_id = User.FindFirst("ID")?.Value;
           

            if (usuario_id == null)
            {
                usuario_id = "1";
            }

            if (fechaInicio != null || fechaFinal !=null)
            {
                peticion = "jugadasFecha";
            }
            if(cell_id != null)
            {
                peticion = "jugadasCellid";
            }

            if(condicion != null)
            {

                peticion = "jugadasCondicion";
            }



            NugetMentopoker.Models.PartidasRequest partidas = new NugetMentopoker.Models.PartidasRequest
            {
                UsuarioId = usuario_id,
                FechaInicio = fechaInicio,
                FechaFinal = fechaFinal
            };

            string token =
            HttpContext.Session.GetString("TOKEN");

            NugetMentopoker.Models.EstadisticasJugadas stats = await this.service.GetEstadisticasJugadasAsync(partidas, token);



            //EstadisticasJugadas stats = this.repoStats.GetEstadisticasJugadas(int.Parse(usuario_id), peticion, fechaInicio, fechaFinal, cell_id, condicion, cantidadJugada);

            return View(stats);

        }


       



    }
}
