
using static ScannerIngreso.HorasRegistradas;
using static ScannerIngreso.IngresoEmpleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using PdfTextAlignment = iText.Layout.Properties.TextAlignment;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http;
using iText.Pdfa;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Borders;
using static ScannerIngreso.Empleados;

namespace ScannerIngreso
{
    public partial class HorasEmpleadoscs : Form
    {
        private Sesion.ApiResponse _sesion;
        private string idEntrada;
        private string idSalida;
        private object tipoSalidaValue;
        private object tipoEntradaValue;
        public HorasEmpleadoscs(Sesion.ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }

        public class DataCombo
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
        }

        public class ComboBoxData
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<DataCombo> data { get; set; }
        }
        private class ComboBoxItemData
        {
            public string NumeroDocumento { get; set; }

            public string NombreEmpleado { get; set; }
            public string Texto { get; set; }

            public override string ToString()
            {
                return Texto; // Esto asegura que solo el texto se muestre en el ComboBox
            }
        }

        public class EmpleadoDetalle
        {
            public int IdEmpleado { get; set; }
            public string NombreCompleto { get; set; }
            public string TipoEmpleado { get; set; }
            public string TipoDocumento { get; set; }
            public long NumeroDocumento { get; set; }
            public string Cargo { get; set; }
            public string RH { get; set; }
            public string Estado { get; set; }
            public string idEntrada { get; set; }
            public string FechaEntrada { get; set; }
            public string HoraEntrada { get; set; }
            public string TipoEntrada { get; set; }

            public string idSalida { get; set; }
            public string FechaSalida { get; set; }

            public string HoraSalida { get; set; }
            public string TipoSalida { get; set; }
        }


        

        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                HttpResponseMessage response = new HttpResponseMessage();
                // Para obtener el NumeroDocumento del elemento seleccionado
                if (TxtNumDoc.SelectedItem != null)
                {
                    ComboBoxItemData seleccionado = (ComboBoxItemData)TxtNumDoc.SelectedItem;
                    string numeroDocumento = seleccionado.NumeroDocumento;
                    response = await HorasRegistradas.MostrarIngresos(DtpFecha.Value.ToString("yyyy/MM/dd"), Convert.ToInt64(numeroDocumento));

                }
                else
                {
                    response = null;

                }
                if (DtpFecha.Text != null && TxtNumDoc.Text != "")
                {
                    string value = DtpFecha.Value.ToString("yyyy/MM/dd");
                  
                    string content = await response.Content.ReadAsStringAsync();
                    HorasRegistradas.Ingreso ingreso = JsonConvert.DeserializeObject<HorasRegistradas.Ingreso>(content);
                    if(ingreso.data.Count == 0)
                    {
                        
                        MessageBox.Show("No se encontraron datos, Ingresar el numero de documento de empleado correcto", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        

                    }
                    else
                    {
                       
                        var datos = TransformarDatos(ingreso); // `ingreso` es la respuesta deserializada
                        DgvIngresos.DataSource = datos;
                    }
                }
                BtnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontraron datos, Ingresar el numero de documento de empleado correcto", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnEliminar.Enabled = false;
            }
            
        }
        public List<EmpleadoDetalle> TransformarDatos(Ingreso ingreso)
        {
            var detalles = new List<EmpleadoDetalle>();

     

            foreach (var empleado in ingreso.data)
            {
                // Procesar las entradas si existen
                if (empleado.entradas != null && empleado.entradas.Count > 0)
                {
                    foreach (var entrada in empleado.entradas)
                    {
                        detalles.Add(new EmpleadoDetalle
                        {
                            IdEmpleado = empleado.idEmpleado,
                            NombreCompleto = $"{empleado.nombreEmpleado} {empleado.apellidoEmpleado}",
                            TipoEmpleado = empleado.tipoEmpleado,
                            TipoDocumento = empleado.tipoDocumento,
                            NumeroDocumento = empleado.numeroDocumento,
                            Cargo = empleado.cargo,
                            RH = empleado.RH,
                            Estado = empleado.estado,
                            idEntrada = entrada.idEntrada,
                            FechaEntrada = entrada.fechaEntrada,
                            HoraEntrada = entrada.horaEntrada,
                            TipoEntrada = entrada.tipoEntrada,
                            FechaSalida = null,  // No hay salida en este caso
                            HoraSalida = null,   // No hay salida en este caso
                            TipoSalida = null    // No hay salida en este caso
                        });
                    }
                }

                // Procesar las salidas si existen
                if (empleado.salidas != null && empleado.salidas.Count > 0)
                {
                    foreach (var salida in empleado.salidas)
                    {
                        detalles.Add(new EmpleadoDetalle
                        {
                            IdEmpleado = empleado.idEmpleado,
                            NombreCompleto = $"{empleado.nombreEmpleado} {empleado.apellidoEmpleado}",
                            TipoEmpleado = empleado.tipoEmpleado,
                            TipoDocumento = empleado.tipoDocumento,
                            NumeroDocumento = empleado.numeroDocumento,
                            Cargo = empleado.cargo,
                            RH = empleado.RH,
                            Estado = empleado.estado,
                            FechaEntrada = null,  // No hay entrada
                            HoraEntrada = null,   // No hay entrada
                            TipoEntrada = null,   // No hay entrada
                            idSalida = salida.idSalida,
                            FechaSalida = salida.fechaSalida,
                            HoraSalida = salida.horaSalida,
                            TipoSalida = salida.tipoSalida
                        });
                    }
                }

                // Si ni entradas ni salidas existen, no agregues ningún objeto vacío
                if (empleado.entradas == null || empleado.entradas.Count == 0)
                {
                    detalles.Add(new EmpleadoDetalle
                    {
                        IdEmpleado = empleado.idEmpleado,
                        NombreCompleto = $"{empleado.nombreEmpleado} {empleado.apellidoEmpleado}",
                        TipoEmpleado = empleado.tipoEmpleado,
                        TipoDocumento = empleado.tipoDocumento,
                        NumeroDocumento = empleado.numeroDocumento,
                        Cargo = empleado.cargo,
                        RH = empleado.RH,
                        Estado = empleado.estado,
                        idEntrada = null,
                        FechaEntrada = null,  // No hay entrada
                        HoraEntrada = null,   // No hay entrada
                        TipoEntrada = null,   // No hay entrada
                        idSalida = null,
                        FechaSalida = null,   // No hay salida
                        HoraSalida = null,    // No hay salida
                        TipoSalida = null     // No hay salida
                    });
                }
            }

            return detalles;


        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnData_Click(object sender, EventArgs e)
        {
            try
            {
               
               
                HttpResponseMessage response = await MostrarTodoIngresos(DateTime.Now.ToString("yyyy-MM-dd"));
                string content = await response.Content.ReadAsStringAsync();
                HorasRegistradas.Ingreso ingreso = JsonConvert.DeserializeObject<HorasRegistradas.Ingreso>(content);
                var datos = TransformarDatos(ingreso);
                DgvIngresos.DataSource = datos;
                BtnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encuentran datos en la tabla");
                DgvIngresos.DataSource = dt;
                BtnEliminar.Enabled = false;
            }
           
            

        }

        private async void HorasEmpleadoscs_Load(object sender, EventArgs e)
        {
            BtnEliminar.Enabled = false;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            if (_sesion == null)
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
            HttpResponseMessage response = await MostrarTodoIngresos(DateTime.Now.ToString("yyyy-MM-dd"));
            string content = await response.Content.ReadAsStringAsync();
            try
            {
               
                HorasRegistradas.Ingreso ingreso = JsonConvert.DeserializeObject<HorasRegistradas.Ingreso>(content);
                var datos = TransformarDatos(ingreso);
                DgvIngresos.DataSource = datos;
                string responseCombo = await mostrarComboEmpleado();
                ComboBoxData empleado = JsonConvert.DeserializeObject<ComboBoxData>(responseCombo);

                foreach (var comboData in empleado.data)
                {
                    string dataCompleta = $"{comboData.numeroDocumento} - {comboData.nombreEmpleado} {comboData.apellidoEmpleado}";

                    TxtNumDoc.Items.Add(new ComboBoxItemData { NumeroDocumento = comboData.numeroDocumento.ToString(), NombreEmpleado = comboData.nombreEmpleado, Texto = dataCompleta });
                }
             
               
            }
            catch (Exception ex) 
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encuentran datos en la tabla");
                DgvIngresos.DataSource = dt;
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

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // Obtener la fecha actual en un formato válido para nombres de archivo
            string fecha = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            // Definir el nombre del archivo PDF
            string rutaArchivo = Path.Combine(downloadsFolder, $"ReporteHoras-{fecha}.pdf");
            // Crear un escritor de PDF
            using (PdfWriter writer = new PdfWriter(rutaArchivo))
            {
                // Crear documento PDF
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    AddHeader(document, pdf);

                    // Título del PDF


                    // Verificar si el DataSource está asignado (simulación de un DataGridView)
                    if (DgvIngresos.DataSource is IEnumerable<object> dataSource)
                    {
                        // Crear la tabla con columnas dinámicas
                        int columnas = DgvIngresos.Columns.Count;
                        Table table = new Table(columnas).UseAllAvailableWidth();  // Usamos todo el ancho disponible
                        Table table1 = new Table(1).UseAllAvailableWidth();

                        // Establecer que la tabla ajustará automáticamente su tamaño
                        table.SetAutoLayout();
                        table1.SetAutoLayout();
                        table1.AddCell(new Cell().Add(new Paragraph("REPORTES DE DATOS")).SetBold().SetBackgroundColor(ColorConstants.YELLOW).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12).SetWidth(UnitValue.CreatePercentValue(100)).SetPaddingTop(15).SetPaddingBottom(15).SetBold());
                        document.Add(table1);






                        // Encabezados de las columnas
                        table.AddCell(new Cell().Add(new Paragraph("ID EMPLEADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("NOMBRE EMPLEADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("AREA EMPLEADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("TIPO DOCUMENTO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("NUMERO DOCUMENTO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("CARGO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("RH")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("ESTADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("FECHA ENTRADA")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("HORA ENTRADA")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("TIPO ENTRADA")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("FECHA SALIDA")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("HORA SALIDA")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));
                        table.AddCell(new Cell().Add(new Paragraph("TIPO SALIDA")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(6));

                        // Recorrer los datos del DataSource
                        foreach (var item in dataSource)
                        {
                            foreach (DataGridViewColumn columna in DgvIngresos.Columns)
                            {
                                var value = item.GetType()
                                    .GetProperty(columna.DataPropertyName, BindingFlags.Public | BindingFlags.Instance)
                                    ?.GetValue(item, null);

                                table.AddCell(new Cell()
                                    .Add(new Paragraph(value?.ToString() ?? "")) // Texto dentro de un párrafo
                                    .SetWidth(40)
                                    .SetFontSize(6)
                                    .SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE)
                                    .SetBackgroundColor(ColorConstants.WHITE)
                                    .SetTextAlignment(TextAlignment.CENTER)); // Alineación de texto

                            }
                        }



                        // Centrar la tabla horizontalmente
                        table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                        // Agregar la tabla al documento
                        document.Add(table);

                        // Confirmación
                        MessageBox.Show($"Archivo PDF generado en: {Path.GetFullPath(rutaArchivo)}",
                                        "Exportación Exitosa",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El DataSource no está configurado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void AddHeader(Document document, PdfDocument pdf)
        {
            // Crear tabla para el encabezado (3 columnas) con proporciones ajustadas
            Table headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 4, 5, 4 }))
                .UseAllAvailableWidth();

            // Logo desde los recursos
            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.logoClinica.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Image logo = new Image(ImageDataFactory.Create(ms.ToArray()));
                logo.SetHeight(50);
                logo.SetWidth(130);

                Cell logoCell = new Cell()
                    .Add(logo)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                    .SetTextAlignment(TextAlignment.LEFT); // Logo alineado a la izquierda
                headerTable.AddCell(logoCell);
            }

            // Título centrado en la tabla
            Paragraph titleInfo = new Paragraph("CLINICA NUEVA DE CARTAGO SAS")
                .SetPaddingBottom(2)
                .SetPaddingTop(2)
                .SetFontSize(12)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER);

            Cell titleCell = new Cell()
                .Add(titleInfo)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            headerTable.AddCell(titleCell);

            // Información del encabezado
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string city = "CARTAGO,";
            string admin = "ADMINISTRADOR";

            Paragraph headerInfo = new Paragraph($"Fecha: {date}\nHora: {time}\nCiudad: {city}\nVALLE DEL CAUCA\n{admin}")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetPaddingBottom(0)
                .SetFontSize(10);

            Cell infoCell = new Cell()
                .Add(headerInfo)
                .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.RIGHT); // Texto alineado a la derecha
            headerTable.AddCell(infoCell);

            // Agregar tabla al documento
            document.Add(headerTable);

            // Línea separadora
            LineSeparator line = new LineSeparator(new SolidLine())
                .SetMarginTop(5)
                .SetMarginBottom(20);





            document.Add(line);
        }

        private void DgvIngresos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            


            // Verificar si la fila actual no es null
            if (DgvIngresos.CurrentRow != null)
            {
               
                // Validar si la celda "TipoEntrada" tiene valor
                tipoEntradaValue = DgvIngresos.CurrentRow.Cells["TipoEntrada"].Value;
               
                

                // Validar si la celda "TipoSalida" tiene valor
                tipoSalidaValue = DgvIngresos.CurrentRow.Cells["TipoSalida"].Value;

                if(tipoEntradaValue != null || tipoSalidaValue != null)
                {
                    BtnEliminar.Enabled = true;
                }
                else
                {
                    BtnEliminar.Enabled = false;
                }
               
            }







        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (tipoEntradaValue != null && !string.IsNullOrEmpty(tipoEntradaValue.ToString()))
            {
                idEntrada = DgvIngresos.CurrentRow.Cells["idEntrada"].Value?.ToString() ?? "N/A";
                string fechaEntrada = DgvIngresos.CurrentRow.Cells["fechaEntrada"].Value?.ToString() ?? "Fecha no disponible";

                DialogResult result = MessageBox.Show($"¿Desea eliminar la entrada con ID: {idEntrada} de la fecha: {fechaEntrada}?",
                                "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        HttpResponseMessage response = await EliminarEntrada(Convert.ToInt64(idEntrada));
                        string content = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<apiResponseHorasRegis>(content);
                        MessageBox.Show(apiResponse.message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DgvIngresos.Refresh();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Esta entrada ha sido eliminada del sistema, renovar la tabla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                   

                   

                }

                BtnEliminar.Enabled = false;

            }

            if (tipoSalidaValue != null && !string.IsNullOrEmpty(tipoSalidaValue.ToString()))
            {
                idSalida = DgvIngresos.CurrentRow.Cells["idSalida"].Value?.ToString() ?? "N/A";
                string fechaSalida = DgvIngresos.CurrentRow.Cells["fechaSalida"].Value?.ToString() ?? "Fecha no disponible";

                DialogResult result = MessageBox.Show($"¿Desea eliminar la salida con ID: {idSalida} de la fecha: {fechaSalida}?",
                                "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    try
                    {
                        HttpResponseMessage response = await EliminarSalida(Convert.ToInt64(idSalida));
                        string content = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<apiResponseHorasRegis>(content);
                        MessageBox.Show(apiResponse.message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DgvIngresos.Refresh();
                    }
                     catch(Exception ex)
                    {
                        MessageBox.Show("Esta Salida ha sido eliminada del sistema, renovar la tabla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                  

                }

                BtnEliminar.Enabled = false;
            }
        }
    }
}
