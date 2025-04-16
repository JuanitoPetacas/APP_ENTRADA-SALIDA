using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Http;
using Newtonsoft.Json;

namespace ScannerIngreso
{

    public partial class panelPrincipal : Form
    {
        
        private Sesion.ApiResponse _sesion;
        private static string scannedData = "";
        private static string tipoEntrada = "";
        private static bool verificacionSalidaAlmuerzo = true;
        private static bool verificacionEntradaAlmuerzo = true;
        private static bool verificacionSalidaTurno = true;
        private static bool verificacionEntradaTurno = true;
        private static int countEntradaTurno = 0;
        private static int countEntradaAlmuerzo = 0;
        private static int countSalidaTurno = 0;
        private static int countSalidaAlmuerzo = 0;
        public panelPrincipal(Sesion.ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }

        private void panelPrincipal_Load(object sender, EventArgs e)
        {
            TxtScanner.Location = new Point(-1000, -1000); // Fuera del área visible
            timer1.Interval = 1000;
            timer1.Enabled = true;
            LabelID.Text = _sesion.usuario.numeroDocumento.ToString();
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblNombre.Text = _sesion.usuario.nombreUsuario.ToUpper();
            lblTipo.Text = _sesion.usuario.tipoUsuario.ToUpper();
            lblHora.Font = new System.Drawing.Font("Californian FB", 12);
            lblHora.Location = new System.Drawing.Point(275, 40);
            // Establecer el color de fondo transparente
           
            Timeout.Interval = 7000; // Tiempo máximo de 7 segundos

            TiempoScaneo.Interval = 100; // Tiempo de espera (100 ms)

            if(detectarPuerto() == null)
            {
                MessageBox.Show("No se ha detectado dispositivo Arduino", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                BtnEntradaAlmuerzo.Enabled = false;
                BtnEntradaTurno.Enabled= false;
                BtnSalidaAlmuerzo.Enabled = false;
                BtnSalidaTurno.Enabled = false;
            }
            else{
                BtnEntradaAlmuerzo.Enabled = true;
                BtnEntradaTurno.Enabled = true;
                BtnSalidaAlmuerzo.Enabled = true;
                BtnSalidaTurno.Enabled = true;
            }


            











        }



        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // Actualizar la hora en el Label
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  void BtnEntradaTurno_Click(object sender, EventArgs e)
        {

            
            DialogResult resultDialog  =  MessageBox.Show("Por favor, Escanea la tarjeta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tipoEntrada = "entrada turno";
            TxtScanner.Focus();
            Timeout.Start();




        }

        public static async Task<bool> registrarIngreso(bool success)
        {
            try
            {
                string puertoArduino = "COM12";

                if (string.IsNullOrEmpty(puertoArduino))
                {
                    MessageBox.Show("No se detectó puerto de Arduino, comuníquese con sistemas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (SerialPort serialPort = new SerialPort(puertoArduino, 9600))
                {
                    serialPort.DtrEnable = true;  // Asegura la inicialización correcta
                    serialPort.RtsEnable = true;  // Evita problemas con algunos adaptadores USB-Serial

                    serialPort.Open(); // Abre la conexión serial
                    await Task.Delay(2000); // Espera para sincronizar con Arduino

                    // Enviar el valor como "true" o "false"
                    string datosEnviar = success ? "true\n" : "false\n";
                    serialPort.Write(datosEnviar);

                    await Task.Delay(1000); // Espera antes de cerrar para asegurar recepción
                    serialPort.Close(); // Cierra la conexión serial
                }

                return true; // Datos enviados correctamente
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string detectarPuerto()
        {
            foreach(string puerto in SerialPort.GetPortNames())
                {
                return puerto;

            }
            return null;
        }

        private void TxtScanner_KeyPress(object sender, KeyPressEventArgs e)
        {
            scannedData += e.KeyChar;

            // Reiniciar el Timer
            TiempoScaneo.Stop();
            TiempoScaneo.Start();
        }

        private async void TiempoScaneo_Tick(object sender, EventArgs e)
        {
            TiempoScaneo.Stop();
            Timeout.Stop();

            if (!string.IsNullOrWhiteSpace(scannedData) || scannedData !="")
            {
                try
                {
                    string[] datosObtenidos = scannedData.Split(',');


                    IngresoEmpleado.DatosQr.Nombre = datosObtenidos[0];
                    IngresoEmpleado.DatosQr.Apellido = datosObtenidos[1];
                    IngresoEmpleado.DatosQr.TipoDoc = datosObtenidos[2];
                    IngresoEmpleado.DatosQr.NumeroDoc = Convert.ToInt64(datosObtenidos[3]);
                    IngresoEmpleado.DatosQr.Cargo = datosObtenidos[4];
                    IngresoEmpleado.DatosQr.RH = datosObtenidos[5];

                    //verificacion si la entrada se repite mas de una vez, recorre la lista de los ingresos de hoy de dicho usuario
                    // y busca si se hay mas de un tipo de entrada para no guardarla
                    try
                    {
                        HttpResponseMessage reponseFindInputs = await HorasRegistradas.MostrarIngresos(DateTime.Now.ToString("yyyy/MM/dd"), Convert.ToInt64(datosObtenidos[3]));
                        string contentInput = await reponseFindInputs.Content.ReadAsStringAsync();
                        HorasRegistradas.Ingreso ingreso = JsonConvert.DeserializeObject<HorasRegistradas.Ingreso>(contentInput);
                        verificacionEntradaTurno = !ingreso.data.Any(i => i.entradas?.Any(r=> r.tipoEntrada.Contains("entrada turno")) == true);
                        verificacionEntradaAlmuerzo = !ingreso.data.Any(i => i.entradas?.Any(r => r.tipoEntrada.Contains("entrada almuerzo")) == true);
                        verificacionSalidaTurno = !ingreso.data.Any(i => i.salidas?.Any(s => s.tipoSalida.Contains("salida turno")) == true);
                        verificacionSalidaAlmuerzo = !ingreso.data.Any(i => i.salidas?.Any(s => s.tipoSalida.Contains("salida almuerzo")) == true);
                    }
                    catch(Exception ex)
                    {
                        //cambiar esta mamada
                        MessageBox.Show("Error al verificar ingresos del empleado");
                    }

                    // Mapeo de verificaciones por tipo de entrada/salida
                    var verificaciones = new Dictionary<string, bool>
{
    { "entrada almuerzo", verificacionEntradaAlmuerzo },
    { "entrada turno", verificacionEntradaTurno },
    { "salida almuerzo", verificacionSalidaAlmuerzo },
    { "salida turno", verificacionSalidaTurno }
};

                    // Mensajes personalizados de restricción
                    var mensajesRestriccion = new Dictionary<string, string>
{
    { "entrada almuerzo", "No se permite más entradas luego del almuerzo para el usuario" },
    { "entrada turno", "No se permite más entradas luego del turno para el usuario" },
    { "salida almuerzo", "No se permite más salidas al almuerzo para el usuario" },
    { "salida turno", "No se permite más salidas de turno para el usuario" }
};

                    // Verificar si el tipo de entrada tiene restricciones
                    if (verificaciones.ContainsKey(tipoEntrada))
                    {
                        if (verificaciones[tipoEntrada]) // Si está permitido, procesar el ingreso
                        {
                            await ProcesarIngresoAsync(tipoEntrada, Convert.ToInt64(datosObtenidos[3]));
                        }
                        else // Si no está permitido, mostrar mensaje de alerta
                        {
                            MessageBox.Show(mensajesRestriccion[tipoEntrada], "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    // Método para procesar el ingreso de manera genérica
                    async Task ProcesarIngresoAsync(string tipo, long usuarioId)
                    {
                        try
                        {
                            HttpResponseMessage response = await IngresoEmpleado.generarIngreso(tipo, usuarioId);
                            string content = await response.Content.ReadAsStringAsync();
                            var respuestaApi = JsonConvert.DeserializeObject<IngresoEmpleado.ApiResponse>(content);

                            // Reset de datos escaneados
                            scannedData = "";
                            TxtScanner.Clear();
                            this.ActiveControl = null; // O enfocar otro control, como un botón

                            // Validar respuesta de la API
                            if (respuestaApi.success && (respuestaApi.message == "Entrada registrada correctamente" || respuestaApi.message == "Salida registrada correctamente"))
                            {
                                bool respuesta = await registrarIngreso(respuestaApi.success);

                                if (respuesta)
                                {
                                    MessageBox.Show(respuestaApi.message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show(respuestaApi.message, "Ooopss...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al procesar el ingreso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }






                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"No se permite ingreso con teclado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                }
                

                // Procesa el dato aquí (e.g., búsqueda en base de datos)

                // Limpiar el dato escaneado
                scannedData = "";
                TxtScanner.Clear();
                this.ActiveControl = null; // O enfocar otro control, como un botón
            }
            else
            {
                MessageBox.Show("No se escanearon datos");
            }
        }

        private void Timeout_Tick(object sender, EventArgs e)
        {
            // El tiempo máximo ha sido excedido
            Timeout.Stop();
            TiempoScaneo.Stop(); // Detener el temporizador de escaneo

            // Mostrar un mensaje de advertencia
            MessageBox.Show("El tiempo para escanear ha sido excedido. Intente de nuevo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            // Limpiar el dato escaneado
            scannedData = "";
            TxtScanner.Clear();

            // Quitar el foco del TextBox
            this.ActiveControl = null; // O enfocar otro control, como un botón
        }

        private void BtnEntradaAlmuerzo_Click(object sender, EventArgs e)
        {
            DialogResult resultDialog = MessageBox.Show("Por favor, Escanea la tarjeta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tipoEntrada = "entrada almuerzo";
            TxtScanner.Focus();
            Timeout.Start();
        }

        private void BtnSalidaAlmuerzo_Click(object sender, EventArgs e)
        {
            DialogResult resultDialog = MessageBox.Show("Por favor, Escanea la tarjeta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tipoEntrada = "salida almuerzo";
            TxtScanner.Focus();
            Timeout.Start();
        }

        private void BtnSalidaTurno_Click(object sender, EventArgs e)
        {
            DialogResult resultDialog = MessageBox.Show("Por favor, Escanea la tarjeta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tipoEntrada = "salida turno";
            TxtScanner.Focus();
            Timeout.Start();
        }

        private void BtnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
