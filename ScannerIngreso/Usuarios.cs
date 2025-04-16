using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace ScannerIngreso
{
    public class Usuarios
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ApiResponse<T>
        {
            public bool success { get; set; }
            public string message { get; set; }
            public T data { get; set; }
            public ApiError error { get; set; }
        }

        public class ApiError
        {
            public int statusCode { get; set; }
            public string errorMessage { get; set; }
        }

        public class Usuario
        {
            public int? idUsuario { get; set; }
            public string tipoUsuario { get; set; }
            public string nombreUsuario { get; set; }
            public long numeroDocumento { get; set; }
            public string passwordUsuario { get; set; }
            
            public string estado { get; set; }
        }



        public static async Task<HttpResponseMessage> MostrarUsuarios()
        {
            string url = "http://localhost:3000/listar/usuario";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar la solicitud GET
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verificar si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer el contenido de la respuesta
                        string content = await response.Content.ReadAsStringAsync();
                        return response;
                    }
                    else
                    {
                        return new HttpResponseMessage(response.StatusCode)
                        {
                            Content = new StringContent($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                    };
                }
            }
        }

        public static async Task<HttpResponseMessage> MostrarPorNombre(string nombreUsuario)
        {
            string url = "http://localhost:3000/buscar/nombre/usuario";
            try
            {
                var datos = new
                {
                    nombreUsuario
                };
                 using (HttpClient client = new HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(datos);

                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return response;
                     
                    }
                        
                    else
                    {
                        return new HttpResponseMessage(response.StatusCode)
                        {
                            Content = new StringContent($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                };

            }
        }
        public static async Task<HttpResponseMessage> AgregarUsuario(string tipoUsuario, string nombreUsuario, long numeroDocumento, string password)
        {
            string url = "http://localhost:3000/crear/usuario";
            tipoUsuario.ToUpper(); 
            nombreUsuario.ToUpper();
            
            try
            {
                var datos = new
                {
                    tipoUsuario,
                    nombreUsuario,
                    numeroDocumento,
                    passwordUsuario = password
                };
                using (HttpClient client = new HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(datos);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return response;

                    }
                    else
                    {
                        return new HttpResponseMessage(response.StatusCode)
                        {
                            Content = new StringContent($"Error: {response.StatusCode} - {response.RequestMessage}")
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                };
            }
        }
        public static async Task<HttpResponseMessage> EditarUsuario(int idUsuario, string tipoUsuario, string nombreUsuario, long numeroDocumento, string password, string estado)
        {
            try
            {
                string url = "http://localhost:3000/editar/usuario";
                tipoUsuario.ToUpper();
                nombreUsuario.ToUpper();
                estado.ToUpper();
                var datos = new
                {
                    idUsuario,
                    tipoUsuario,
                    nombreUsuario,
                    numeroDocumento,
                    passwordUsuario = password,
                    estado
                };
                using (HttpClient client = new HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(datos);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return response;

                    }
                    else
                    {

                        return new HttpResponseMessage(response.StatusCode)
                        {
                            Content = new StringContent($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                };
            }
        }
        public static async Task<HttpResponseMessage> EliminarUsuario(int idUsuario)
        {
            try
            {
                string url = "http://localhost:3000/eliminar/usuario";
                var datos = new
                {
                    idUsuario
                };
                using (HttpClient client = new HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(datos);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url,content);
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    else
                    {
                        return new HttpResponseMessage(response.StatusCode)
                        {
                            Content = new StringContent($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                };
            }

        }
    }
}
