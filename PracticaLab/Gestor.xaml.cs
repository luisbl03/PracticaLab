using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Gestor : Window
    {
        public Gestor()
        {
            InitializeComponent();

            // Crear una lista de 25 elementos con el formato "paciente n"
            List<string> pacientes = new List<string>();
            for (int i = 1; i <= 25; i++)
            {
                pacientes.Add($"Paciente {i}");
            }

            // Asignar la lista como origen de datos para el ListBox
            Lista_de_pacientes.ItemsSource = pacientes;
        }

    }

}
