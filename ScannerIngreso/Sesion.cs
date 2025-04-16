using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerIngreso
{
    public class Sesion
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ApiResponse
        {
            public string message { get; set; }
            public Usuario usuario { get; set; }
        }

        public class Usuario
        {
            public long idUsuario { get; set; }
            public string tipoUsuario { get; set; }
            public string nombreUsuario { get; set; }
            public long numeroDocumento { get; set; }
            public string passwordUsuario { get; set; }
            public string estado { get; set; }
        }



    }
}
