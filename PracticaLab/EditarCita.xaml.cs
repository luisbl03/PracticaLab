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
using System.Windows.Shapes;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para EditarCita.xaml
    /// </summary>
    public partial class EditarCita : Window
    {
        public EditarCita(Paciente p, Cita c, Page2 page2)
        {
            InitializeComponent();
        }
    }
}
