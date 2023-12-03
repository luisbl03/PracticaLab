    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Xml;

    namespace PracticaLab
    {
        public partial class Gestor : Window
        {
            public List<Paciente> Pacientes { get; set; }

            public Gestor()
            {
                InitializeComponent();

                Pacientes = new List<Paciente>();

                try
                {
                    // Ruta al archivo pacientes.xml
                    string rutaArchivo = "C:\\Users\\jcc20\\source\\repos\\luideoz\\PracticaLab\\PracticaLab\\Datos\\pacientes.xml"; // Poned vuestra ruta

                    // Crear un objeto XmlDocument
                    XmlDocument xmlDoc = new XmlDocument();

                    // Cargar el documento XML desde el archivo
                    xmlDoc.Load(rutaArchivo);

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
        }
    }
