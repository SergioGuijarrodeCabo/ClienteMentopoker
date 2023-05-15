using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ProyectoMentopoker.Models;
using ClienteMentopoker.Models;
using Newtonsoft.Json.Linq;


namespace ClienteMentopoker.Services
{
    public class ServiceApiMentopoker
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApiEmpleados;

        public ServiceApiMentopoker(IConfiguration configuration)
        {
            this.UrlApiEmpleados =
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
                client.BaseAddress = new Uri(this.UrlApiEmpleados);
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





        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiEmpleados);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
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

        private async Task<T> CallApiAsync<T>
            (string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiEmpleados);
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

        #region

        //METODO PROTEGIDO
        public async Task<List<Celda>> GetTablaAsync(string token, int idtabla)
        {
            string request = "api/Tablas/GetTabla/"+idtabla;
            List<Celda> tabla =
                await this.CallApiAsync<List<Celda>>(request, token);
            return tabla;
        }


        #endregion



    }
}
