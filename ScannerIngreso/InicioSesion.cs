using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using static ScannerIngreso.Usuarios;

namespace ScannerIngreso
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private async void InicioSesion_Load(object sender, EventArgs e)
        {
            try
            {
                HttpResponseMessage usuarios = await Usuarios.MostrarUsuarios();
                string contentUsuarios = await usuarios.Content.ReadAsStringAsync();
                ApiResponse<List<Usuario>> apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Usuario>>>(contentUsuarios);
                if (apiResponse.data == null)
                {
                    DialogResult result = MessageBox.Show("Es necesario generar un usuario administrador, por lo tanto de rediccionaremos al sistema para crear un administrador usuario", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.OK)
                    {
                        PanelAdministracion panelAdministracion = new PanelAdministracion(null);
                        this.Hide();
                        panelAdministracion.ShowDialog();
                        this.Show();


                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al conectarse con el servidor, verifica el funcionamiento del servidor","alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
           
           
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            registro form1 = new registro();
            InicioSesion sesion = new InicioSesion();
            this.Hide();
            form1.ShowDialog();
            this.Show();
            
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:3000/login/usuario";
           
            try {
                var datos = new
                {
                    numeroDocumento = Convert.ToInt64(txtDocumento.Text),
                    passwordUsuario = txtpass.Text
                };

                using (HttpClient client = new HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(datos);

                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        Sesion.ApiResponse responseJson = JsonConvert.DeserializeObject<Sesion.ApiResponse>(responseBody);


                        if (responseJson.message == "Login successful" && responseJson.usuario.tipoUsuario == "ADMINISTRADOR")
                        {

                            PanelAdministracion panelAdministracion = new PanelAdministracion(responseJson);
                            this.Hide();
                            panelAdministracion.ShowDialog();
                            this.Show();
                            txtDocumento.Text = "";
                            txtpass.Text = "";

                        }
                        else if (responseJson.message == "Login successful" && responseJson.usuario.tipoUsuario == "ORIENTADOR")
                        {
                            panelPrincipal panelPrincipal = new panelPrincipal(responseJson);
                            this.Hide();
                            panelPrincipal.ShowDialog();
                            this.Show();
                            txtDocumento.Text = "";
                            txtpass.Text = "";


                        }
                        else if (responseJson.message == "el usuario ha sido desactivado")
                        {
                            MessageBox.Show(responseJson.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        
                       
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                    
            
            }
            catch(Exception ex)
            {
                  MessageBox.Show($"Excepción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        
    }
}