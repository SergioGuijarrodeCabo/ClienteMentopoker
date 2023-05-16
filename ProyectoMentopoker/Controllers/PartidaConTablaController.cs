using ClienteMentopoker.Services;
using Microsoft.AspNetCore.Mvc;
using NugetMentopoker.Models;
using ProyectoMentopoker.Filters;

using ProyectoMentopoker.Repositories;

namespace ProyectoMentopoker.Controllers
{
    public class PartidaConTablaController : Controller
    {

        private RepositoryTablas repoTablas;
        private ServiceApiMentopoker service;

        public PartidaConTablaController(ServiceApiMentopoker service)
        {
            this.repoTablas = new RepositoryTablas();
            this.service = service;

        }


        [AuthorizeUsers]
        public IActionResult Jugar()
        {

            return View();
        }

        [HttpPost]
        public async Task<List<NugetMentopoker.Models.Celda>> Jugar(int id)
        {
            string token =
             HttpContext.Session.GetString("TOKEN");

            //List<Celda> tabla = this.repoTablas.GetTabla(id);
            List<NugetMentopoker.Models.Celda> tabla = await this.service.GetTablaAsync(token, id);
            return tabla;
        }

      



        public IActionResult insertar()
        {

            return View();
        }

        


        [HttpPost]
        public async Task<IActionResult> insertar(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
            string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
            Boolean[] seguimiento_jugadas, double dineroInicial, double dineroActual, string comentario, string usuario_id)
        {
            PartidaRequest partidaaInsertar = new PartidaRequest();

            partidaaInsertar.IdsJugadas = ids_Jugadas;
            partidaaInsertar.IdsRondas = ids_Rondas;
            partidaaInsertar.GananciasRondas = ganancias_Rondas;
            partidaaInsertar.CantidadesRondas = cantidades_Rondas;
            partidaaInsertar.CellIdsJugadas = cell_ids_Jugadas;
            partidaaInsertar.TableIdsJugadas = table_ids_Jugadas;
            partidaaInsertar.CantidadesJugadas = cantidades_Jugadas;
            partidaaInsertar.SeguimientoJugadas = seguimiento_jugadas;
            partidaaInsertar.DineroInicial = dineroInicial;
            partidaaInsertar.DineroActual = dineroActual;
            partidaaInsertar.Comentario = comentario;
            partidaaInsertar.UsuarioId = usuario_id;


            string token =
             HttpContext.Session.GetString("TOKEN");


            // Create a JSON object to return as the response
            var result = new
            {
                success = true,
                message = "Partida insertada correctamente"
            };

            //this.repoTablas.insertPartida(ids_Jugadas, ids_Rondas, ganancias_Rondas, cantidades_Rondas, cell_ids_Jugadas, table_ids_Jugadas, cantidades_Jugadas, seguimiento_jugadas, dineroInicial, dineroActual, comentario, usuario_id);

            await this.service.InsertarPartidaAsync(partidaaInsertar, token);


            return Json(result);
        }
    }
}
