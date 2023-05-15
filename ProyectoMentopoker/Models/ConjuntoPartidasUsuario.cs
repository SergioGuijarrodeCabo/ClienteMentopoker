namespace ProyectoMentopoker.Models
{
    public class ConjuntoPartidasUsuario
    {
        public List<PartidaModel> Partidas { get; set;}
        public List<RondaModel> Rondas { get;set;}
        public List<JugadasCalculadasModel> Jugadas { get; set; }

        //public EstadisticasPartidas EstadisticasPartidas { get; set; }

        //public EstadisticasJugadas EstadisticasJugadas { get; set; }
    }
}
