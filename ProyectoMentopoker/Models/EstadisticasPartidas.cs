namespace ProyectoMentopoker.Models
{
    public class EstadisticasPartidas
    {
        public ConjuntoPartidasUsuario partidas { get; set; }
       
        public double CashInicialPartidas { get; set; }
        public double CashFinalPartidas { get; set; }
        public double GananciasPartidasAcumuladas { get; set; }
        public double MediaGananciasPartidas { get; set; }
        public double RentabilidadPartidas { get; set; }

        public double GananciasRondasAcumuladas { get; set; }
        public double MediaGananciasRondas { get; set; }
        public double RentabilidadRondas { get; set; }
        
        }
}
