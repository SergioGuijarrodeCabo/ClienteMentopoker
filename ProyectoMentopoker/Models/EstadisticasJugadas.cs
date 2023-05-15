namespace ProyectoMentopoker.Models
{
    public class EstadisticasJugadas
    {

        public ConjuntoPartidasUsuario partidas { get; set; }
        //public List<JugadasCalculadasModel> Jugadas { get; set; }

        public List<string> SeguimientoTipoRondas { get; set; }
        public List<double> CantidadesJugadas { get; set; }
        public List<double> CantidadesRondas { get; set; }
        public List<double> GananciasTipoRondas { get; set; }

        public List<string> Rondas_ids { get; set; }

        public List<double> MediaGananciasTipoRondas { get; set; }
        public List<double> RentabilidadTipoRondas { get; set; }

        public List<double> MediaCantidadesJugadas { get; set; }


        public EstadisticasJugadas()
        {
            this.Rondas_ids = new List<string>();

            this.CantidadesRondas = new List<double>();
            this.CantidadesRondas.Add(0);
            this.CantidadesRondas.Add(0);


            this.CantidadesJugadas = new List<double>();
            this.CantidadesJugadas.Add(0);
            this.CantidadesJugadas.Add(0);


            this.GananciasTipoRondas = new List<double>();
            this.GananciasTipoRondas.Add(0);
            this.GananciasTipoRondas.Add(0);



            this.MediaGananciasTipoRondas = new List<double>();
            this.MediaGananciasTipoRondas.Add(0);
            this.MediaGananciasTipoRondas.Add(0);

            this.RentabilidadTipoRondas = new List<double>();
            this.RentabilidadTipoRondas.Add(0);
            this.RentabilidadTipoRondas.Add(0);

            this.MediaCantidadesJugadas = new List<double>();
            this.MediaCantidadesJugadas.Add(0);
            this.MediaCantidadesJugadas.Add(0);

            this.SeguimientoTipoRondas = new List<string>();

        }
    }
}
