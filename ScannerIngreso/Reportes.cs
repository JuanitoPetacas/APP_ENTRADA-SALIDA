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
using static ScannerIngreso.HorasRegistradas;


namespace ScannerIngreso
{
    public partial class Reportes : Form
    {
        public Sesion.ApiResponse _sesion;
        public Reportes(Sesion.ApiResponse sesion)
        {
            InitializeComponent();
            _sesion = sesion;
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
        public class TablaEmpleado
        {
            public int IdEmpleado { get; set; }
            public string NombreCompleto { get; set; }
            public string TipoEmpleado { get; set; }
            public string TipoDocumento { get; set; }
            public long NumeroDocumento { get; set; }
            public string Cargo { get; set; }
            public string RH { get; set; }
            public string Estado { get; set; }
            public double HorasTotales { get; set; }
            public List<RegistroDetalle> Registros { get; set; }
        }

        public class RegistroDetalle
        {
            public string TipoRegistro { get; set; }
            public string FechaEntrada { get; set; }
            public string HoraEntrada { get; set; }
            public string FechaSalida { get; set; }
            public string HoraSalida { get; set; }
            public double HorasCalculadas { get; set; }
        }


        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Reportes_Load(object sender, EventArgs e)
        {
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

            try
            {
                HttpResponseMessage response = await ReporteIngresos.MostrarIngresosTotales(DateTime.Now.ToString("yyyy/MM"));
                string content = await response.Content.ReadAsStringAsync();
                ReporteIngresos.ApiResponse ingresos = JsonConvert.DeserializeObject<ReporteIngresos.ApiResponse>(content);
                var data = ProcesarDatos(ingresos);
                DgvIngresos.DataSource = data;
                var empleadoCombo = ProcesarDatos(ingresos);

                foreach (var empleador in empleadoCombo)
                {
                    // Crear el texto para el ComboBox
                    string dataCompleta = $"{empleador.NumeroDocumento} - {empleador.NombreCompleto}";

                    // Agregar un nuevo ítem al ComboBox usando una clase personalizada
                    TxtNumDoc.Items.Add(new ComboBoxItemData { NumeroDocumento = empleador.NumeroDocumento.ToString(), NombreEmpleado = empleador.NombreCompleto, Texto = dataCompleta });
                }
            }
            catch (Exception ex)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encuentran datos registrados");
                DgvIngresos.DataSource = dt;

            }



        }
        public List<TablaEmpleado> ProcesarDatos(ReporteIngresos.ApiResponse ingreso)
        {
            var tabla = new List<TablaEmpleado>();

            foreach (var empleado in ingreso.data)
            {
                var tablaEmpleado = new TablaEmpleado
                {
                    IdEmpleado = empleado.idEmpleado,
                    NombreCompleto = $"{empleado.nombreEmpleado} {empleado.apellidoEmpleado}",
                    TipoEmpleado = empleado.tipoEmpleado,
                    TipoDocumento = empleado.tipoDocumento,
                    NumeroDocumento = empleado.numeroDocumento,
                    Cargo = empleado.cargo,
                    RH = empleado.RH,
                    Estado = empleado.estado,
                    HorasTotales = 0,
                    Registros = new List<RegistroDetalle>()
                };

                try
                {
                    // Ordenar entradas y salidas por fecha y hora
                    var entradasOrdenadas = empleado.entradas.OrderBy(e => e.fechaEntrada).ThenBy(e => e.horaEntrada).ToList();
                    var salidasOrdenadas = empleado.salidas.OrderBy(s => s.fechaSalida).ThenBy(s => s.horaSalida).ToList();

                    TimeSpan totalHoras = TimeSpan.Zero;

                    foreach (var entrada in entradasOrdenadas)
                    {
                        var tipoEntrada = entrada.tipoEntrada.ToLower();
                        var horaEntrada = TimeSpan.Parse(entrada.horaEntrada);

                        // Buscar la salida correspondiente
                        var salidaCorrespondiente = salidasOrdenadas.FirstOrDefault(s =>
                            s.fechaSalida == entrada.fechaEntrada && // Coinciden las fechas
                            ((tipoEntrada == "entrada turno" && s.tipoSalida.ToLower() == "salida turno") ||
                             (tipoEntrada == "entrada almuerzo" && s.tipoSalida.ToLower() == "salida almuerzo"))
                        );

                        if (salidaCorrespondiente != null)
                        {
                            var horaSalida = TimeSpan.Parse(salidaCorrespondiente.horaSalida);

                            // Calcular horas trabajadas
                            TimeSpan horasTrabajadas = horaSalida - horaEntrada;
                            totalHoras += horasTrabajadas;

                            // Agregar registro detallado
                            tablaEmpleado.Registros.Add(new RegistroDetalle
                            {
                                TipoRegistro = tipoEntrada == "entrada turno" ? "Turno" : "Almuerzo",
                                FechaEntrada = entrada.fechaEntrada,
                                HoraEntrada = entrada.horaEntrada,
                                FechaSalida = salidaCorrespondiente.fechaSalida,
                                HoraSalida = salidaCorrespondiente.horaSalida,
                                HorasCalculadas = horasTrabajadas.TotalHours
                            });
                        }
                    }

                    // Asignar horas totales al empleado
                    tablaEmpleado.HorasTotales = ((int)totalHoras.TotalHours);
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine($"Error procesando empleado {empleado.idEmpleado}: {ex.Message}");
                }

                tabla.Add(tablaEmpleado);
            }

            return tabla;
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
                    response = await ReporteIngresos.MostrarHorasEmpleados(DtpFecha.Value.ToString("yyyy/MM/dd"), Convert.ToInt64(numeroDocumento));

                }
                else
                {
                    response = null;

                }
               
                string content = await response.Content.ReadAsStringAsync();
                ReporteIngresos.ApiResponse ingresos = JsonConvert.DeserializeObject<ReporteIngresos.ApiResponse>(content);
                var data = ProcesarDatos(ingresos);
                DgvIngresos.DataSource = data;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encontró referencia de datos");
                DgvIngresos.DataSource = dt;
                MessageBox.Show("No se encontro el numero de documento del usuario");

            }


        }

        private async void BtnData_Click(object sender, EventArgs e)
        {
            try
            {
                HttpResponseMessage response = await ReporteIngresos.MostrarIngresosTotales(DateTime.Now.ToString("yyyy/MM"));
                string content = await response.Content.ReadAsStringAsync();
                ReporteIngresos.ApiResponse ingresos = JsonConvert.DeserializeObject<ReporteIngresos.ApiResponse>(content);
                var data = ProcesarDatos(ingresos);
                DgvIngresos.DataSource = data;
            }
            catch (Exception ex)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Data");
                dt.Rows.Add("No se encuentran datos registrados");
                DgvIngresos.DataSource = dt;

            }
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
                        table1.AddCell(new Cell().Add(new Paragraph("REPORTES DE DATOS")).SetBold().SetBackgroundColor(ColorConstants.YELLOW).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12).SetWidth(UnitValue.CreatePercentValue(100)).SetPaddingTop(15) .SetPaddingBottom(15) .SetBold());
                        document.Add(table1);
                            
                            
                        

                         

                        // Encabezados de las columnas
                        table.AddCell(new Cell().Add(new Paragraph("ID EMPLEADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY) .SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE) .SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("NOMBRE EMPLEADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("AREA EMPLEADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("TIPO DOCUMENTO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("NUMERO DOCUMENTO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("CARGO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("RH")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("ESTADO")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        table.AddCell(new Cell().Add(new Paragraph("HORAS CUMPLIDAS")).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetVerticalAlignment(verticalAlignment: VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));


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
                                    .SetWidth(50)
                                    .SetFontSize(8)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHoraActual.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}

