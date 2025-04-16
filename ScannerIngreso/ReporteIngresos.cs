using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static ScannerIngreso.HorasRegistradas;

namespace ScannerIngreso
{
    public class ReporteIngresos
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
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
            public List<Entrada> entradas { get; set; } // Cambiado a lista de `Entrada`
            public List<Salida> salidas { get; set; }   // Cambiado a lista de `Salida`
        }


        public class Entrada
        {
            public string fechaEntrada { get; set; }
            public string tipoEntrada { get; set; }
            public string horaEntrada { get; set; }
        }
        public class Salida
        {
            public string fechaSalida { get; set; }
            public string tipoSalida { get; set; }
            public string horaSalida { get; set; }
        }
        public class ApiResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<Data> data { get; set; }
        }


        public static async Task<HttpResponseMessage> MostrarIngresosTotales(string fecha)
        {
            string url = "http://localhost:3000/mostrar/ingresos";
            using (HttpClient client = new HttpClient())
            {

                var datos = new
                {
                    fecha

                };
                try
                {
                    string jsonData = JsonConvert.SerializeObject(datos);

                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la solicitud fue exitosa
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
                catch (Exception ex)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent($"Excepción ocurrida: {ex.Message}")
                    };
                }
            }
        }

        public static async Task<HttpResponseMessage> MostrarHorasEmpleados(string fecha, long numeroDocumento)
        {
            try
            {
                string url = "http://localhost:3000/buscar/horas/empleado";
                using (HttpClient client = new HttpClient())
                {
                    var datos = new
                    {
                        numeroDocumento,
                        fecha


                    };
                    string jsonData = JsonConvert.SerializeObject(datos);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la solicitud fue exitosa
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
