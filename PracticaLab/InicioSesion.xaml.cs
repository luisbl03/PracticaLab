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
using System.Xml;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class IniciarSesion : Window
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void BotónRegistrate_Click(object sender, RoutedEventArgs e)
        {
            Window registro = new Registro();
            this.Hide();
            registro.Show();
        }

        private void IniciarSesión_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private List<Usuario> cargarUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/usuarios.xml", UriKind.Relative));
            doc.Load(fichero.Stream);

            /*bucle de asignacion de valores*/
            foreach(XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var usuario = new Usuario("", "", "",0, "", "");
                usuario.nombre = node.Attributes["Nombre"].Value;
                usuario.apellido1 = node.Attributes["Apellido1"].Value;
                usuario.apellido2 = node.Attributes["Apellido2"].Value;
                usuario.numTelefono = long.Parse(node.Attributes["telefono"].Value);
                usuario.correo = node.Attributes["correo"].Value;
                usuario.contraseña = node.Attributes["contraseña"].Value;
                
                listaUsuarios.Add(usuario);

            }
            return listaUsuarios;
        }

        private void BotónIniciarSesión_Click(object sender, RoutedEventArgs e)
        {
            /*cogemos el listado del xml*/
            List<Usuario> listado = cargarUsuarios();
            /*comprobamos que los datos introducidos son correctos*/

            Usuario usuario = listado.FirstOrDefault(u => u.correo == txtEmail_IniciarSesion.Text && u.contraseña == txtContraseña_IniciarSesion.Text);

            if (usuario != null)
            {
                Window gestor = new Gestor();
                this.Hide();
                gestor.Show();
            }
            else {
                /*borramos el contenido de la contraseña y ponemos los bordes de los textbox en rojo*/
                txtContraseña_IniciarSesion.Clear();
                txtContraseña_IniciarSesion.BorderBrush = Brushes.Red;
                txtEmail_IniciarSesion.BorderBrush = Brushes.Red;

            }
        }

        private void txtEmail_IniciarSesion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmail_IniciarSesion.Clear();
            txtEmail_IniciarSesion.Foreground = Brushes.Black;
        }

        private void txtEmail_IniciarSesion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) {
                txtContraseña_IniciarSesion.Clear();
                txtContraseña_IniciarSesion.Foreground = Brushes.Black;
                txtContraseña_IniciarSesion.Focus();
            }
            
        }

        private void txtContraseña_IniciarSesion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                /*cogemos el listado del xml*/
                List<Usuario> listado = cargarUsuarios();
                /*comprobamos que los datos introducidos son correctos*/

                Usuario usuario = listado.FirstOrDefault(u => u.correo == txtEmail_IniciarSesion.Text && u.contraseña == txtContraseña_IniciarSesion.Text);

                if (usuario != null)
                {
                    Window gestor = new Gestor();
                    this.Hide();
                    gestor.Show();
                }
                else
                {
                    /*borramos el contenido de la contraseña y ponemos los bordes de los textbox en rojo*/
                    txtContraseña_IniciarSesion.Clear();
                    txtContraseña_IniciarSesion.BorderBrush = Brushes.Red;
                    txtEmail_IniciarSesion.BorderBrush = Brushes.Red;

                }
            }
        }
        private void txtContraseña_IniciarSesion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtContraseña_IniciarSesion.Clear();
            txtContraseña_IniciarSesion.Foreground = Brushes.Black;

        }
    }
}
