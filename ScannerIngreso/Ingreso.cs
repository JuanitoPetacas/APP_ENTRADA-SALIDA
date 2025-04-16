using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScannerIngreso
{
    public class IngresoEmpleado
    {

        public class DatosQr
        {

            public static string Nombre { get; set; }
            public static string Apellido { get; set; }
            public static string TipoDoc { get; set; }
            public static long NumeroDoc { get; set; }
            public static string Cargo { get; set; }

            public static string RH { get; set; }

        }

        public class ApiResponse
        {
            public  bool success { get; set; }
            public  string message { get; set; }
        }

        public static async  Task<HttpResponseMessage> generarIngreso(string tipoEntrada, long numeroDocumento)
        {
            string url = "http://localhost:3000/generar/ingreso";
            try
            {
                var datos = new
                {
                    tipoIngreso = tipoEntrada,
                    numeroDocumento

                };
                string json = JsonConvert.SerializeObject(datos);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
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
    }
}
