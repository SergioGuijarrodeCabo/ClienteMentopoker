namespace ClienteMentopoker.Models
{
    public class PartidasRequest
    {


        public int UsuarioId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string? CellId { get; set; }
        public int? Condicion { get; set; }
        public double? CantidadJugada { get; set; }


    }
    
}
