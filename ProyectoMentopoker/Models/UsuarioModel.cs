using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ProyectoMentopoker.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key]
        [Column("Usuario_id")]
        public string Usuario_id { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public byte[] Pass { get; set; }
        [Column("Salt")]
        public string Salt { get; set; }
        [Column("Rol")]
        public string Rol { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
