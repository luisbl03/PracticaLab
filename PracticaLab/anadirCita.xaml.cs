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
using System.Xml;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para anadirCita.xaml
    /// </summary>
    public partial class anadirCita : Window
    {
        public event EventHandler ValorInicializado;
        public Cita cita { get; set; }
        public Page2 page2 { get; set; }
        public Paciente paciente { get; set; }
        public List<Trabajadores> listaTrabajadores { get; set; }
        public anadirCita(Paciente p, Page2 page2)
        {
            this.paciente = p;
            this.page2 = page2;
            InitializeComponent();
            listaTrabajadores = cargarTrabajadores();
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

                /*pillamos un trabajador aleatorio cuyo trabajo no sea encargado de la limpieza*/
                Random r = new Random();
                Trabajadores trabajador = listaTrabajadores[r.Next(0, listaTrabajadores.Count)];
                while (trabajador.trabajo.Equals("limpiador"))
                {
                    trabajador = listaTrabajadores[r.Next(0, listaTrabajadores.Count)];
                }
                DateTime fecha = dateSelector.SelectedDate.Value;
                if (DateTime.TryParseExact(comboHora.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime hora))
                {
                    DateTime resultado = fecha.Date + hora.TimeOfDay;
                    cita = new Cita(txtPaciente.Text, paciente.DNI,txtMotivo.Text, resultado,trabajador.correo, trabajador.Nombre);
                    MessageBox.Show("Cita añadida correctamente");
                    page2.Citas.Add(cita);
                    /*lo añadimos al datagrid*/
                    page2.dataGridCitas.ItemsSource = null;
                    List<Cita> citas = page2.cargarCitasPAciente(paciente,page2.Citas);
                    page2.dataGridCitas.ItemsSource = citas;
                    page2.dataGridCitas.Items.Refresh();
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
        private List<Trabajadores> cargarTrabajadores()
        {
            List<Trabajadores> listaTrabajadores = new List<Trabajadores>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/trabajadores.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var trabajador = new Trabajadores("", "", "", "", "", "", "", "");
                trabajador.Nombre = node.Attributes["Nombre"].Value;
                trabajador.Apellido1 = node.Attributes["Apellido1"].Value;
                trabajador.Apellido2 = node.Attributes["Apellido2"].Value;
                trabajador.DNI = node.Attributes["DNI"].Value;
                trabajador.Telefono = node.Attributes["Telefono"].Value;
                trabajador.Direccion = node.Attributes["Direccion"].Value;
                trabajador.correo = node.Attributes["correo"].Value;
                trabajador.trabajo = node.Attributes["trabajo"].Value;
                listaTrabajadores.Add(trabajador);
            }
            return listaTrabajadores;
        }

        private void txtMotivo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMotivo.Text.Equals("Motivo") || txtMotivo.Text == "")
            {
                txtMotivo.Text = "";
                txtMotivo.Foreground = Brushes.Black;
            }
        }
    }
}
