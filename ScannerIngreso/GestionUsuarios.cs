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
using Microsoft.VisualBasic;
using static ScannerIngreso.Usuarios;
using static ScannerIngreso.Empleados;

namespace ScannerIngreso
{
    public partial class GestionUsuarios : Form
    {
        private Sesion.ApiResponse _sesion;
        public GestionUsuarios(Sesion.ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }
        private class ComboBoxItemData
        {
            public string numeroDocumento { get; set; }
            public string nombreUsuario{ get; set; }
            public string Texto { get; set; }

            public override string ToString()
            {
                return Texto; // Esto asegura que solo el texto se muestre en el ComboBox
            }
        }

        private async void GestionUsuarios_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            timer1.Interval = 1000;
            timer1.Enabled = true;
            if(_sesion == null)
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
            

            BtnCancelEditDel.Visible = false;
         
            BtnAgregar.Enabled = true;
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnMostrarTodo.Enabled = false;
            TLPFormulario.Visible = false;
            List<Usuario> usuarios = await Func_Data();

            // Verificar si la lista de usuarios está vacía
            if (usuarios.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encuentran datos en la tabla");
                DgvUsuarios.DataSource = dt;
            }
            else
            {
                // Si hay datos, asignarlos como DataSource
                DgvUsuarios.DataSource = usuarios;
                DgvUsuarios.Refresh();  // Refrescar el DataGridView
                DgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar las columnas
            }


            List<Usuario> empleadoCombo = await Func_Data();

            foreach (var empleador in empleadoCombo)
            {
                // Crear el texto para el ComboBox
                string dataCompleta = $"{empleador.numeroDocumento} - {empleador.nombreUsuario}";

                // Agregar un nuevo ítem al ComboBox usando una clase personalizada
                TxtNumDoc.Items.Add(new ComboBoxItemData { numeroDocumento = empleador.numeroDocumento.ToString() ,nombreUsuario = empleador.nombreUsuario, Texto = dataCompleta });
            }





        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            DgvUsuarios.Visible = false;
            
            TxtNumDoc.Enabled = false;
            BtnMostrarTodo.Enabled = true;
            BtnBuscar.Enabled = false;
            LblBuscar.Enabled = false;
            TLPFormulario.Visible = true;     
            LblTituloPanel.Text = "Agregar Usuario";
            TxtNombreUsuario.Text = "";
            TxtTipoUsuario.Text = "";
            TxtNumeroDoc.Text = "";
            TxtPass.Text = "";

            LblIdUsuario.Visible = false;
            TxtIdUsuario.Visible = false;
            LblEstado.Visible = false;
            CbxEstado.Visible = false;
            BtnGuardar.Visible = true;

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            TxtNumDoc.Enabled = true;
            BtnBuscar.Enabled = true;
            LblBuscar.Enabled = true;
            

            
            DgvUsuarios.Visible = true;
            TLPFormulario.Visible = false;
            BtnEditar.Enabled = false;
            BtnMostrarTodo.Enabled = false;
            BtnAgregar.Enabled = true;
            BtnEliminar.Enabled = false;
        }

        private async void BtnMostrarTodo_Click(object sender, EventArgs e)
        {
            List<Usuario> usuario = await Func_Data();
            DgvUsuarios.DataSource = usuario;
            DgvUsuarios.Refresh();
            DgvUsuarios.Visible = true;
            TLPFormulario.Visible = false;
          
            TxtNumDoc.Enabled = true;
            BtnBuscar.Enabled = true;
            LblBuscar.Enabled = true;
            BtnMostrarTodo.Enabled = false;
            BtnEditar.Enabled = false;
            BtnAgregar.Enabled = true;
            BtnEliminar.Enabled = false;
        }

        private void DgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DgvAgregarEditar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            return;
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            TLPFormulario.Visible = true;
            BtnCancelEditDel.Visible = false;   
            BtnMostrarTodo.Enabled = true;
            BtnBuscar.Enabled = false;
            TxtNumDoc.Enabled = false ;
            LblBuscar.Enabled = false;
            BtnGuardar.Visible = true;
            LblTituloPanel.Text = "Editar Usuario";   
            DgvUsuarios.Visible = false;
            LblIdUsuario.Visible = true;
            TxtIdUsuario.Visible = true;
            LblEstado.Visible = true;
            CbxEstado.Visible = true;
            BtnEliminar.Enabled = false;
           
          

        }

        private void DgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
                
                TxtIdUsuario.Text = DgvUsuarios.CurrentRow.Cells["idUsuario"].Value.ToString();
                TxtNombreUsuario.Text = DgvUsuarios.CurrentRow.Cells["nombreUsuario"].Value.ToString();
                TxtTipoUsuario.Text = DgvUsuarios.CurrentRow.Cells["tipoUsuario"].Value.ToString();
                TxtNumeroDoc.Text = DgvUsuarios.CurrentRow.Cells["numeroDocumento"].Value.ToString();
                CbxEstado.Text = DgvUsuarios.CurrentRow.Cells["estado"].Value.ToString();
                BtnAgregar.Enabled = false;
                BtnEliminar.Enabled = false;
                BtnMostrarTodo.Enabled = false;
            BtnEditar.Enabled = true;
            BtnEliminar.Enabled = true;
            BtnCancelEditDel.Visible = true;
            BtnCancelEditDel.Enabled = true;
          


            
            
            
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
           
            DialogResult result = MessageBox.Show("¿Desea eliminar el usuario: " + TxtNombreUsuario.Text + "?","Alerta" , MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if(result == DialogResult.OK)
            {
                HttpResponseMessage response = await Usuarios.EliminarUsuario(Convert.ToInt32(TxtIdUsuario.Text));
                string content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Usuario>>(content);

                if (apiResponse.success)
                {
                    List<Usuario> usuario = await Func_Data();
                    DgvUsuarios.DataSource = usuario;
                    DgvUsuarios.Refresh();
                    BtnEliminar.Enabled = false;
                    BtnEditar.Enabled = false;
                    BtnAgregar.Enabled = true;
                    BtnCancelEditDel.Visible = false;
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
                HttpResponseMessage response = await Usuarios.MostrarPorNombre(TxtNumDoc.Text);

                if (TxtNumDoc.SelectedItem != null)
                {
                    ComboBoxItemData seleccionado = (ComboBoxItemData)TxtNumDoc.SelectedItem;

                    string NombreUsuario = seleccionado.nombreUsuario;
                    response = await Usuarios.MostrarPorNombre(NombreUsuario);

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
                    ApiResponse<List<Usuario>> apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Usuario>>>(content);

                    if (apiResponse.success)
                    {
                        DgvUsuarios.DataSource = null;
                        DgvUsuarios.Refresh();
                        DgvUsuarios.DataSource = apiResponse.data;
                        TxtNumDoc.Text = "";
                        BtnMostrarTodo.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Si la respuesta es un error (código diferente a 2xx)
                    string content = await response.Content.ReadAsStringAsync();
                    ApiResponse<List<Usuario>> apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Usuario>>>(content);
                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error de solicitud: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario no encontrado, intenta buscar exactamente el nombre. ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtNumDoc.Text = "";
            }
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if(LblTituloPanel.Text == "Agregar Usuario")
                {
                    if (TxtNombreUsuario.Text != "" && TxtNumeroDoc.Text != "" && TxtTipoUsuario.Text != "" && TxtPass.Text != "") {
                        if (TxtTipoUsuario.Text.ToUpper() == "ADMINISTRADOR" || TxtTipoUsuario.Text.ToUpper() == "ORIENTADOR")
                        {
                            HttpResponseMessage response = await Usuarios.AgregarUsuario(TxtTipoUsuario.Text.ToUpper(), TxtNombreUsuario.Text.ToUpper(), Convert.ToInt64(TxtNumeroDoc.Text), TxtPass.Text);
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Usuario>>(content, new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore,   // Ignora los valores nulos
                                    DefaultValueHandling = DefaultValueHandling.Ignore // Ignora los valores predeterminados
                                });
                                if (apiResponse.success)
                                {
                                    MessageBox.Show(apiResponse.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    List<Usuario> usuario = await Func_Data();
                                    DgvUsuarios.DataSource = usuario;
                                    DgvUsuarios.Refresh();

                                    TxtNombreUsuario.Text = "";
                                    TxtTipoUsuario.Text = "";
                                    TxtNumeroDoc.Text = "";
                                    TxtPass.Text = "";

                                }
                                else
                                {
                                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Ingrese el tipo de usuario correcto!! (administrador o orientador)", "alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar todos los datos para crear el usuario", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                else
                {
                    if(TxtNombreUsuario.Text != "" && TxtNumeroDoc.Text != "" && TxtTipoUsuario.Text != "" && TxtPass.Text != "" && TxtIdUsuario.Text != "" && CbxEstado.Text != "")
                    {
                        if (TxtPass.Text == "")
                        {
                            MessageBox.Show("Recuerde llenar el campo de contraseña con su clave anterior o una nueva", "alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (TxtTipoUsuario.Text.ToUpper() == "ADMINISTRADOR" || TxtTipoUsuario.Text.ToUpper() == "ORIENTADOR")
                        {
                            HttpResponseMessage response = await Usuarios.EditarUsuario(Convert.ToInt32(TxtIdUsuario.Text), TxtTipoUsuario.Text.ToUpper(), TxtNombreUsuario.Text.ToUpper(), Convert.ToInt64(TxtNumeroDoc.Text), TxtPass.Text, CbxEstado.Text);
                            string content = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                               
                                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Usuario>>(content);
                                if (apiResponse.success)
                                {
                                    MessageBox.Show(apiResponse.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    List<Usuario> usuario = await Func_Data();
                                    DgvUsuarios.DataSource = usuario;
                                    DgvUsuarios.Refresh();

                                    TxtNombreUsuario.Text = "";
                                    TxtTipoUsuario.Text = "";
                                    TxtNumeroDoc.Text = "";
                                    TxtPass.Text = "";
                                    TxtIdUsuario.Text = "";
                                    CbxEstado.Text = "";

                                }
                                else
                                {
                                    MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                
                                MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                        else
                        {
                            MessageBox.Show("Ingrese el tipo de usuario correcto!! (administrador o orientador)", "alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar todos los datos para editar el usuario", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnGuardarAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if(TxtTipoUsuario.Text.ToUpper() == "ADMINISTRADOR" || TxtTipoUsuario.Text.ToUpper() == "ORIENTADOR")
                {
                    HttpResponseMessage response = await Usuarios.AgregarUsuario(TxtTipoUsuario.Text.ToUpper(), TxtNombreUsuario.Text.ToUpper(), Convert.ToInt64(TxtNumeroDoc.Text), TxtPass.Text);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Usuario>>(content, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,   // Ignora los valores nulos
                            DefaultValueHandling = DefaultValueHandling.Ignore // Ignora los valores predeterminados
                        });
                        if (apiResponse.success)
                        {
                            MessageBox.Show(apiResponse.message, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            List<Usuario> usuario = await Func_Data();
                            DgvUsuarios.DataSource = usuario;
                            DgvUsuarios.Refresh();
                          
                            TxtNombreUsuario.Text = "";
                            TxtTipoUsuario.Text = "";
                            TxtNumeroDoc.Text = "";
                            TxtPass.Text = "";

                        }
                        else
                        {
                            MessageBox.Show(apiResponse.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el tipo de usuario correcto!! (administrador o orientador)" ,"alerta",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public async Task<List<Usuario>> Func_Data()
        {
            try
            {
                // Obtener la respuesta de la API
                HttpResponseMessage response = await Usuarios.MostrarUsuarios();
                string content = await response.Content.ReadAsStringAsync();

                // Deserializar el contenido a ApiResponse<List<Usuario>>
                ApiResponse<List<Usuario>> apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Usuario>>>(content);

                // Verificar si la respuesta tiene datos
                if (apiResponse == null || apiResponse.data == null || apiResponse.data.Count == 0)
                {
                    // Si no hay datos, retornar una lista vacía
                    return new List<Usuario>();
                }

                // Si hay datos, retornar la lista de usuarios
                return apiResponse.data;
            }
            catch (Exception ex)
            {
                // Manejar cualquier error durante la llamada a la API
                MessageBox.Show($"Error al obtener datos: {ex.Message}");
                return new List<Usuario>(); // Retornar una lista vacía en caso de error
            }
        }

        private void BtnCancelEditDel_Click(object sender, EventArgs e)
        {
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnAgregar.Enabled = true;
            BtnCancelEditDel.Visible = false;
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHoraActual.Text = DateTime.Now.ToString("HH:mm:ss");
        }

       
    }
}
