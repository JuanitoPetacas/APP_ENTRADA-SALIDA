using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;
using static ScannerIngreso.Empleados;
using static ScannerIngreso.Sesion;
using static ScannerIngreso.Usuarios;

namespace ScannerIngreso
{
    public partial class gestionEmpleados : Form
    {
        private Sesion.ApiResponse _sesion;
        public gestionEmpleados(ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }
        // Clase personalizada para ComboBoxItem
        private class ComboBoxItemData
        {
            public string NumeroDocumento { get; set; }
            public string Texto { get; set; }

            public override string ToString()
            {
                return Texto; // Esto asegura que solo el texto se muestre en el ComboBox
            }
        }

        private async void gestionEmpleados_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            timer1.Interval = 1000;
            timer1.Enabled = true;
            if (_sesion == null)
            {
                LabelID.Text = "0";
                lblNombrePanel.Text = "usuario";
                lblTipo.Text = "usuario";

            }
            else
            {
                LabelID.Text = _sesion.usuario.numeroDocumento.ToString();
                lblNombrePanel.Text = _sesion.usuario.nombreUsuario.ToString();
                lblTipo.Text = _sesion.usuario.tipoUsuario.ToString();
            }
            

            TLPFormulario.Visible = false;
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnMostrarTodo.Enabled = false;
            BtnCancelEditDel.Visible = false;
            List<Empleado> empleado = await Func_Data();
            // Verificar si la lista de usuarios está vacía
            if (empleado.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encuentran datos en la tabla");
                DgvEmpleados.DataSource = dt;



            }
            else
            {
                // Si hay datos, asignarlos como DataSource
                DgvEmpleados.DataSource = empleado;
                DgvEmpleados.Refresh();  // Refrescar el DataGridView
                DgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar las columnas
            }


            List<Empleado> empleadoCombo = await Func_Data();

            foreach (var empleador in empleadoCombo)
            {
                // Crear el texto para el ComboBox
                string dataCompleta = $"{empleador.numeroDocumento} - {empleador.nombreEmpleado} {empleador.apellidoEmpleado}";

                // Agregar un nuevo ítem al ComboBox usando una clase personalizada
                TxtBuscarNombre.Items.Add(new ComboBoxItemData { NumeroDocumento = empleador.numeroDocumento.ToString(), Texto = dataCompleta });
            }











}
public async Task<List<Empleado>> Func_Data()
        {
            try
            {
                // Obtener la respuesta de la API
                HttpResponseMessage response = await Empleados.MostrarEmpleados();
                string content = await response.Content.ReadAsStringAsync();

                // Deserializar el contenido a ApiResponse<List<Usuario>>
                ApiResponseEmpleado<List<Empleado>> apiResponse = JsonConvert.DeserializeObject<ApiResponseEmpleado<List<Empleado>>>(content);


                // Verificar si la respuesta tiene datos
                if (apiResponse == null || apiResponse.data == null || apiResponse.data.Count == 0)
                {
                    // Si no hay datos, retornar una lista vacía
                    return new List<Empleado>();
                }

                // Si hay datos, retornar la lista de usuarios
                return apiResponse.data;
            }
            catch (Exception ex)
            {

                return new List<Empleado>(); // Retornar una lista vacía en caso de error
            }
        }

        private void DgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtIdEmpleado.Text = DgvEmpleados.CurrentRow.Cells["idEmpleado"].Value.ToString();
            TxtNombreEmpleado.Text = DgvEmpleados.CurrentRow.Cells["nombreEmpleado"].Value.ToString();
            TxtApellidoEmpleado.Text = DgvEmpleados.CurrentRow.Cells["apellidoEmpleado"].Value.ToString();
            TxtTipoDoc.Text = DgvEmpleados.CurrentRow.Cells["tipoDocumento"].Value.ToString();
            TxtNumDoc.Text = DgvEmpleados.CurrentRow.Cells["numeroDocumento"].Value.ToString();
            TxtTipoEmpleado.Text = DgvEmpleados.CurrentRow.Cells["tipoEmpleado"].Value.ToString();
            TxtCargo.Text = DgvEmpleados.CurrentRow.Cells["cargo"].Value.ToString();
            TxtRH.Text = DgvEmpleados.CurrentRow.Cells["RH"].Value.ToString();
            CbxEstado.Text = DgvEmpleados.CurrentRow.Cells["estado"].Value.ToString();


            BtnAgregar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnMostrarTodo.Enabled = false;
            BtnEditar.Enabled = true;
            BtnEliminar.Enabled = true;
            BtnCancelEditDel.Visible = true;
            BtnCancelEditDel.Enabled = true;
        }
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            DgvEmpleados.Visible = false;
            
            TxtBuscarNombre.Enabled = false;
            BtnBuscar.Enabled = false;
            TxtBuscarNombre.Enabled = false;
            LblBuscar.Enabled = false;
            TLPFormulario.Visible = true;
            BtnMostrarTodo.Enabled = true;
          
            LblTituloPanel.Text = "Agregar Empleado";
            TxtTipoEmpleado.Text = "";
            TxtNombreEmpleado.Text = "";
            TxtApellidoEmpleado.Text = "";
            TxtTipoDoc.Text = "";
            TxtNumDoc.Text = "";
            TxtCargo.Text = "";
            TxtRH.Text = "";

            LblIdEmpleado.Visible = false;
            TxtIdEmpleado.Visible = false;
            LblEstado.Visible = false;
            CbxEstado.Visible = false;
           

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            BtnBuscar.Enabled = true;
            TxtBuscarNombre.Enabled = true;
            LblBuscar.Enabled = true;
            DgvEmpleados.Visible = true;
            TLPFormulario.Visible = false;
            BtnEditar.Enabled = false;
            BtnMostrarTodo.Enabled = false;
            BtnAgregar.Enabled = true;
            BtnEliminar.Enabled = false;
        }

        private async void BtnMostrarTodo_Click(object sender, EventArgs e)
        {
            List<Empleado> empleado = await Func_Data();
            DgvEmpleados.DataSource = empleado;
            DgvEmpleados.Refresh();
            DgvEmpleados.Visible = true;
            TLPFormulario.Visible = false;

            BtnBuscar.Enabled = true;
            TxtBuscarNombre.Enabled = true;
            LblBuscar.Enabled = true;
            BtnMostrarTodo.Enabled = false;
            BtnEditar.Enabled = false;
            BtnAgregar.Enabled = true;
            BtnEliminar.Enabled = false;
        }
        private async void BtnEliminar_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Desea eliminar el Empleado: " + TxtNombreEmpleado.Text + "?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                HttpResponseMessage response = await Empleados.EliminarEmpleado(Convert.ToInt32(TxtIdEmpleado.Text));
                string content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseEmpleado<Empleado>>(content);

                if (apiResponse.success)
                {
                    if(DgvEmpleados.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Data");
                        dt.Rows.Add("No se encuentran datos en la tabla");
                        DgvEmpleados.DataSource = dt;

                    }
                    else
                    {
                        List<Empleado> empleado = await Func_Data();
                        DgvEmpleados.DataSource = empleado;
                        DgvEmpleados.Refresh();
                        BtnEliminar.Enabled = false;
                        BtnEditar.Enabled = false;
                        BtnAgregar.Enabled = true;
                        BtnCancelEditDel.Visible = false;
                    }
                   
                    MessageBox.Show(apiResponse.message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {
                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                // Para obtener el NumeroDocumento del elemento seleccionado
                if (TxtBuscarNombre.SelectedItem != null)
                {
                    ComboBoxItemData seleccionado = (ComboBoxItemData)TxtBuscarNombre.SelectedItem;
                    string numeroDocumento = seleccionado.NumeroDocumento;
                    response = await Empleados.MostrarPorId(Convert.ToInt64(Convert.ToInt64(numeroDocumento)));

                }
                else
                {
                    response = null;
                   
                }
                

                // Verifica el código de estado de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    // Si la respuesta es exitosa (código 2xx)
                    string content = await response.Content.ReadAsStringAsync();
                    ApiResponseEmpleado<List<Empleado>> apiResponse = JsonConvert.DeserializeObject<ApiResponseEmpleado<List<Empleado>>>(content);

                    if (apiResponse.success)
                    {
                        DgvEmpleados.DataSource = null;
                        DgvEmpleados.Refresh();
                        DgvEmpleados.DataSource = apiResponse.data;
                        TxtBuscarNombre.Text = "";
                        BtnMostrarTodo.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (response == null)
                {
                    MessageBox.Show("Selecciona el empleado", "alerta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    // Si la respuesta es un error (código diferente a 2xx)
                    string content = await response.Content.ReadAsStringAsync();
                    ApiResponseEmpleado<List<Empleado>> apiResponse = JsonConvert.DeserializeObject<ApiResponseEmpleado<List<Empleado>>>(content);
                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error de solicitud: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Empleado no encontrado, intenta buscar exactamente el nombre. ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBuscarNombre.Text = "";
            }
        }


        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                if(LblTituloPanel.Text == "Agregar Empleado")
                {
                    if ( TxtNombreEmpleado.Text != "" && TxtApellidoEmpleado.Text != "" && TxtTipoDoc.Text != "" && TxtNumDoc.Text != "" && TxtCargo.Text != "" && TxtRH.Text != "")
                    {
                        
                        if(long.TryParse(TxtNumDoc.Text, out long result))
                        {
                            HttpResponseMessage response = await Empleados.AgregarEmpleado(TxtTipoEmpleado.Text.ToUpper(), TxtNombreEmpleado.Text.ToUpper(), TxtApellidoEmpleado.Text.ToUpper(), TxtTipoDoc.Text.ToUpper(), Convert.ToInt64(TxtNumDoc.Text), TxtCargo.Text.ToUpper(), TxtRH.Text.ToUpper());
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
                                    MessageBox.Show(apiResponse.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    List<Empleado> empleado = await Func_Data();
                                    DgvEmpleados.DataSource = empleado;
                                    DgvEmpleados.Refresh();

                                    TxtTipoEmpleado.Text = "";
                                    TxtNombreEmpleado.Text = "";
                                    TxtApellidoEmpleado.Text = "";
                                    TxtTipoDoc.Text = "";
                                    TxtNumDoc.Text = "";
                                    TxtRH.Text = "";
                                    TxtCargo.Text = "";
                                    CbxEstado.Text = "";

                                }
                                else
                                {
                                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                        else{
                            MessageBox.Show("El numero de documento debe ser numerico :)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar todos los datos para crear un empleado!!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                   
                }
                else
                {
                    if (TxtNombreEmpleado.Text != "" && TxtApellidoEmpleado.Text != "" && TxtTipoDoc.Text != "" && TxtNumDoc.Text != "" && TxtCargo.Text != "" && TxtRH.Text != "" && TxtIdEmpleado.Text != "" && CbxEstado.Text != "")
                    {
                        if (long.TryParse(TxtNumDoc.Text, out long result) && long.TryParse(TxtIdEmpleado.Text, out long resultId))
                        {
                            HttpResponseMessage response = await Empleados.EditarEmpleado(Convert.ToInt32(TxtIdEmpleado.Text), TxtTipoEmpleado.Text.ToUpper(), TxtNombreEmpleado.Text.ToUpper(), TxtApellidoEmpleado.Text.ToUpper(), TxtTipoDoc.Text.ToUpper(), Convert.ToInt64(TxtNumDoc.Text), TxtCargo.Text.ToUpper(), TxtRH.Text.ToUpper(), CbxEstado.Text);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                var apiResponse = JsonConvert.DeserializeObject<ApiResponseEmpleado<Empleado>>(content);
                                if (apiResponse.success)
                                {
                                    MessageBox.Show(apiResponse.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    List<Empleado> empleado = await Func_Data();
                                    DgvEmpleados.DataSource = empleado;
                                    DgvEmpleados.Refresh();

                                    TxtIdEmpleado.Text = "";
                                    TxtTipoEmpleado.Text = "";
                                    TxtNombreEmpleado.Text = "";
                                    TxtApellidoEmpleado.Text = "";
                                    TxtTipoDoc.Text = "";
                                    TxtNumDoc.Text = "";
                                    TxtCargo.Text = "";
                                    TxtRH.Text = "";
                                    CbxEstado.Text = "";


                                }
                                else
                                {
                                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("El ID y el numero de documento deben de ser numericos :)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar todos los datos para crear un empleado!!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                   

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            TLPFormulario.Visible = true;
            BtnCancelEditDel.Visible = false;
            BtnMostrarTodo.Enabled = true;
            BtnBuscar.Enabled = false;
            TxtBuscarNombre.Enabled = false;
            LblBuscar.Enabled = false;
           
            LblTituloPanel.Text = "Editar Empleado";
      
            DgvEmpleados.Visible = false;
            LblIdEmpleado.Visible = true;
            TxtIdEmpleado.Visible = true;
            CbxEstado.Visible = true;
            BtnEliminar.Enabled = false;



        }

        private void BtnCancelEditDel_Click(object sender, EventArgs e)
        {
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnAgregar.Enabled = true;
            BtnCancelEditDel.Visible = false;
        }

        

        private void BtnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHoraActual.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    } 
    
}
