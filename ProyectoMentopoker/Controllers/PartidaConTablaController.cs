using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Filters;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;

namespace ProyectoMentopoker.Controllers
{
    public class PartidaConTablaController : Controller
    {

        private RepositoryTablas repoTablas;

        public PartidaConTablaController()
        {
            this.repoTablas = new RepositoryTablas();

        }


        [AuthorizeUsers]
        public IActionResult Jugar()
        {

            return View();
        }

        [HttpPost]
        public List<Celda> Jugar(int id)
        {

            List<Celda> tabla = this.repoTablas.GetTabla(id);
            return tabla;
        }

      



        public IActionResult insertar()
        {

            return View();
        }

        


        [HttpPost]
        public IActionResult insertar(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
            string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
            Boolean[] seguimiento_jugadas, double dineroInicial, double dineroActual, string comentario, string usuario_id)
        {

            // Create a JSON object to return as the response
            var result = new
            {
                success = true,
                message = "Partida insertada correctamente"
            };

            this.repoTablas.insertPartida(ids_Jugadas, ids_Rondas, ganancias_Rondas, cantidades_Rondas, cell_ids_Jugadas, table_ids_Jugadas, cantidades_Jugadas, seguimiento_jugadas, dineroInicial, dineroActual, comentario, usuario_id);

            return Json(result);
        }
    }
}
