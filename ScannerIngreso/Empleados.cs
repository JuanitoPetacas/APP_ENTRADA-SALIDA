using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScannerIngreso
{
    public class Empleados
    {
        public class ApiResponseEmpleado<T>
        {
            public bool success { get; set; }
            public string message { get; set; }
            public T data { get; set; }
            public ApiErrorEmpleado error { get; set; }
        }

        public class ApiErrorEmpleado
        {
            public int statusCode { get; set; }
            public string errorMessage { get; set; }
        }

        public class Empleado
        {
            public int? idEmpleado { get; set; }
            public string tipoEmpleado { get; set; }
            public string nombreEmpleado { get; set; }
            public string apellidoEmpleado { get; set; }
            public string tipoDocumento { get; set; }
            public long numeroDocumento { get; set; }
            public string cargo { get; set; }
            public string RH { get; set; }

            public string estado { get; set; }
        }



        public static async Task<HttpResponseMessage> MostrarEmpleados()
        {
            string url = "http://localhost:3000/listar/empleado";
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

        public static async Task<HttpResponseMessage> MostrarPorId(long numeroDocumento)
        {
            string url = "http://localhost:3000/encontrar/nombre/empleado";
            try
            {
                var datos = new
                {
                    numeroDocumento
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
        public static async Task<HttpResponseMessage> AgregarEmpleado(string tipoEmpleado, string nombreEmpleado, string apellidoEmpleado, string tipoDocumento, long numeroDocumento, string cargo, string RH )
        {
            string url = "http://localhost:3000/crear/empleado";
            tipoEmpleado.ToUpper();
            nombreEmpleado.ToUpper();
            apellidoEmpleado.ToUpper();
            tipoDocumento.ToUpper();
            cargo.ToUpper();
            RH.ToUpper();

            try
            {
                var datos = new
                {
                    tipoEmpleado,
                    nombreEmpleado,
                    apellidoEmpleado,
                    tipoDocumento,
                    numeroDocumento,
                    cargo,
                    RH
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
        public static async Task<HttpResponseMessage> EditarEmpleado(int idEmpleado, string tipoEmpleado, string nombreEmpleado, string apellidoEmpleado, string tipoDocumento, long numeroDocumento, string cargo, string RH, string estado)
        {
            try
            {
                string url = "http://localhost:3000/editar/empleado";
                tipoEmpleado.ToUpper();
                nombreEmpleado.ToUpper();
                apellidoEmpleado.ToUpper();
                tipoDocumento.ToUpper();
                cargo.ToUpper();
                RH.ToUpper(); estado.ToUpper();
                var datos = new
                {
                    idEmpleado,
                    tipoEmpleado,
                    nombreEmpleado,
                    apellidoEmpleado,
                    tipoDocumento,
                    numeroDocumento,
                    cargo,
                    RH,
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
        public static async Task<HttpResponseMessage> EliminarEmpleado(int idEmpleado)
        {
            try
            {
                string url = "http://localhost:3000/eliminar/empleado";
                var datos = new
                {
                    idEmpleado
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
