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
    /// Lógica de interacción para VerCita.xaml
    /// </summary>
    public partial class VerCita : Window
    {
        public VerCita(Cita c)
        {
            InitializeComponent();
            txtPaciente.Text = c.nombre_paciente;
            txtMotivo.Text = c.motivo;
            dateSelector.SelectedDate = c.fecha.Date;
            comboHora.IsReadOnly = false;
            comboHora.Items.Add("09:00");
            comboHora.Items.Add("10:00");
            comboHora.Items.Add("11:00");
            comboHora.Items.Add("12:00");
            comboHora.Items.Add("13:00");
            comboHora.Items.Add("16:00");
            comboHora.Items.Add("17:00");
            comboHora.Items.Add("18:00");
            comboHora.Items.Add("19:00");
            comboHora.Items.Add("20:00");
            comboHora.Text = c.fecha.TimeOfDay.ToString().Substring(0, 5);
            comboHora.IsReadOnly = true;

        }
    }
}
