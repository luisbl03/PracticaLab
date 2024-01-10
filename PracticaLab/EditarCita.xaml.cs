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
        public Paciente paciente { get; set; }
        public Cita cita { get; set; }
        public Page2 page2 { get; set; }
        public Citas_Fisio citas_Fisio { get; set; }
        public EditarCita(Paciente p, Cita c, Page2 page2)
        {
            InitializeComponent();
            this.paciente = p;
            this.cita = c;
            this.page2 = page2;
            txtPaciente.Text = paciente.Nombre;
            txtMotivo.Text = cita.motivo;
            dateSelector.SelectedDate = cita.fecha.Date;

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

            comboHora.Text = cita.fecha.TimeOfDay.ToString().Substring(0, 5);

        }
        public EditarCita(Paciente p, Cita c, Citas_Fisio citas_Fisio)
        {
            InitializeComponent();
            this.paciente = p;
            this.cita = c;
            this.citas_Fisio = citas_Fisio;
            txtPaciente.Text = paciente.Nombre;
            txtMotivo.Text = cita.motivo;
            dateSelector.SelectedDate = cita.fecha.Date;

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

            comboHora.Text = cita.fecha.TimeOfDay.ToString().Substring(0, 5);

        }

        private void txtMotivo_GotFocus(object sender, RoutedEventArgs e)
        {
            
            txtMotivo.Foreground = Brushes.Black;
            
        }

        private void bttnGuardarCita_Click(object sender, RoutedEventArgs e)
        {
            if (txtMotivo.Text != "Motivo" && txtMotivo.Text != "" && dateSelector.SelectedDate != null && comboHora.SelectedItem != null)
            {
                if (page2 != null)
                {
                    page2.Citas.Remove(cita);
                    cita.motivo = txtMotivo.Text;
                    cita.fecha = (DateTime)dateSelector.SelectedDate;
                    cita.fecha = cita.fecha.Add(TimeSpan.Parse(comboHora.SelectedItem.ToString()));
                    page2.Citas.Add(cita);
                    page2.dataGridCitas.ItemsSource = null;
                    List<Cita> citas = page2.cargarCitasPAciente(paciente, page2.Citas);
                    page2.dataGridCitas.ItemsSource = citas;
                    page2.dataGridCitas.Items.Refresh();
                    MessageBox.Show("Cita modificada correctamente");
                    this.Close();
                }
                else
                {
                    citas_Fisio.Citas.Remove(cita);
                    cita.motivo = txtMotivo.Text;
                    cita.fecha = (DateTime)dateSelector.SelectedDate;
                    cita.fecha = cita.fecha.Add(TimeSpan.Parse(comboHora.SelectedItem.ToString()));
                    citas_Fisio.Citas.Add(cita);
                    citas_Fisio.dataGridCitas.ItemsSource = null;
                    List<Cita> citas = citas_Fisio.cargarCitasPAciente(paciente, citas_Fisio.Citas);
                    citas_Fisio.dataGridCitas.ItemsSource = citas;
                    citas_Fisio.dataGridCitas.Items.Refresh();
                    MessageBox.Show("Cita modificada correctamente");
                    this.Close();
                }
                
                
            }
            else
            {
                /*ponemos en rojo los campos que falten*/
                if (txtMotivo.Text == "Motivo" || txtMotivo.Text == "")
                {
                    txtMotivo.Text = "Motivo";
                    txtMotivo.Foreground = Brushes.Red;
                }
                if (dateSelector.SelectedDate == null)
                {
                    dateSelector.Foreground = Brushes.Red;
                }
                if (comboHora.SelectedItem == null)
                {
                    comboHora.Foreground = Brushes.Red;
                }
            }
        }
    }
}
