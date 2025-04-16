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

namespace ScannerIngreso
{
    public partial class MarcarManualmente : Form
    {
        private Sesion.ApiResponse _sesion;
        public MarcarManualmente(Sesion.ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }


        private void MarcarManualmente_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            timer1.Interval = 1000;
            timer1.Enabled = true;
            tableLayoutPanelRegis.Visible = false;
            tableLayoutPanelBtn.Visible = true;
            if (_sesion == null)
            {
                TxtNombre.Text = "usuario";
                lblTipo.Text = "usuario";
                LabelID.Text = "0";
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");


            }
            else
            {
                TxtNombre.Text = _sesion.usuario.nombreUsuario;
                lblTipo.Text = _sesion.usuario.tipoUsuario;
                LabelID.Text = _sesion.usuario.numeroDocumento.ToString();
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

        }

        

        private void BtnEntradaTurno_Click(object sender, EventArgs e)
        {
            TxtEntrada.Text = "entrada turno";
            tableLayoutPanelRegis.Visible = true;
            tableLayoutPanelBtn.Visible = false;
        }

        private void BtnSalidaAlmuerzo_Click(object sender, EventArgs e)
        {
            TxtEntrada.Text = "salida almuerzo";
            tableLayoutPanelRegis.Visible = true;
            tableLayoutPanelBtn.Visible = false;
        }

        private void BtnEntradaAlmuerzo_Click(object sender, EventArgs e)
        {
            TxtEntrada.Text = "entrada almuerzo";
            tableLayoutPanelRegis.Visible = true;
            tableLayoutPanelBtn.Visible = false;
        }

        private void BtnSalidaTurno_Click(object sender, EventArgs e)
        {
            TxtEntrada.Text = "salida turno";
            tableLayoutPanelRegis.Visible = true;
            tableLayoutPanelBtn.Visible = false;
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            tableLayoutPanelRegis.Visible = false;
            tableLayoutPanelBtn.Visible = true;
            TxtEntrada.Text = "";
            DtpFecha.Text = "";
            DtpHora.Text = "";
            TxtNumDoc.Text = "";
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            try{
                if (TxtNumDoc.Text.Length > 0 && TxtEntrada.Text.Length > 0)
                {



                    string url = "http://localhost:3000/marcar/manualmente";
                    var datos = new
                    {
                        numeroDocumento = Convert.ToInt64(TxtNumDoc.Text),
                        tipoIngreso = TxtEntrada.Text,
                        fecha = DtpFecha.Value.ToString("yyyy-MM-dd"),
                        hora = DtpHora.Value.ToString("HH:mm:ss")

                    };
                    using (HttpClient client = new HttpClient())
                    {

                        string jsonData = JsonConvert.SerializeObject(datos);
                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(url, content);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            marcarManualmente.respuesta respuesta = JsonConvert.DeserializeObject<marcarManualmente.respuesta>(responseBody);
                            tableLayoutPanelRegis.Visible = false;
                            tableLayoutPanelBtn.Visible = true;
                            TxtEntrada.Text = "";
                            DtpFecha.Text = "";
                            DtpHora.Text = "";
                            TxtNumDoc.Text = "";

                            MessageBox.Show(respuesta.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            marcarManualmente.respuesta respuesta = JsonConvert.DeserializeObject<marcarManualmente.respuesta>(responseBody);
                            tableLayoutPanelRegis.Visible = false;
                            tableLayoutPanelBtn.Visible = true;
                            TxtEntrada.Text = "";
                            DtpFecha.Text = "";
                            DtpHora.Text = "";
                            TxtNumDoc.Text = "";

                            MessageBox.Show(respuesta.message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           
                        }
                    }

                }
                else
                {

                    MessageBox.Show("Es necesario ingresar todos los datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                tableLayoutPanelRegis.Visible = false;
                tableLayoutPanelBtn.Visible = true;
                TxtEntrada.Text = "";
                DtpFecha.Text = "";
                DtpHora.Text = "";
                TxtNumDoc.Text = "";
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHoraActual.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCerrarSesion_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
