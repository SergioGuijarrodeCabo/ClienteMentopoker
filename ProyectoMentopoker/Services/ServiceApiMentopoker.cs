using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ProyectoMentopoker.Models;
using ClienteMentopoker.Models;
using Newtonsoft.Json.Linq;
using NugetMentopoker.Models;

namespace ClienteMentopoker.Services
{
    public class ServiceApiMentopoker
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApiMentopoker;

        public ServiceApiMentopoker(IConfiguration configuration)
        {
            this.UrlApiMentopoker =
                configuration.GetValue<string>("ApiUrls:ApiMentopoker");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //public async Task<UsuarioRequest> GetTokenAsync
        //    (string username, string password)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string request = "/api/Login/Login";
        //        client.BaseAddress = new Uri(this.UrlApiEmpleados);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(this.Header);
        //        UsuarioRequest model = new UsuarioRequest
        //        {
        //            Email = username,
        //            Pass = password
        //        };
        //        string jsonModel = JsonConvert.SerializeObject(model);
        //        StringContent content =
        //            new StringContent(jsonModel, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response =
        //            await client.PostAsync(request, content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data =
        //                await response.Content.ReadAsStringAsync();

        //            JObject jsonObject = JObject.Parse(data);
        //            string token =
        //                jsonObject.GetValue("response").ToString();

        //            model.Token = token;
        //            return model;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        public async Task<UsuarioRequest> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Login/Login";
                client.BaseAddress = new Uri(this.UrlApiMentopoker);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                UsuarioRequest model = new UsuarioRequest
                {
                    Email = username,
                    Pass = password
                };
                string jsonModel = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    UsuarioRequest usuario = JsonConvert.DeserializeObject<UsuarioRequest>(data);

                    // Assign the relevant properties to the UsuarioRequest object
                    model.Usuario_id = usuario.Usuario_id;
                    model.Nombre = usuario.Nombre;
                    model.Email = usuario.Email;
                    model.Rol = usuario.Rol;
                    model.Token = usuario.Token;

                    return model;
                }
                else
                {
                    return null;
                }
            }
        }





        private async Task<T> CallApiAsync<T>(string request, object requestData = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiMentopoker);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);

                if (requestData != null)
                {


                    string json = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(request, content);
                }
                else
                {
                    response = await client.GetAsync(request);
                }


                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
      
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApiAsync<T>
            (string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiMentopoker);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
                    ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request, string token, object requestData = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiMentopoker);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                HttpResponseMessage response;
                if (requestData != null)
                {


                    string json = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(request, content);
                }
                else
                {
                    response = await client.GetAsync(request);
                }

                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }




        #region

       


        public async Task InsertarPartidaAsync(PartidaRequest partidaRequest, string token)
        {
            string request = "api/Tablas/InsertarPartida"; // Replace 'ControllerName' with the actual name of the controller handling the API endpoint
            await this.CallApiAsync<object>(request, token, partidaRequest);
        }


      
        public async Task<List<NugetMentopoker.Models.Celda>> GetTablaAsync(string token, int idtabla)
        {
            string request = "api/Tablas/GetTabla/"+idtabla;
            List<NugetMentopoker.Models.Celda> tabla =
                await this.CallApiAsync<List<NugetMentopoker.Models.Celda>>(request, token);
            return tabla;
        }


        #endregion


        #region
       
        public async Task<List<NugetMentopoker.Models.UsuarioModel>> GetUsuariosAsync(string token)
        {
            string request = "api/Login/GetUsuarios";
            List<NugetMentopoker.Models.UsuarioModel> usuarios =
                await this.CallApiAsync<List<NugetMentopoker.Models.UsuarioModel>>(request, token);
            return usuarios;
        }

        public async Task UpdateUsuarioAsync(UsuarioRequest usuario, string token)
        {
            string request = "api/Login/UpdateUsuario";          
            await this.CallApiAsync<List<NugetMentopoker.Models.UsuarioModel>>(request, token, usuario);        
        }

        public async Task DeleteUsuarioAsync(UsuarioRequest usuario, string token)
        {
            string request = "api/Login/DeleteUsuario";
            await this.CallApiAsync<List<NugetMentopoker.Models.UsuarioModel>>(request, token, usuario);
        }

        public async Task RegisterUsuarioAsync(UsuarioRequest usuario)
        {
            string request = "api/Login/RegisterUsuario";
            await this.CallApiAsync<List<NugetMentopoker.Models.UsuarioModel>>(request, usuario);
        }
        #endregion

        #region
        public async Task BorrarPartidasUsuarioAsync(string token, int id)
        {
            string request = "api/Partidas/BorrarPartidasUsuario/"+id;
            await this.CallApiAsync<object>(request, token);

        }
        public async Task BorrarPartidasId(string token, int id)
        {
            string request = "api/Partidas/BorrarPartidasId/"+id;
            await this.CallApiAsync<object>(request, token);
        }

        public async Task UpdatePartidaAsync(PartidaRequest partida, string token)
        {
            string request = "api/Partidas/UpdatePartida";
            await this.CallApiAsync<object>(request, token, partida);
        }

        #endregion


        #region
        public async Task<List<NugetMentopoker.Models.PartidaModel>> GetAllPartidasAsync(string token)
        {
            string request = "api/Estadisticas/GetAllPartidas";
            List<NugetMentopoker.Models.PartidaModel> partidas =
                await this.CallApiAsync<List<NugetMentopoker.Models.PartidaModel>>(request, token);
            return partidas;
        }

        public async Task<NugetMentopoker.Models.PartidaModel> FindPartidaAsync(string token, int id)
        {
            string request = "api/Estadisticas/FindPartida/"+id;
            NugetMentopoker.Models.PartidaModel partida =
                await this.CallApiAsync<NugetMentopoker.Models.PartidaModel>(request, token);
            return partida;
        }


        public async Task<List<NugetMentopoker.Models.ConjuntoPartidasUsuario>> GetPartidasAsync(PartidaRequest filtros, string token)
        {
            string request = "api/Estadisticas/GetPartidas";
            List<NugetMentopoker.Models.ConjuntoPartidasUsuario> partidas =
                await this.CallApiAsync<List<NugetMentopoker.Models.ConjuntoPartidasUsuario>>(request, token, filtros);
            return partidas;
        }

        public async Task<List<NugetMentopoker.Models.EstadisticasPartidas>> GetEstadisticasPartidasAsync(PartidaRequest filtros, string token)
        {
            string request = "api/Estadisticas/GetEstadisticasPartidas";
            List<NugetMentopoker.Models.EstadisticasPartidas> partidas =
                await this.CallApiAsync<List<NugetMentopoker.Models.EstadisticasPartidas>>(request, token, filtros);
            return partidas;
        }

        public async Task<List<NugetMentopoker.Models.EstadisticasJugadas>> GetEstadisticasJugadasAsync(PartidaRequest filtros, string token)
        {
            string request = "api/Estadisticas/GetEstadisticasJugadas";
            List<NugetMentopoker.Models.EstadisticasJugadas> partidas =
                await this.CallApiAsync<List<NugetMentopoker.Models.EstadisticasJugadas>>(request, token, filtros);
            return partidas;
        }

        #endregion
    }
}
