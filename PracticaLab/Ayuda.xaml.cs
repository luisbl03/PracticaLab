﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para Ayuda.xaml
    /// </summary>
    public partial class Ayuda : Window
    {
        public Ayuda()
        {
            InitializeComponent();
        }

        private void bttnVer_Tutorial_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://pruebasaluuclm-my.sharepoint.com/:v:/r/personal/luis_benito_alu_uclm_es/Documents/tutorial%20de%20uso.mkv?csf=1&web=1&nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJPbmVEcml2ZUZvckJ1c2luZXNzIiwicmVmZXJyYWxBcHBQbGF0Zm9ybSI6IldlYiIsInJlZmVycmFsTW9kZSI6InZpZXciLCJyZWZlcnJhbFZpZXciOiJNeUZpbGVzTGlua0NvcHkifX0&e=7ldLpn"));

        }

        private void bttnVer_Documentacion_Click(object sender, RoutedEventArgs e)
        {
            string pdfFilePath = @"PracticaLab\DocumentacionTrabajoIPO.pdf";
            string fullPath = System.IO.Path.GetFullPath(pdfFilePath);
            //cuando encuentre //bin//debug, lo quita
            pdfFilePath = fullPath.Replace(@"PracticaLab\bin\Debug", "");
            try
            {
                Process.Start(new ProcessStartInfo(pdfFilePath));
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha encontrado el archivo");
            }
           
        }
    }
}
