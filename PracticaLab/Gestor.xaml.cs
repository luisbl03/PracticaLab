﻿    using System;
    using System.Collections.Generic;
    using System.Windows;
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
        public Gestor(Usuario u)
            {
                InitializeComponent();

                Pacientes = new List<Paciente>();
                usuario = u;

                try
                {

                    // Crear un objeto XmlDocument
                    XmlDocument xmlDoc = new XmlDocument();
                    
                    //almacenamos la informacion de "pacientes.xml" en la variable fichero
                    var fichero = Application.GetResourceStream(new Uri("Datos/pacientes.xml", UriKind.Relative));
                  
                    // Cargar el documento XML desde el archivo
                    xmlDoc.Load(fichero.Stream);

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

        private void txtCodigoPostaly_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
    }
