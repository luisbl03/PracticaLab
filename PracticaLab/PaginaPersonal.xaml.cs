﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
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
using System.Xml;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public ObservableCollection<Nomina> Nominas { get; set; }
        public List<Trabajadores> trabajadoresList { get; set; }

        public Page1()
        {
            trabajadoresList = new List<Trabajadores>();
            InitializeComponent();
            // Crear un objeto XmlDocument
            try
            {

                // Crear un objeto XmlDocument
                XmlDocument xmlDocTrabajadores = new XmlDocument();
       
                //almacenamos la informacion de "pacientes.xml" en la variable fichero
                var fichero = Application.GetResourceStream(new Uri("Datos/trabajadores.xml", UriKind.Relative));
                // Cargar el documento XML desde el archivo
                xmlDocTrabajadores.Load(fichero.Stream);

                // Obtener la lista de nodos de pacientes
                XmlNodeList trabajadoresXML = xmlDocTrabajadores.SelectNodes("/Trabajadores/Trabajador");

                // Iterar a través de los pacientes y agregar a la lista de Pacientes
                foreach (XmlNode trabajadorXML in trabajadoresXML)
                {
                    string nombre = trabajadorXML.Attributes["Nombre"].Value;
                    string apellido1 = trabajadorXML.Attributes["Apellido1"].Value;
                    string apellido2 = trabajadorXML.Attributes["Apellido2"].Value;
                    string dNI = trabajadorXML.Attributes["DNI"].Value;
                    string telefono = trabajadorXML.Attributes["Telefono"].Value;
                    string direccion = trabajadorXML.Attributes["Direccion"].Value;
                    string correo =trabajadorXML.Attributes["correo"].Value;
                    string trabajo = trabajadorXML.Attributes["trabajo"].Value;
                    Trabajadores trabajador = new Trabajadores(nombre,apellido1, apellido2, dNI, telefono, direccion, correo, trabajo);
                   
                    trabajadoresList.Add(trabajador);
                }

                Lista_trabajadores.ItemsSource = trabajadoresList;
            
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Nomians y demas
            Nominas = new ObservableCollection<Nomina>();
            datagridNominas.ItemsSource = Nominas;
        }

        Boolean seleccionado = false;
        private void Lista_trabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay un paciente seleccionado
            if (Lista_trabajadores.SelectedItem != null)
            {
                //limpiamos el datagrid de citas
                datagridPacientesAtendidos.ItemsSource = null;
                seleccionado = true;
                // Obtén el paciente seleccionado
                Trabajadores trabajadorSeleccionador = (Trabajadores)Lista_trabajadores.SelectedItem;

                // Muestra los detalles del paciente en el TextBox

                // Muestra los detalles del paciente en los TextBox respectivos
                txtNombre.Text = $"{trabajadorSeleccionador.Nombre}";
                txtApellido1.Text = $"{trabajadorSeleccionador.Apellido1}";
                txtApellido2.Text = $"{trabajadorSeleccionador.Apellido2}";
                txtDNI.Text = $"{trabajadorSeleccionador.DNI}";
                txtTelefono.Text = $"{trabajadorSeleccionador.Telefono}";
                txtDireccion.Text = $"{trabajadorSeleccionador.Direccion}";
                txtTrabajo.Text = $"{trabajadorSeleccionador.trabajo}";



                //ImagenPaciente.Source = new BitmapImage(new Uri(pacienteSeleccionado.RutaFoto, UriKind.RelativeOrAbsolute));


                //busco en la lista de ciats aquellas citas del paciente seleccionado y las añado al datagrid
               /*
                List<Cita> citasPaciente = new List<Cita>();
                citasPaciente = cargarCitasPAciente(pacienteSeleccionado, Citas);
                dataGridCitas.ItemsSource = citasPaciente;
                dataGridCitas.Items.Refresh();
                */
            }
        }

        private void cmbTiposTrabajador_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Obtener el tipo de trabajador seleccionado en el ComboBox
            string tipoTrabajador = (cmbTiposTrabajador.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Realizar acciones según el tipo de trabajador seleccionado
            switch (tipoTrabajador)
            {
                case "Sanitario":
                    MostrarInformacionSanitario();
                    break;
                case "Encargado de la Limpieza":
                    MostrarInformacionEncargadoLimpieza();
                    break;
                default:
                    // Manejar otros casos si es necesario
                    break;
            }
        }

        private void MostrarInformacionSanitario()
        {
            // Lógica para mostrar información de un Sanitario
            MessageBox.Show("Información de Sanitario");
        }

        private void MostrarInformacionEncargadoLimpieza()
        {
            // Lógica para mostrar información de un Encargado de la Limpieza
            MessageBox.Show("Información de Encargado de la Limpieza");
        }

        private void datagridPacientesAtendidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_añadir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class Nomina
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
    }
}
