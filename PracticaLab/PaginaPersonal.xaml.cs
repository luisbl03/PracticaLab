﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public Page1()
        {
            InitializeComponent();
            // Crear un objeto XmlDocument
            XmlDocument xmlDocPersonal = new XmlDocument();

            //almacenamos la informacion de "pacientes.xml" en la variable fichero
            // var fichero = Application.GetResourceStream(new Uri(" \\Datos\\personal.xml", UriKind.Relative));

            // Cargar el documento XML desde el archivo
            // xmlDocPersonal.Load(fichero.Stream);

            // Obtener la lista de nodos del personale
            // XmlNodeList personal = xmlDocPersonal.SelectNodes("/Personal/Personal");
            Nominas = new ObservableCollection<Nomina>();
            datagridNominas.ItemsSource = Nominas;
        }

        private void Lista_trabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Sanitarios(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Limpieza(object sender, RoutedEventArgs e)
        {

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
    }
    public class Nomina
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
    }
}
