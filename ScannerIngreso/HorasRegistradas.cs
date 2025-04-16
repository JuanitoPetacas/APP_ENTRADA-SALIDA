using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Newtonsoft.Json;

namespace ScannerIngreso
{
    public class HorasRegistradas
    {
        public class apiResponseHorasRegis
        {
            public string message { get; set; }
        }
        public class Datum
        {
            public int idEmpleado { get; set; }
            public string tipoEmpleado { get; set; }
            public string nombreEmpleado { get; set; }
            public string apellidoEmpleado { get; set; }
            public string tipoDocumento { get; set; }
            public long numeroDocumento { get; set; }
            public string cargo { get; set; }
            public string RH { get; set; }
            public string estado { get; set; }
            public List<Entrada> entradas { get; set; }
            public List<Salida> salidas { get; set; }
        }

        public class Entrada
        {
            public string idEntrada { get; set; }
            public string fechaEntrada { get; set; }
            public string tipoEntrada { get; set; }

            public string horaEntrada { get; set; }
        }

        public class Ingreso
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<Datum> data { get; set; }
            public ApiErrorIngreso error { get; set; }

        }
        public class ApiErrorIngreso
        {
            public int statusCode { get; set; }
            public string errorMessage { get; set; }
        }
        public class Salida
        {
            public string idSalida { get; set; }
            public string fechaSalida { get; set; }
            public string tipoSalida { get; set; }

            public string horaSalida { get; set; }
        }




        public static async Task<string> mostrarComboEmpleado()
        {
            string url = "http://localhost:3000/listar/empleado";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                   HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return "no se encontro";

            }
        }
        public static async Task<HttpResponseMessage> EliminarEntrada(long idEntrada)
        {
            string url = "http://localhost:3000/eliminar/entrada";
            try
            {
                var datos = new
                {
                    idEntrada
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
            catch(Exception ex)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                };
            }
        }

        public static async Task<HttpResponseMessage> EliminarSalida(long idSalida)
        {
            string url = "http://localhost:3000/eliminar/salida";
            try
            {
                var datos = new
                {
                    idSalida
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
        public static async Task<HttpResponseMessage> MostrarIngresos(string fecha, long numeroDocumento)
        {
            string url = "http://localhost:3000/buscar/horas/empleado";
            try
            {
                var datos = new
                {
                    fecha,
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
        public static async Task<HttpResponseMessage> MostrarTodoIngresos(string fecha)
        {
            string url = "http://localhost:3000/horas/empleado";
            try
            {
                var datos = new
                {
                    fecha,

                };
                string json = JsonConvert.SerializeObject(datos);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url,content);
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
    }
}
