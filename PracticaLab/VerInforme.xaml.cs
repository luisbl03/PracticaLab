using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
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
    /// Lógica de interacción para EditarInforme.xaml
    /// </summary>
    public partial class VerInforme : Window
    {
        public Paciente PacienteSeleccionado { get; set; }
        public Informe InformeSeleccionado { get; set; }

        public VerInforme(Paciente paciente, Informe informe)
        {
            InitializeComponent();

            PacienteSeleccionado = paciente;
            InformeSeleccionado = informe;

            // Verifica si hay un informe seleccionado
            if (InformeSeleccionado != null)
            {
                txtDolencias.Text = InformeSeleccionado.Descripcion;
            }
        }
    }
}
