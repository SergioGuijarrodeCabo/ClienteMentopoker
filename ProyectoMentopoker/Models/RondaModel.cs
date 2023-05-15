using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProyectoMentopoker.Models
{

    [Table("RONDAS")]
    public class RondaModel
    {
        [Key]
        [Column("Ronda_id")]
        public string Ronda_id { get; set; }
        [Column("Cantidad_jugada")]
        public double Cantidad_jugada { get; set; }
        [Column("Ganancias")]
        public double Ganancias { get; set; }
        [Column("Partida_id")]
        public string Partida_id { get; set; }
        //public List<Jugada> Jugadas { get; set; }
    }
}
