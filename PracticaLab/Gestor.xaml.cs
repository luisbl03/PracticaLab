using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace PracticaLab
{
    public partial class Gestor : Window
    {
        public List<Paciente> Pacientes { get; set; }
        public Usuario usuario { get; set; }
        private void Gestor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Ajustar dinámicamente el ancho de la segunda columna en proporción
            double nw_gridAbajoMitadInf = e.NewSize.Width * 0.78;
            double nh_gridAbajoMitadInf = e.NewSize.Height * 0.78;
            // Establecer la nueva anchura
            gridInferior.ColumnDefinitions[1].Width = new GridLength(nw_gridAbajoMitadInf);
            gridPrincipal.RowDefinitions[1].Height = new GridLength(nh_gridAbajoMitadInf);
        }
        //hay un paciente selecionado
        private bool seleccionado = false;
        private void Lista_de_pacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay un paciente seleccionado
            if (Lista_de_pacientes.SelectedItem != null)
            {
                seleccionado = true;
                // Obtén el paciente seleccionado
                Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;

                // Muestra los detalles del paciente en el TextBox

                // Muestra los detalles del paciente en los TextBox respectivos
                txtNombre.Text = $"{pacienteSeleccionado.Nombre}";
                txtApellido1.Text = $"{pacienteSeleccionado.Apellido1}";
                txtApellido2.Text = $"{pacienteSeleccionado.Apellido2}";
                txtDNI.Text = $"{pacienteSeleccionado.DNI}";
                txtTelefono.Text = $"{pacienteSeleccionado.Telefono}";
                txtDireccion.Text = $"{pacienteSeleccionado.Direccion}";
            }

        }

        // Para saber que en estado esta el boton
        private bool modo1 = true;
        private void bnEdicion_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (modo1==true)
            {
                txtNombre.IsReadOnly = false;
                txtApellido1.IsReadOnly = false;
                txtApellido2.IsReadOnly = false;
                txtDNI.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
                txtDireccion.IsReadOnly = false;
                btn.Content = "Guardar";
            }
            else {
                txtNombre.IsReadOnly = true;
                txtApellido1.IsReadOnly = true;
                txtApellido2.IsReadOnly = true;
                txtDNI.IsReadOnly = true;
                txtTelefono.IsReadOnly = true;
                txtDireccion.IsReadOnly = true;
                btn.Content = "Editar";
                // Guardar los cambios en el paciente (no se guarda en la persistencia)
                if (seleccionado == true)
                {
                    Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;
                    pacienteSeleccionado.Nombre = txtNombre.Text;
                    pacienteSeleccionado.Apellido1 = txtApellido1.Text;
                    pacienteSeleccionado.Apellido2 = txtApellido2.Text;
                    pacienteSeleccionado.DNI = txtDNI.Text;
                    pacienteSeleccionado.Telefono = txtTelefono.Text;
                    pacienteSeleccionado.Direccion = txtDireccion.Text;
                }
            }
            modo1 = !modo1;

        }
        public Gestor(Usuario u)
        {
            InitializeComponent();

            Pacientes = new List<Paciente>();
            usuario = u;

            //Usamos el usuariio para poner sus datos
            this.DataContext = usuario;
            try
            {

                // Crear un objeto XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                XmlDocument xmlDocCitas = new XmlDocument();
                //almacenamos la informacion de "pacientes.xml" en la variable fichero
                var fichero = Application.GetResourceStream(new Uri("Datos/pacientes.xml", UriKind.Relative));
                var fichero2 = Application.GetResourceStream(new Uri("Datos/citas.xml", UriKind.Relative));
                // Cargar el documento XML desde el archivo
                xmlDoc.Load(fichero.Stream);
                xmlDocCitas.Load(fichero2.Stream);

                // Obtener la lista de nodos de pacientes
                XmlNodeList pacientesXml = xmlDoc.SelectNodes("/Pacientes/Paciente");

                // Iterar a través de los pacientes y agregar a la lista de Pacientes
                foreach (XmlNode pacienteXml in pacientesXml)
                {
                    Paciente paciente = new Paciente
                    {
                        Nombre = pacienteXml.SelectSingleNode("Nombre").InnerText,
                        Apellido1 = pacienteXml.SelectSingleNode("Apellido1").InnerText,
                        Apellido2 = pacienteXml.SelectSingleNode("Apellido2").InnerText,
                        DNI = pacienteXml.SelectSingleNode("DNI").InnerText,
                        Telefono = pacienteXml.SelectSingleNode("Telefono").InnerText,
                        Direccion = pacienteXml.SelectSingleNode("Direccion").InnerText
                    };
                    XmlNode citaXml = xmlDocCitas.SelectSingleNode($"/Citas/Cita/Paciente[DNI='{paciente.DNI}']");
                    if (citaXml != null)
                    {
                        string fechaCita = citaXml.SelectSingleNode("Fecha").InnerText;
                        paciente.FechaCita = DateTime.ParseExact(fechaCita, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    }
                    Pacientes.Add(paciente);
                }

                // Asignar la lista de pacientes como origen de datos para el ListBox
                Lista_de_pacientes.ItemsSource = Pacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtCodigoPostaly_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}