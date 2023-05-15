using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoMentopoker.Models
{

    [Table("Partidas")]
    public class PartidaModel
    {
        [Key]
        [Column("Partida_id")]
        public string Partida_id { get; set; }
        [Column("Cash_inicial")]
        public double Cash_Inicial { get; set; }
        [Column("Cash_final")]
        public double Cash_Final { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("Comentarios")]
        public string Comentarios { get; set; }
        [Column("Usuario_id")]
        public string Usuario_id { get; set; }
        
       // public List<Ronda> Rondas { get; set; }
    }
}
