using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoMentopoker.Models
{

    [Table("CELDAS")]
    public class Celda
    {

        [Key]
        [Column("Identificador")]
        public int Identificador { get; set; }
        [Column("Table_id")]
        public int Table_id { get; set; }
        [Column("Id_celda")]
        public string Id_celda { get; set; }
        [Column("Background_color")]
        public string Background_color { get; set; }
        [Column("Color")]
        public string Color { get; set; }   

        public Celda(int identificador, int table_id, string id_celda, string background_color, string color)
        {
            this.Identificador = identificador;
            this.Table_id = table_id;
            this.Id_celda = id_celda;
            this.Background_color = background_color;
            this.Color = color;

        }
    }
}
