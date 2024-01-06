using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica de interacción para anadirCita.xaml
    /// </summary>
    public partial class anadirCita : Window
    {
        public Cita cita { get; set; }
        public Paciente paciente { get; set; }
        public Trabajadores trabajador { get; set; }
        public anadirCita(Paciente p, Trabajadores t)
        {
            this.paciente = p;
            this.trabajador = t;
            InitializeComponent();
            txtPaciente.Text = paciente.Nombre;
            /*cargamos unas horas por defecto en el combo box*/
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
        }

        private void bttnGuardarCita_Click(object sender, RoutedEventArgs e)
        {
            /*miramos si todos los campos tienen contenido y se ha fijado fecha*/
            if (txtPaciente.Text != "" && !txtPaciente.Text.Equals("Paciente") && txtMotivo.Text != "" && !txtMotivo.Text.Equals("Motivo") && dateSelector.SelectedDate != null && comboHora.Text != null)
            {
                DateTime fecha = dateSelector.SelectedDate.Value;
                if (DateTime.TryParseExact(comboHora.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime hora))
                {
                    DateTime resultado = fecha.Date + hora.TimeOfDay;
                    cita = new Cita(txtPaciente.Text, paciente.DNI,txtMotivo.Text, resultado,trabajador.correo, trabajador.Nombre);
                    MessageBox.Show("Cita añadida correctamente");
                }
                else
                {
                    MessageBox.Show("La hora no es valida");
                }
            }
            /*ahora en los campos que esten vacios o tengan su nombre por defecto los ponemos en rojo*/
            if (txtPaciente.Text == "" || txtPaciente.Text.Equals("Paciente"))
            {
                txtPaciente.BorderBrush = Brushes.Red;
            }
            if (txtMotivo.Text == "" || txtMotivo.Text.Equals("Motivo"))
            {
                txtMotivo.BorderBrush = Brushes.Red;
            }
            if (dateSelector.SelectedDate == null)
            {
                dateSelector.BorderBrush = Brushes.Red;
            }
            if (comboHora.Text == null)
            {
                comboHora.BorderBrush = Brushes.Red;
            }
        }

        private void bttnCancelarCIta_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que quieres cancelar?", "Cancelar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }
    }
}
