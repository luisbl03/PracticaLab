using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Globalization;
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
        public List<Nomina> listNominas { get; set; }
        public List<Trabajador> listTrabajadoresSanitarios { get; set; }
        public List<Trabajador> listTrabajadoresLimpieza { get; set; }
        public List<Cita> listCitas { get; set; }
        public List<Paciente> listPacientes { get; set; }
        public Page1()
        {
            listCitas = new List<Cita>();
            listTrabajadoresSanitarios = new List<Trabajador>();
            listTrabajadoresLimpieza = new List<Trabajador>();
            listPacientes = new List<Paciente>();
            listNominas = new List<Nomina>();

            listCitas = cargarCitas();
            listPacientes = cargarPacientes();
            listNominas = cargarNominas();


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
                    string correo = trabajadorXML.Attributes["correo"].Value;
                    string trabajo = trabajadorXML.Attributes["trabajo"].Value;
                    string imagenRuta = trabajadorXML.Attributes["ImagenRuta"].Value;

                    Trabajador trabajador;
                    if (imagenRuta == "")
                        trabajador = new Trabajador(nombre, apellido1, apellido2, dNI, telefono, direccion, correo, trabajo);
                    else
                        trabajador = new Trabajador(nombre, apellido1, apellido2, dNI, telefono, direccion, correo, trabajo, imagenRuta);

                    switch (trabajador.trabajo)
                    {
                        case "fisioterapeuta":
                            listTrabajadoresSanitarios.Add(trabajador);
                            break;
                        case "fisioterapeuta en practicas":
                            listTrabajadoresSanitarios.Add(trabajador);
                            break;
                        case "fisioterapeuta jefe":
                            listTrabajadoresSanitarios.Add(trabajador);
                            break;
                        case "limpiador":
                            listTrabajadoresLimpieza.Add(trabajador);
                            break;
                        case "limpiador jefe":
                            listTrabajadoresLimpieza.Add(trabajador);
                            break;
                        default:

                            break;
                    }
                }
                listTrabajadoresSanitarios.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.Ordinal));
                listTrabajadoresLimpieza.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.Ordinal));
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        private void Lista_trabajadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay un paciente seleccionado
            if (Lista_trabajadores.SelectedItem != null)
            {
                //limpiamos el datagrid de citas
                dataGridPacientesAtendidos.ItemsSource = null;
                // Obtén el paciente seleccionado
                Trabajador trabajadorSeleccionador = (Trabajador)Lista_trabajadores.SelectedItem;

                // Muestra los detalles del paciente en el TextBox

                // Muestra los detalles del paciente en los TextBox respectivos
                txtNombre.Text = $"{trabajadorSeleccionador.Nombre}";
                txtApellido1.Text = $"{trabajadorSeleccionador.Apellido1}";
                txtApellido2.Text = $"{trabajadorSeleccionador.Apellido2}";
                txtDNI.Text = $"{trabajadorSeleccionador.DNI}";
                txtTelefono.Text = $"{trabajadorSeleccionador.Telefono}";
                txtDireccion.Text = $"{trabajadorSeleccionador.Direccion}";
                txtTrabajo.Text = $"{trabajadorSeleccionador.trabajo}";

                imgTrabajador.Source = new BitmapImage(new Uri(trabajadorSeleccionador.ImagenRuta, UriKind.RelativeOrAbsolute));

                //ImagenPaciente.Source = new BitmapImage(new Uri(pacienteSeleccionado.RutaFoto, UriKind.RelativeOrAbsolute));


                //busco en la lista de ciats aquellas citas del paciente seleccionado y las añado al datagrid

                dataGridPacientesAtendidos.ItemsSource = cargarCitasAtendidas(listPacientes, listCitas, trabajadorSeleccionador);
                dataGridPacientesAtendidos.Items.Refresh();

                // Nominas
                datagridNominas.ItemsSource = cargarNominaTrabajador(trabajadorSeleccionador, listNominas);
                datagridNominas.Items.Refresh();

            }
        }

        Boolean Sanitario = false;
        private void cmbTiposTrabajador_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Obtener el tipo de trabajador seleccionado en el ComboBox
            string tipoTrabajador = (cmbTiposTrabajador.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Realizar acciones según el tipo de trabajador seleccionado
            switch (tipoTrabajador)
            {
                case "Sanitario":
                    Lista_trabajadores.ItemsSource = listTrabajadoresSanitarios;
                    txtBlockPacientesAtendidos.Visibility = Visibility.Visible;
                    dataGridPacientesAtendidos.Visibility = Visibility.Visible;
                    Sanitario = true;
                    limpiar();
                    break;
                case "Encargado de la Limpieza":
                    Lista_trabajadores.ItemsSource = listTrabajadoresLimpieza;
                    txtBlockPacientesAtendidos.Visibility = Visibility.Collapsed;
                    dataGridPacientesAtendidos.Visibility = Visibility.Collapsed;
                    Sanitario = false;
                    limpiar();
                    break;
                default:
                    // Manejar otros casos si es necesario
                    break;
            }
        }



        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            var Trabajador = (Trabajador)Lista_trabajadores.SelectedItem;
            //MessageBoxResult result = MessageBox.Show("¿Estas seguro de que quieres eliminar a este trabajador?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBox.Show("¿Estas seguro de que quieres eliminar a este trabajador?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            if (Sanitario == true)
            {
                listTrabajadoresSanitarios.Remove(Trabajador);

            }
            else
            {
                listTrabajadoresLimpieza.Remove(Trabajador);

            }

            Lista_trabajadores.Items.Refresh();

            limpiar();
        }

        private List<Nomina> cargarNominas()
        {
            List<Nomina> nominas = new List<Nomina>();

            try
            {
                // Crear un objeto XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                //almacenamos la informacion de "pacientes.xml" en la variable fichero
                var fichero = Application.GetResourceStream(new Uri("Datos/nominas.xml", UriKind.Relative));
                // Cargar el documento XML desde el archivo
                xmlDoc.Load(fichero.Stream);
                // Obtener la lista de nodos de nominas
                XmlNodeList nominasXML = xmlDoc.SelectNodes("/Nominas/Nomina");
                // Iterar a través de las nominas y agregar a la lista de Nominas
                foreach (XmlNode nominaNode in nominasXML)
                {
                    double monto = double.Parse(nominaNode.SelectSingleNode("Cantidad").InnerText);
                    string DNI_trabajador = nominaNode.SelectSingleNode("DNI_Trabajador").InnerText;
                    DateTime fecha = DateTime.Parse(nominaNode.SelectSingleNode("Fecha").InnerText);

                    Nomina nomina = new Nomina(fecha, monto, DNI_trabajador);
                    nominas.Add(nomina);
                }
                return nominas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return nominas;
            }
        }

        private List<Nomina> cargarNominaTrabajador(Trabajador t, List<Nomina> nominas)
        {
            List<Nomina> nominaTrabajador = new List<Nomina>();
            if (nominas == null)
            {
                return null;
            }

            foreach (Nomina n in nominas)
            {
                if (t.DNI == n.DNI_trabajador)
                {
                    nominaTrabajador.Add(n);
                }

            }
            return nominaTrabajador;
        }
        private List<Paciente> cargarPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();
            try
            {
                // Crear un objeto XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                //almacenamos la informacion de "pacientes.xml" en la variable fichero
                var fichero = Application.GetResourceStream(new Uri("Datos/pacientes.xml", UriKind.Relative));
                // Cargar el documento XML desde el archivo
                xmlDoc.Load(fichero.Stream);
                // Obtener la lista de nodos de pacientes
                XmlNodeList pacientesXML = xmlDoc.SelectNodes("/Pacientes/Paciente");
                // Iterar a través de los pacientes y agregar a la lista de Pacientes
                foreach (XmlNode pacienteNode in pacientesXML)
                {
                    string nombre = pacienteNode.SelectSingleNode("Nombre").InnerText;
                    string apellido1 = pacienteNode.SelectSingleNode("Apellido1").InnerText;
                    string apellido2 = pacienteNode.SelectSingleNode("Apellido2").InnerText;
                    string dni = pacienteNode.SelectSingleNode("DNI").InnerText;
                    string telefono = pacienteNode.SelectSingleNode("Telefono").InnerText;
                    string direccion = pacienteNode.SelectSingleNode("Direccion").InnerText;

                    Paciente paciente = new Paciente(nombre, apellido1, apellido2, dni, telefono, direccion);
                    pacientes.Add(paciente);
                }
                return pacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return pacientes;
            }

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

                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    var cita = new Cita("", "", "", DateTime.Now, "", "");

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
        public List<Cita> cargarCitasAtendidas(List<Paciente> pacientes, List<Cita> citas, Trabajador t)
        {
            List<Cita> citaPaciente = new List<Cita>();
            foreach (Cita c in citas)
            {
                if (t.correo == c.correo_fisio && pacientes != null)
                {
                    foreach (Paciente p in pacientes)
                    {
                        if (c.DNI_paciente == p.DNI)
                        {
                            c.nombre_paciente = p.Nombre + " " + p.Apellido1 + " " + p.Apellido2;
                            t = listTrabajadoresSanitarios.Find(x => x.correo == c.correo_fisio);
                            citaPaciente.Add(c);
                        }
                    }
                }
            }
            return citaPaciente;
        }

        // El boton tiene 3 modos (Editar, Guardar, Añadir)
        // Editar = 0 , Guardar = 1, Añadir = 3 
        private int modoBotonEditGuard = 0;

        private void Editar_Guardar_Click(object sender, RoutedEventArgs e)
        {
            Trabajador trabajadorSeleccionado = (Trabajador)Lista_trabajadores.SelectedItem;

            if (modoBotonEditGuard == 3)
            {
               
                if (comprobarCampos())
                {
                    modoBotonEditGuard = 0;
                    Trabajador trabajador;


                    string ImagenRuta = "/Imagenes/Imagenes_trabajadores/Predeterminado.png";


                    trabajador = new Trabajador(txtNombre.Text, txtApellido1.Text,
                        txtApellido2.Text, txtDNI.Text, txtTelefono.Text, txtDireccion.Text,
                        "", txtTrabajo.Text, ImagenRuta);

                    txtNombre.IsReadOnly = true;
                    txtApellido1.IsReadOnly = true;
                    txtApellido2.IsReadOnly = true;
                    txtDNI.IsReadOnly = true;
                    txtTelefono.IsReadOnly = true;
                    txtDireccion.IsReadOnly = true;
                    txtTrabajo.IsReadOnly = true;
                    txtNombre.Foreground = Brushes.Gray;
                    txtApellido1.Foreground = Brushes.Gray;
                    txtApellido2.Foreground = Brushes.Gray;
                    txtDNI.Foreground = Brushes.Gray;
                    txtTelefono.Foreground = Brushes.Gray;
                    txtDireccion.Foreground = Brushes.Gray;
                    txtTrabajo.Foreground = Brushes.Gray;

                    if (Sanitario == true)
                    {
                        listTrabajadoresSanitarios.Add(trabajador);
                        listTrabajadoresSanitarios.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.Ordinal));
                    }
                    else
                    {
                        listTrabajadoresLimpieza.Add(trabajador);
                        listTrabajadoresLimpieza.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.Ordinal));
                    }

                    Lista_trabajadores.Items.Refresh();
                    Lista_trabajadores.SelectedItem = trabajador;

                    btnEditar_Guardar.Content = "Editar";
                }
                else
                {
                    return;
                }
            }

            else if (trabajadorSeleccionado != null)
            {
                Button btn = (Button)sender;
                if (modoBotonEditGuard == 0)
                {
                    modoBotonEditGuard = 1;
                    txtNombre.IsReadOnly = false;
                    txtApellido1.IsReadOnly = false;
                    txtApellido2.IsReadOnly = false;
                    txtDNI.IsReadOnly = false;
                    txtTelefono.IsReadOnly = false;
                    txtDireccion.IsReadOnly = false;
                    txtTrabajo.IsReadOnly = false;
                    txtNombre.Foreground = Brushes.Black;
                    txtApellido1.Foreground = Brushes.Black;
                    txtApellido2.Foreground = Brushes.Black;
                    txtDNI.Foreground = Brushes.Black;
                    txtTelefono.Foreground = Brushes.Black;
                    txtDireccion.Foreground = Brushes.Black;
                    txtTrabajo.Foreground = Brushes.Black;

                    btn.Content = "Guardar";
                }
                else
                {

                    if (comprobarCampos())
                    {
                        modoBotonEditGuard = 0;

                        Trabajador trabajador;
                        if (trabajadorSeleccionado.ImagenRuta == null)
                        {
                            trabajadorSeleccionado.ImagenRuta = "/Imagenes/Imagenes_trabajadores/Predeterminado.png";
                        }

                        trabajador = new Trabajador(txtNombre.Text, txtApellido1.Text,
                            txtApellido2.Text, txtDNI.Text, txtTelefono.Text, txtDireccion.Text,
                            trabajadorSeleccionado.correo, txtTrabajo.Text,
                            trabajadorSeleccionado.ImagenRuta);

                        txtNombre.IsReadOnly = true;
                        txtApellido1.IsReadOnly = true;
                        txtApellido2.IsReadOnly = true;
                        txtDNI.IsReadOnly = true;
                        txtTelefono.IsReadOnly = true;
                        txtDireccion.IsReadOnly = true;
                        txtTrabajo.IsReadOnly = true;
                        txtNombre.Foreground = Brushes.Gray;
                        txtApellido1.Foreground = Brushes.Gray;
                        txtApellido2.Foreground = Brushes.Gray;
                        txtDNI.Foreground = Brushes.Gray;
                        txtTelefono.Foreground = Brushes.Gray;
                        txtDireccion.Foreground = Brushes.Gray;
                        txtTrabajo.Foreground = Brushes.Gray;

                        if (Sanitario == true)
                        {
                            listTrabajadoresSanitarios.Remove(trabajadorSeleccionado);
                            listTrabajadoresSanitarios.Add(trabajador);
                            listTrabajadoresSanitarios.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.Ordinal));
                        }
                        else
                        {
                            listTrabajadoresLimpieza.Remove(trabajadorSeleccionado);
                            listTrabajadoresLimpieza.Add(trabajador);
                            listTrabajadoresLimpieza.Sort((a, b) => string.Compare(a.Nombre, b.Nombre, StringComparison.Ordinal));
                        }

                        Lista_trabajadores.Items.Refresh();
                        Lista_trabajadores.SelectedItem = trabajador;

                        btnEditar_Guardar.Content = "Editar";
                    }

                }

            }
        }

        private Boolean comprobarCampos() {
            if (txtNombre.Text != "" && txtApellido1.Text != "" && txtApellido2.Text != ""
                        && txtDNI.Text != "" && txtTelefono.Text != "" && txtDireccion.Text != ""
                        && txtTrabajo.Text != "") { 
                txtNombre.BorderBrush = Brushes.Black;
                txtApellido1.BorderBrush = Brushes.Black;
                txtApellido2.BorderBrush = Brushes.Black;
                txtDNI.BorderBrush = Brushes.Black;
                txtTelefono.BorderBrush = Brushes.Black;
                txtDireccion.BorderBrush = Brushes.Black;
                txtTrabajo.BorderBrush = Brushes.Black;
                return true;}

            if (txtNombre.Text == "")
            {
                txtNombre.BorderBrush = Brushes.Red;
            }
            if (txtApellido1.Text == "")
            {
                txtApellido1.BorderBrush = Brushes.Red;
            }
            if (txtApellido2.Text == "")
            {
                txtApellido2.BorderBrush = Brushes.Red;
            }
            if (txtDNI.Text == "")
            {
                txtDNI.BorderBrush = Brushes.Red;
            }
            if (txtTelefono.Text == "")
            {
                txtTelefono.BorderBrush = Brushes.Red;
            }
            if (txtDireccion.Text == "")
            {
                txtDireccion.BorderBrush = Brushes.Red;
            }
            if (txtTrabajo.Text == "")
            {
                txtTrabajo.BorderBrush = Brushes.Red;
            }

            MessageBox.Show("No se puede guardar, hay campos vacios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        private void btn_añadir_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtDNI.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtTrabajo.Text = "";
            imgTrabajador.Source = new BitmapImage(new Uri("/Imagenes/Imagenes_pacientes/Predeterminado.png", UriKind.RelativeOrAbsolute));
            txtNombre.IsReadOnly = false;
            txtApellido1.IsReadOnly = false;
            txtApellido2.IsReadOnly = false;
            txtDNI.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            txtDireccion.IsReadOnly = false;
            txtTrabajo.IsReadOnly = false;
            txtNombre.Foreground = Brushes.Black;
            txtApellido1.Foreground = Brushes.Black;
            txtApellido2.Foreground = Brushes.Black;
            txtDNI.Foreground = Brushes.Black;
            txtTelefono.Foreground = Brushes.Black;
            txtDireccion.Foreground = Brushes.Black;
            txtTrabajo.Foreground = Brushes.Black;
            modoBotonEditGuard = 3;
            btnEditar_Guardar.Content = "Guardar";
            dataGridPacientesAtendidos.ItemsSource = null;

            Lista_trabajadores.SelectedItem = null;

        }
        private void limpiar()
        {
            txtNombre.Text = "Nombre";
            txtApellido1.Text = "Primer Apellido";
            txtApellido2.Text = "Segundo Apellido";
            txtDNI.Text = "000000000A";
            txtTelefono.Text = "000000000";
            txtDireccion.Text = "calle x, nº";
            txtTrabajo.Text = "Trabajo";
            imgTrabajador.Source = new BitmapImage(new Uri("/Imagenes/Imagenes_trabajadores/Predeterminado.png", UriKind.RelativeOrAbsolute));
            txtNombre.IsReadOnly = true;
            txtApellido1.IsReadOnly = true;
            txtApellido2.IsReadOnly = true;
            txtDNI.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            txtDireccion.IsReadOnly = true;
            txtTrabajo.IsReadOnly = true;
            txtNombre.Foreground = Brushes.Gray;
            txtApellido1.Foreground = Brushes.Gray;
            txtApellido2.Foreground = Brushes.Gray;
            txtDNI.Foreground = Brushes.Gray;
            txtTelefono.Foreground = Brushes.Gray;
            txtDireccion.Foreground = Brushes.Gray;
            txtTrabajo.Foreground = Brushes.Gray;
            btnEditar_Guardar.Content = "Editar";
            dataGridPacientesAtendidos.ItemsSource = null;
            datagridNominas.ItemsSource = null;
            modoBotonEditGuard = 0;
        }
        private void cmbTiposTrabajador_Loader(object sender, RoutedEventArgs e)
        {
            cmbTiposTrabajador.SelectedIndex = 0;
        }
    }
}
