using ProyectoMentopoker.Models;
using ProyectoMentopoker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ProyectoMentopoker.Helpers;

namespace ProyectoMentopoker.Repositories
{
    public class RepositoryLogin
    {
        private MentopokerContext context;


        public RepositoryLogin(MentopokerContext context)
        {
            this.context = context;
        }


        private string GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return "1";
            }
            else{
                var maxUsuarioId = this.context.Usuarios.Max(z => z.Usuario_id);
                var nextUsuarioId = int.Parse(maxUsuarioId) + 1;
                return nextUsuarioId.ToString();
            }
        }


        public async Task RegisterUsuario(string Email, string Pass, string Nombre, string Rol)
        {
            UsuarioModel usuario = new UsuarioModel();
            usuario.Usuario_id = this.GetMaxIdUsuario();
            usuario.Email = Email;
            usuario.Nombre = Nombre;
            usuario.Rol = Rol;

            usuario.Salt = HelperCryptography.GenerateSalt();
            usuario.Pass = HelperCryptography.EncryptPassword(Pass, usuario.Salt);          
            this.context.Usuarios.Add(usuario);
            await this.context.SaveChangesAsync();
        }


        public UsuarioModel Login(string Email, string Pass)
        {
            UsuarioModel usuario = this.context.Usuarios.FirstOrDefault(z => z.Email == Email);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                byte[] passUsuario = usuario.Pass;
                string salt = usuario.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(Pass, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);
                if(respuesta == true)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }


            }

        }


    

        public List<UsuarioModel> GetUsuarios()
        {
            var consulta = from datos in this.context.Usuarios
                           select datos;
            return consulta.ToList();
        }

        public UsuarioModel FindUsuario(string usuario_id)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Usuario_id == usuario_id
                           select datos;
            return consulta.FirstOrDefault();

        }



        public async Task UpdateUsuario(string Usuario_id, string Email, string Nombre, string Rol)
        {
            UsuarioModel usuario = this.FindUsuario(Usuario_id);
            usuario.Email = Email;
            usuario.Nombre = Nombre;
            usuario.Rol = Rol;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteUsuario(string Usuario_id)
        {
            UsuarioModel usuario = this.FindUsuario(Usuario_id);
            this.context.Remove(usuario);
            await this.context.SaveChangesAsync();
        }
    }
}
