﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Citas_Fisio : Page
    {
        private bool isInitialized = false;
        private void PaginaPacientes_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            // Ajustar dinámicamente el ancho y alto de pacientes

            double nw_gridAbajoMitadInf = e.NewSize.Width * 0.79;

            GridPacientes.ColumnDefinitions[1].Width = new GridLength(nw_gridAbajoMitadInf);


        }

        public List<Paciente> Pacientes { get; set; }
        //hay un paciente selecionado
        private bool seleccionado = false;
        private void Lista_de_pacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
            {
                return;
            }

            //listViewInformes.Items.Clear();

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



                ImagenPaciente.Source = new BitmapImage(new Uri(pacienteSeleccionado.RutaFoto, UriKind.RelativeOrAbsolute));

                //listViewInformes.Items.Clear();

                // Añadir los informes del paciente seleccionado a la ListView
                UpdateInformesList(pacienteSeleccionado);
            }
            else
            {
                // No hay paciente seleccionado, no es necesario agregar informes, solo actualizar la visibilidad
                listViewInformes.Visibility = Visibility.Visible;
            }
        }
        // Para saber que en estado esta el boton
        private bool modo1 = true;

        private void bnEdicion_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (modo1 == true)
            {
                txtNombre.IsReadOnly = false;
                txtApellido1.IsReadOnly = false;
                txtApellido2.IsReadOnly = false;
                txtDNI.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
                txtDireccion.IsReadOnly = false;
                btn.Content = "Guardar";
            }
            else
            {
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
        public Citas_Fisio(Usuario u)
        {
            InitializeComponent();
            Pacientes = new List<Paciente>();
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

                    foreach (XmlNode informeXml in pacienteXml.SelectNodes("Informes/Informe"))
                    {
                        Informe informe = new Informe
                        {
                            Descripcion = informeXml?.InnerText,
                            FechaInforme = DateTime.Now // Puedes ajustar la fecha según tus necesidades
                        };

                        paciente.Informes.Add(informe);
                        listViewInformes.Items.Add(new
                        {
                            IdInforme = paciente.Informes.Count, // Puedes ajustar esto según tu lógica
                            FechaInforme = informe.FechaInforme.ToString("dd/MM/yyyy"),
                            Descripcion = informe.Descripcion
                        });
                    }

                    switch (paciente.DNI)
                    {
                        case "12345678A":
                            paciente.RutaFoto = "/Imagenes/Imagenes_pacientes/Paciente1.png";
                            break;
                        case "12456478Q":
                            paciente.RutaFoto = "/Imagenes/Imagenes_pacientes/Paciente2.png";
                            break;
                        case "67890123F":
                            paciente.RutaFoto = "/Imagenes/Imagenes_pacientes/PacienteChica.png";
                            break;
                        default:
                            paciente.RutaFoto = "/Imagenes/Imagenes_pacientes/Predeterminado.png";
                            break;
                    }
                    Pacientes.Add(paciente);
                }
                List<Cita> listaCitas = new List<Cita>();
                listaCitas = cargarCitas(listaCitas);
                List<Paciente> lista_paciente_con_cita = new List<Paciente>();
                List<Paciente> lista_paciente_sin_cita = new List<Paciente>();
                foreach (Cita cita in listaCitas)
                {
                    if (cita.correo_fisio.Equals(u.correo))
                    {
                        Paciente p = Pacientes.FirstOrDefault(p2 => p2.DNI == cita.DNI_paciente);
                        if (p != null)
                        {
                            lista_paciente_con_cita.Add(p);
                        }
                    }
                    else
                    {
                        Paciente p = Pacientes.FirstOrDefault(p2 => p2.DNI == cita.DNI_paciente);
                        if (p != null)
                        {
                            lista_paciente_sin_cita.Add(p);
                        }
                    }
                }
                lista_paciente_con_cita = lista_paciente_con_cita.OrderBy(p => p.FechaCita).ToList();


                this.DataContext = this;
                isInitialized = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateInformesList(Paciente paciente = null)
        {
            // Limpiar la ListView de informes
            listViewInformes.Items.Clear();

            if (paciente != null)
            {
                // Añadir los informes del paciente seleccionado a la ListView
                foreach (Informe informe in paciente.Informes)
                {
                    listViewInformes.Items.Add(new
                    {
                        IdInforme = paciente.Informes.IndexOf(informe) + 1, // Ajusta esto según tu lógica
                        FechaInforme = informe.FechaInforme.ToString("dd/MM/yyyy"),
                        Descripcion = informe.Descripcion
                    });
                }
                listViewInformes.Visibility = paciente.Informes.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private void editarInforme_Click(object sender, RoutedEventArgs e)
        {
            EditarInforme editarInformeWindow = new EditarInforme();
            editarInformeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editarInformeWindow.ShowDialog();
        }

        private void anadirInforme_Click(object sender, RoutedEventArgs e)
        {
            AnadirInforme anadirInformeWindow = new AnadirInforme();
            anadirInformeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            anadirInformeWindow.ShowDialog();
        }

        private void verInforme_Click(object sender, RoutedEventArgs e)
        {
            VerInforme verInformeWindow = new VerInforme();
            verInformeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            verInformeWindow.ShowDialog();
        }

        private List<Cita> cargarCitas(List<Cita> listaCitas)
        {
            // Crear un objeto XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            //almacenamos la informacion de "pacientes.xml" en la variable fichero
            var fichero = Application.GetResourceStream(new Uri("Datos/citas.xml", UriKind.Relative));
            // Cargar el documento XML desde el archivo
            xmlDoc.Load(fichero.Stream);

            // Obtener la lista de nodos de pacientes
            XmlNodeList citasXml = xmlDoc.SelectNodes("/Citas/Cita");

            // Iterar a través de los pacientes y agregar a la lista de Pacientes
            foreach (XmlNode citaXml in citasXml)
            {
                Cita cita = new Cita("", "", DateTime.Now, "")
                {
                    DNI_paciente = citaXml.SelectSingleNode("DNI").InnerText,
                    motivo = citaXml.SelectSingleNode("Motivo").InnerText,
                    fecha = DateTime.ParseExact(citaXml.SelectSingleNode("Fecha").InnerText, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    correo_fisio = citaXml.SelectSingleNode("CorreoFisio").InnerText
                };
                listaCitas.Add(cita);
            }
            return listaCitas;
        }

    }
}