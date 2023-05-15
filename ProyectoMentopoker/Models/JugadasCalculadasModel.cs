namespace ProyectoMentopoker.Models
{
    public class JugadasCalculadasModel
    {
        //public string Id_celda { get; set; }

        public string Cell_id { get; set; }
        public int Table_id { get; set; }
        public string Condicion { get; set; }
        public double Cantidad_jugada_Preflop { get; set; }
        public Boolean Seguimiento_Tabla { get; set; }
        public string Ronda_id { get; set; }
        public string Jugada_id { get; set; }
        public double Cantidad_jugada { get; set; }
        public double Ganancias { get; set; }

    }
}
