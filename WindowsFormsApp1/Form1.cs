using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> selectedFiles = new List<string>(openFileDialog.FileNames);
                MergePDFs(selectedFiles);
            }
        }

        private void MergePDFs(List<string> fileNames)
        {
            Document document = new Document();
            PdfCopy pdfCopy = new PdfCopy(document, new FileStream("MergedPDF.pdf", FileMode.Create));
            document.Open();
            foreach (string fileName in fileNames)
            {
                PdfReader pdfReader = new PdfReader(fileName);
                for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    PdfImportedPage page = pdfCopy.GetImportedPage(pdfReader, i);
                    pdfCopy.AddPage(page);
                }
                pdfReader.Close();
            }
            document.Close();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FileName = "MergedPDF.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.Copy("MergedPDF.pdf", saveFileDialog.FileName, true);
                System.IO.File.Delete("MergedPDF.pdf");
            }
        }
        

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the OpenFileDialog dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to allow only PDF files
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            // Show the dialog and get the result
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Get the path of the selected file
                string filePath = openFileDialog.FileName;

                // Create a ProcessStartInfo object to specify the process to start
                ProcessStartInfo startInfo = new ProcessStartInfo();

                // Set the file name and arguments to open the selected file with the default program
                startInfo.FileName = filePath;
                startInfo.UseShellExecute = true;

                // Start the process
                Process.Start(startInfo);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Love Makes You Happy!";
            label1.ForeColor = Color.Red;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Update();
            Timer timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += (s, b) =>
            {
                label1.BackColor = Color.Transparent;
                label1.Text = "Избери PDF за сливане";
                label1.ForeColor = Color.Black;
                label1.Size = new Size(200, 20);
                label1.TextAlign = ContentAlignment.MiddleLeft;
                timer.Stop();
            };
            timer.Start();
        }

    }
}
