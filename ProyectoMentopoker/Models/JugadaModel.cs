using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProyectoMentopoker.Models
{

    [Table("JUGADAS")]
    public class JugadaModel
    {
        [Key]
        [Column("Jugada_id")]
        public string Jugada_id { get; set; }
        [Column("Cantidad_jugada_Preflop")]
        public double Cantidad_jugada_Preflop { get; set; }
        [Column("Seguimiento_Tabla")]
        public Boolean Seguimiento_Tabla { get; set; }
        [Column("Identificador")]
        public int Identificador { get; set; }
        [Column("Ronda_id")]
        public string Ronda_id { get; set; }

    }
}
