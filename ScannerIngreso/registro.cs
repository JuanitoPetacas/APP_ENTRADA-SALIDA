using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static ScannerIngreso.Empleados;

namespace ScannerIngreso
{
    public partial class registro : Form
    {
        public registro()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Length > 0 && txtApellido.Text.Length > 0 && txtTipo.Text.Length > 0 && TxtNumDoc.Text.Length > 0 && txtRH.Text.Length > 0 && txtCargo.Text.Length > 0)
                {

                    ClaseVariables persona = new ClaseVariables();

                    ClaseVariables.Nombres = txtNombre.Text;
                    ClaseVariables.Apellidos = txtApellido.Text;
                    ClaseVariables.TipoId = txtTipo.Text;
                    ClaseVariables.Identificacion = Convert.ToInt64(TxtNumDoc.Text);
                    ClaseVariables.RH = txtRH.Text;
                    ClaseVariables.Cargo = txtCargo.Text;

                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtTipo.Text = "";
                    TxtNumDoc.Text = "";
                    txtRH.Text = "";
                    txtCargo.Text = "";


                    codigoqr form2 = new codigoqr();
                    this.Hide();

                    form2.ShowDialog();
                    this.Show();



                }
                else
                {
                    MessageBox.Show("Faltan campo(s) por llenar, verifica la información", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }
            }
            catch (Exception ex) {

                MessageBox.Show("La identificacion debe de ser numerica", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void registro_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            timer1.Enabled = true;


        }



        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Length > 0 && txtApellido.Text.Length > 0 && txtTipo.Text.Length > 0 && TxtNumDoc.Text.Length > 0 && txtRH.Text.Length > 0 && txtCargo.Text.Length > 0)
                {
                    if(long.TryParse(TxtNumDoc.Text, out long result))
                    {
                        

                        
                        DialogResult resultado = MessageBox.Show("¿Estas seguro de generar el empleado?", "Informacion",  MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if(resultado == DialogResult.OK)
                        {
                            HttpResponseMessage encontrarEmpleado = await Empleados.MostrarPorId(Convert.ToInt64(TxtNumDoc.Text));

                            if (encontrarEmpleado.IsSuccessStatusCode)
                            {
                                // Preguntar si quiere generar el QR con los datos registrados
                                string contentEmpleado = await encontrarEmpleado.Content.ReadAsStringAsync();
                                var respuestaEmpleado = JsonConvert.DeserializeObject<ApiResponseEmpleado<List<Empleado>>>(contentEmpleado);
                                DialogResult peticionEmpleado = MessageBox.Show("Este empleado ya se encuentra registrado, ¿Quieré generar QR de nuevo?", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                if(peticionEmpleado == DialogResult.OK)
                                {

                                    var primerEmpleado = respuestaEmpleado.data[0];

                                    ClaseVariables.Nombres = primerEmpleado.nombreEmpleado;
                                    ClaseVariables.Apellidos = primerEmpleado.apellidoEmpleado;
                                    ClaseVariables.TipoId = primerEmpleado.tipoEmpleado;
                                    ClaseVariables.Identificacion = primerEmpleado.numeroDocumento;
                                    ClaseVariables.RH = primerEmpleado.RH;
                                    ClaseVariables.Cargo = primerEmpleado.cargo;

                                    txtNombre.Text = "";
                                    txtApellido.Text = "";
                                    txtTipo.Text = "";
                                    TxtNumDoc.Text = "";
                                    txtRH.Text = "";
                                    txtCargo.Text = "";
                                    TxtTipoEmpleado.Text = "";

                                    codigoqr codigoqr = new codigoqr();
                                    this.Hide();
                                    codigoqr.ShowDialog();
                                    this.Show();
                                }

                            }
                            else
                            {
                                HttpResponseMessage response = await Empleados.AgregarEmpleado(TxtTipoEmpleado.Text, txtNombre.Text, txtApellido.Text, txtTipo.Text, Convert.ToInt64(TxtNumDoc.Text), txtCargo.Text, txtRH.Text);
                                if (response.IsSuccessStatusCode)
                                {


                                    string content = await response.Content.ReadAsStringAsync();
                                    var apiResponse = JsonConvert.DeserializeObject<ApiResponseEmpleado<Empleado>>(content, new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore,   // Ignora los valores nulos
                                        DefaultValueHandling = DefaultValueHandling.Ignore // Ignora los valores predeterminados
                                    });
                                    if (apiResponse.success)

                                    {
                                        

                                        ClaseVariables.Nombres = txtNombre.Text;
                                        ClaseVariables.Apellidos = txtApellido.Text;
                                        ClaseVariables.TipoId = txtTipo.Text;
                                        ClaseVariables.Identificacion = Convert.ToInt64(TxtNumDoc.Text);
                                        ClaseVariables.RH = txtRH.Text;
                                        ClaseVariables.Cargo = txtCargo.Text;

                                        txtNombre.Text = "";
                                        txtApellido.Text = "";
                                        txtTipo.Text = "";
                                        TxtNumDoc.Text = "";
                                        txtRH.Text = "";
                                        txtCargo.Text = "";
                                        TxtTipoEmpleado.Text = "";
                                        codigoqr codigoqr = new codigoqr();
                                        this.Hide();
                                        codigoqr.ShowDialog();
                                        this.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error Interno del servidor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                           
                        }
                        else
                        {
                            txtNombre.Text = "";
                            txtApellido.Text = "";
                            txtTipo.Text = "";
                            TxtNumDoc.Text = "";
                            txtRH.Text = "";
                            txtCargo.Text = "";
                            TxtTipoEmpleado.Text = "";
                        }

                       
                      
                        


                    }
                    else
                    {
                        MessageBox.Show("El numero de documento debe ser numerico :)");

                    }


                    

                    


                  



                }
                else
                {
                    MessageBox.Show("Faltan campo(s) por llenar, verifica la información", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("La identificacion debe de ser numerica", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHoraActual.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
