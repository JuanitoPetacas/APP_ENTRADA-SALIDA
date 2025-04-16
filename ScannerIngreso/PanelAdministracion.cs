using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerIngreso
{
    public partial class PanelAdministracion : Form
    {
        private Sesion.ApiResponse _sesion;
        public PanelAdministracion(Sesion.ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }

        private void PanelAdministracion_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
            if(_sesion == null)
            {
                lblNombrePanel.Text = "usuario";
                lblTipo.Text = "usuario";
                LabelID.Text = "0";
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");


            }
            else
            {
                lblNombrePanel.Text = _sesion.usuario.nombreUsuario;
                lblTipo.Text = _sesion.usuario.tipoUsuario;
                LabelID.Text = _sesion.usuario.numeroDocumento.ToString();
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
          
           
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHoraActual.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGestionUsuarios_Click(object sender, EventArgs e)
        {
             GestionUsuarios gestionUsuarios = new GestionUsuarios(_sesion);
            this.Hide();
            gestionUsuarios.ShowDialog();
            this.Show();
        }

        private void BtnGestionEmpleados_Click(object sender, EventArgs e)
        {
            gestionEmpleados gestionEmpleados = new gestionEmpleados(_sesion);
            this.Hide();
            gestionEmpleados.ShowDialog();
            this.Show();
        }

        private void BtnHoraEmpleados_Click(object sender, EventArgs e)
        {
            HorasEmpleadoscs horasEmpleados = new HorasEmpleadoscs(_sesion);
            this.Hide();
            horasEmpleados.ShowDialog();
            this.Show();
        }

        private void BtnMarcarManual_Click(object sender, EventArgs e)
        {
            MarcarManualmente marcar = new MarcarManualmente(_sesion);
            this.Hide();
            marcar.ShowDialog();
            this.Show();
        }

        private void Header_Resize(object sender, EventArgs e)
        {
           
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            Reportes reportes = new Reportes(_sesion);
            this.Hide();
            reportes.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
