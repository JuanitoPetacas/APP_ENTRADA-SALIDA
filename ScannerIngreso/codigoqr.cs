using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using QRCoder;
using System.Drawing;
using System.Drawing.Printing;

namespace ScannerIngreso
{
    public partial class codigoqr : Form
    {
        public codigoqr()
        {
            InitializeComponent();
        }
        private Panel panelToprint;

        private void Form2_Load(object sender, EventArgs e)
        {

            nombreUsuario.Text = $"{ClaseVariables.Nombres} {ClaseVariables.Apellidos}".ToUpper();
            CargarQr();
            panelToprint = sticker1;
            this.AutoSize = false; // Evita que el formulario cambie de tamaño automáticamente
            this.AutoScaleMode = AutoScaleMode.None; // Desactiva el escalado automático

        }

        public void CargarQr()
        {

            string nombres = ClaseVariables.Nombres;
            string apellidos = ClaseVariables.Apellidos;
            string tipoId = ClaseVariables.TipoId;
            long ide = ClaseVariables.Identificacion;
            string cargo = ClaseVariables.Cargo;
            string rh = ClaseVariables.RH;
            string datos = $"{nombres} , {apellidos} , {tipoId} , {ide} , {cargo} , {rh}";
            this.SuspendLayout(); // Evita redibujos durante los cambios

            // Datos del QR
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(datos, QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(qrCodeData);

            // Generar imagen QR con tamaño moderado
            using (Bitmap qrCodeImage = qrCode.GetGraphic(250)) // Reduce tamaño para evitar consumo de memoria
            {
                // Convertir a escala de grises para impresión en Zebra
                Bitmap optimizedQR = qrCodeImage.Clone(new Rectangle(0, 0, qrCodeImage.Width, qrCodeImage.Height),
                                                       System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                // Configurar PictureBox
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Width = 1000;
                pictureBox2.Height = 300;
                pictureBox2.Image = new Bitmap(optimizedQR); // Evita fugas de memoria
            }

            this.ResumeLayout(); // Reanuda el redibujo del formulario




        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += Imprimir;
            printDocument1.Print();
        }

        
        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            Panel panelToPrint = sticker1;
            // Crear un Bitmap con la resolución adecuada
            Bitmap panelBitmap = new Bitmap(panelToPrint.Width, panelToPrint.Height);
            panelToPrint.DrawToBitmap(panelBitmap, new Rectangle(0, 0, panelToPrint.Width, panelToPrint.Height));

            // Ajustar la resolución del Bitmap para mejorar calidad de impresión
            panelBitmap.SetResolution(300, 300);

            int pageWidth = e.PageBounds.Width;
            int pageHeight = e.PageBounds.Height;

            // Opcional: Escalar la imagen si es más grande que el área de impresión
            float scaleX = (float)pageWidth / panelBitmap.Width;
            float scaleY = (float)pageHeight / panelBitmap.Height;
            float scale = Math.Min(scaleX, scaleY); // Mantener proporciones

            int printWidth = (int)(panelBitmap.Width * scale);
            int printHeight = (int)(panelBitmap.Height * scale);

            // Calcular posición para centrar en la página
            int x = (pageWidth - printWidth) / 2;
            int y = (pageHeight - printHeight) / 2;

            // Dibujar el Bitmap en la página centrado
            e.Graphics.DrawImage(panelBitmap, x, y, printWidth, printHeight);

        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
