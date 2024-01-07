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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private bool isInitialized = false;
        private void PaginaPacientes_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            // Ajustar dinámicamente el ancho y alto de pacientes

            double nw_gridAbajoMitadInf = e.NewSize.Width * 0.79;

            GridPacientes.ColumnDefinitions[1].Width = new GridLength(nw_gridAbajoMitadInf);

            
        }

        public List<Paciente> Pacientes { get; set; }
        public List<Cita> Citas { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Trabajadores> list_trabajadores { get; set; }
        //hay un paciente selecionado
        private bool seleccionado = false;


        private void Lista_de_pacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
            {
                return;
            }

            if (Lista_de_pacientes.SelectedItem != null)
            {
                Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;
                UpdateInformesList(pacienteSeleccionado);
                anadirInforme.IsEnabled = true;
            }
            else
            {
                // No hay paciente seleccionado, deshabilita el botón
                verInforme.IsEnabled = false;
                anadirInforme.IsEnabled = false;

            }
            //listViewInformes.Items.Clear();

            // Verifica si hay un paciente seleccionado
            if (Lista_de_pacientes.SelectedItem != null)
            {

                //limpiamos el datagrid de citas
                dataGridCitas.ItemsSource = null;
                seleccionado = true;
                // Obtén el paciente seleccionado
                Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;

                pacienteSeleccionado.InformesTemporales.ForEach(informeTemporal =>
                {
                    listViewInformes.Items.Add(new
                    {
                        IdInforme = pacienteSeleccionado.InformesTemporales.IndexOf(informeTemporal) + 1,
                        FechaInforme = informeTemporal.FechaInforme.ToString("dd/MM/yyyy"),
                        Descripcion = informeTemporal.Descripcion
                    });
                });

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
                //busco en la lista de ciats aquellas citas del paciente seleccionado y las añado al datagrid
                List<Cita> citasPaciente = new List<Cita>();
                citasPaciente = cargarCitasPAciente(pacienteSeleccionado, Citas);
                dataGridCitas.ItemsSource = citasPaciente;
                dataGridCitas.Items.Refresh();
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
                /*vemos el contenido de los campos, si el usuario existe, actualizamos sus valores, si no, lo añadimos*/
                if (txtNombre.Text != "" && txtApellido1.Text != "" && txtApellido2.Text != "" && txtDNI.Text != "" && txtTelefono.Text != "" && txtDireccion.Text != "")
                {
                    if (seleccionado == true)
                    {
                        Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;
                        pacienteSeleccionado.Nombre = txtNombre.Text;
                        pacienteSeleccionado.Apellido1 = txtApellido1.Text;
                        pacienteSeleccionado.Apellido2 = txtApellido2.Text;
                        pacienteSeleccionado.DNI = txtDNI.Text;
                        pacienteSeleccionado.Telefono = txtTelefono.Text;
                        pacienteSeleccionado.Direccion = txtDireccion.Text;
                        Lista_de_pacientes.Items.Refresh();
                    }
                    else
                    {
                        Paciente paciente = new Paciente
                        {
                            Nombre = txtNombre.Text,
                            Apellido1 = txtApellido1.Text,
                            Apellido2 = txtApellido2.Text,
                            DNI = txtDNI.Text,
                            Telefono = txtTelefono.Text,
                            Direccion = txtDireccion.Text
                        };
                        paciente.RutaFoto = "/Imagenes/Imagenes_pacientes/Predeterminado.png";
                        Pacientes.Add(paciente);
                        /*añadimos al nuevo paciente en la listbox en orden*/
                        Lista_de_pacientes.Items.Refresh();
                        Lista_de_pacientes.SelectedItem = paciente;
                    }
                }
                else
                {
                    if (txtNombre.Text == "")
                    {
                        txtNombre.Foreground = Brushes.Red;
                    }
                    if (txtApellido1.Text == "")
                    {
                        txtApellido1.Foreground = Brushes.Red;
                    }
                    if (txtApellido2.Text == "")
                    {
                        txtApellido2.Foreground = Brushes.Red;
                    }
                    if (txtDNI.Text == "")
                    {
                        txtDNI.Foreground = Brushes.Red;
                    }
                    if (txtTelefono.Text == "")
                    {
                        txtTelefono.Foreground = Brushes.Red;
                    }
                    if (txtDireccion.Text == "")
                    {
                        txtDireccion.Foreground = Brushes.Red;
                    }
                }
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


        public Page2(Usuario u)
        {   

            InitializeComponent();
            Pacientes = new List<Paciente>();
            Citas = cargarCitas();
            Usuarios = cargarUsuarios();
            list_trabajadores = cargarTrabajadores();
            listViewInformes.SelectionChanged += listViewInformes_SelectionChanged;


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
                Lista_de_pacientes.ItemsSource = Pacientes;
                listViewInformes.Visibility = Visibility.Visible;
                listViewInformes.Items.Clear();

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
                listViewInformes.Visibility = paciente.Informes.Any() ? Visibility.Visible : Visibility.Visible;
            }
        }
        private void editarInforme_Click(object sender, RoutedEventArgs e)
        {
            if (listViewInformes.SelectedItem != null)
            {
                Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;

                Informe informeSeleccionado = pacienteSeleccionado.Informes[listViewInformes.SelectedIndex];

                EditarInforme editarInformeWindow = new EditarInforme(pacienteSeleccionado, informeSeleccionado);
                editarInformeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                editarInformeWindow.ShowDialog();
            }
        }

        private void anadirInforme_Click(object sender, RoutedEventArgs e)
        {
            if (Lista_de_pacientes.SelectedItem != null)
            {
                Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;

                // Crear una nueva instancia de AnadirInforme y pasar la referencia de Page2
                AnadirInforme anadirInformeWindow = new AnadirInforme(this, pacienteSeleccionado);
                anadirInformeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                anadirInformeWindow.ShowDialog();
            }
        }

        public void UpdateInformesListAfterAdd(Paciente paciente)
        {
            UpdateInformesList(paciente);
        }


        private void verInforme_Click(object sender, RoutedEventArgs e)
        {
            if (listViewInformes.SelectedItem != null)
            {
                Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;

                Informe informeSeleccionado = pacienteSeleccionado.Informes[listViewInformes.SelectedIndex];

                VerInforme verInformeWindow = new VerInforme(pacienteSeleccionado, informeSeleccionado);
                verInformeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                verInformeWindow.ShowDialog();
            }
        }


        private void listViewInformes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Manejar la habilitación del botón aquí
            if (listViewInformes.SelectedItem != null)
            {
                verInforme.IsEnabled = true;
                editarInforme.IsEnabled= true;
                eliminarInforme.IsEnabled= true;
            }
            else
            {
                verInforme.IsEnabled = false;
                editarInforme.IsEnabled= false;
                eliminarInforme.IsEnabled = false;
            }
        }


        private void eliminarInforme_Click(object sender, RoutedEventArgs e)
        {
            Paciente pacienteSeleccionado = (Paciente)Lista_de_pacientes.SelectedItem;
            if (listViewInformes.SelectedItem != null )
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que quieres eliminar este informe?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var informeSeleccionado = (dynamic)listViewInformes.SelectedItem;
                    int idInforme = informeSeleccionado.IdInforme;

                    Informe informe = pacienteSeleccionado.Informes[idInforme - 1];
                    pacienteSeleccionado.Informes.Remove(informe);
                    UpdateInformesList(pacienteSeleccionado);
                }
            }

        }

        




        private void bttn_añadir_Click(object sender, RoutedEventArgs e)
        {   
            listViewInformes.Items.Clear();
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtDNI.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            ImagenPaciente.Source = new BitmapImage(new Uri("/Imagenes/Imagenes_pacientes/Predeterminado.png", UriKind.RelativeOrAbsolute));
            txtNombre.IsReadOnly = false;
            txtApellido1.IsReadOnly = false;
            txtApellido2.IsReadOnly = false;
            txtDNI.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            txtDireccion.IsReadOnly = false;
            txtNombre.Foreground = Brushes.Black;
            txtApellido1.Foreground = Brushes.Black;
            txtApellido2.Foreground = Brushes.Black;
            txtDNI.Foreground = Brushes.Black;
            txtTelefono.Foreground = Brushes.Black;
            txtDireccion.Foreground = Brushes.Black;
            modo1 = false;
            bttn_Editar.Content = "Guardar";
            dataGridCitas.ItemsSource = null;
            /*quitamos la seleccion de la lista*/
            Lista_de_pacientes.SelectedItem = null;
            seleccionado = false;
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            var paciente= (Paciente)Lista_de_pacientes.SelectedItem;
            Pacientes.Remove(paciente);
            Lista_de_pacientes.Items.Refresh();
            /*dejamos los campos por defecto*/
            listViewInformes.Items.Clear();
            dataGridCitas.ItemsSource = null;
            limpiar();

        }
        private void limpiar()
        {
            txtNombre.Text = "Nombre";
            txtApellido1.Text = "Apellido1";
            txtApellido2.Text = "Apellido2";
            txtDNI.Text = "DNI";
            txtTelefono.Text = "Telefono";
            txtDireccion.Text = "Direccion";
            ImagenPaciente.Source = new BitmapImage(new Uri("/Imagenes/Imagenes_pacientes/Predeterminado.png", UriKind.RelativeOrAbsolute));
            txtNombre.IsReadOnly = true;
            txtApellido1.IsReadOnly = true;
            txtApellido2.IsReadOnly = true;
            txtDNI.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            txtDireccion.IsReadOnly = true;
            txtNombre.Foreground = Brushes.Gray;
            txtApellido1.Foreground = Brushes.Gray;
            txtApellido2.Foreground = Brushes.Gray;
            txtDNI.Foreground = Brushes.Gray;
            txtTelefono.Foreground = Brushes.Gray;
            txtDireccion.Foreground = Brushes.Gray;
            bttn_Editar.Content = "Editar";
        }
        private List<Cita> cargarCitas()
        {
            List<Cita> citas = new List<Cita>();
            try
            {
                // Crear un objeto XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                //almacenamos la informacion de "pacientes.xml" en la variable fichero
                var fichero = Application.GetResourceStream(new Uri("Datos/citas.xml", UriKind.Relative));
                // Cargar el documento XML desde el archivo
                xmlDoc.Load(fichero.Stream);
                //ahora vamos leyendo citas de un determinado paciente, lo comprobamos con el nombre del paciente
                
                foreach(XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    var cita = new Cita("", "","", DateTime.Now, "", "");
                    
                    cita.DNI_paciente = node.Attributes["DNI"].Value;
                    cita.motivo = node.Attributes["Motivo"].Value;
                    cita.fecha = DateTime.ParseExact(node.Attributes["Fecha"].Value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    cita.correo_fisio = node.Attributes["CorreoFisio"].Value;
                    citas.Add(cita);
                }
                return citas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return citas;
            }
        }

        private void eliminarCita_Click(object sender, RoutedEventArgs e)
        {
            var cita = (Cita)dataGridCitas.SelectedItem;

            MessageBoxResult result = MessageBox.Show("¿Estás seguro que quieres eliminar esta cita?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Remueve la cita de la lista 'Citas'
                Citas.Remove(cita);

                // Actualiza el DataGrid volviendo a asignar la lista modificada a ItemsSource
                dataGridCitas.ItemsSource = null;
                List<Cita> citasPaciente = new List<Cita>();
                citasPaciente = cargarCitasPAciente((Paciente)Lista_de_pacientes.SelectedItem, Citas);
                dataGridCitas.ItemsSource = citasPaciente;
            }

        }
        private List<Usuario> cargarUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/usuarios.xml", UriKind.Relative));
            doc.Load(fichero.Stream);

            /*bucle de asignacion de valores*/
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var usuario = new Usuario("", "", 0, "", "");
                usuario.nombre = node.Attributes["Nombre"].Value;
                usuario.apellidos = node.Attributes["Apellidos"].Value;
                usuario.numTelefono = long.Parse(node.Attributes["telefono"].Value);
                usuario.correo = node.Attributes["correo"].Value;
                usuario.contraseña = node.Attributes["contraseña"].Value;
                if (node.Attributes["admin"].Value.Equals("T"))
                {
                    usuario.admin = true;
                }
                else
                {
                    usuario.admin = false;
                }

                listaUsuarios.Add(usuario);

            }
            return listaUsuarios;
        }
        public List<Cita> cargarCitasPAciente(Paciente p, List<Cita> citas )
        {
            List<Cita> citaPaciente = new List<Cita>();
            foreach(Cita c in citas)
            {
                if(c.DNI_paciente == p.DNI)
                {
                    c.nombre_paciente = p.Nombre + " " + p.Apellido1 + " " + p.Apellido2;
                    Trabajadores t = list_trabajadores.Find(x => x.correo == c.correo_fisio);
                    if (t != null)
                    {
                        c.nombre_fisio = t.Nombre + " " + t.Apellido1 + " " + t.Apellido2;
                        citaPaciente.Add(c);
                    }
                }
            }
            return citaPaciente;
        }

        private void dataGridCitas_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridCitas.SelectedItem != null)
            {
                btneliminarCita.IsEnabled = true;
                btneditarCita.IsEnabled = true;
                btnanadirCita.IsEnabled = true;
                btnverCita.IsEnabled = true;
            }
            else
            {
                btneliminarCita.IsEnabled = false;
                btneditarCita.IsEnabled = false;
                btnanadirCita.IsEnabled = true;
                btnverCita.IsEnabled = false;
            }
        }

        private List<Trabajadores> cargarTrabajadores()
        {
            List<Trabajadores> listaTrabajadores = new List<Trabajadores>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/trabajadores.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach(XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var trabajador = new Trabajadores("", "","", "", "","","","");
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

        private void btnanadirCita_Click(object sender, RoutedEventArgs e)
        {
            /*escondemos esta ventana y sacamos la de añadir cita*/
            Paciente p = (Paciente)Lista_de_pacientes.SelectedItem;
            anadirCita anadirCitaWindow = new anadirCita(p, this);
            anadirCitaWindow.Show();
            

        }
        
    }
   
}
