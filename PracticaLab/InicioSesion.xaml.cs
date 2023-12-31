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
                var usuario = new Usuario("", "",0, "", "");
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

        private void BotónIniciarSesión_Click(object sender, RoutedEventArgs e)
        {
            if (passInicioSesion.Password != "" && txtEmail_IniciarSesion.Text != "")
            {
                /*cogemos el listado del xml*/
                List<Usuario> listado = cargarUsuarios();
                /*comprobamos que los datos introducidos son correctos*/

                Usuario usuario = listado.FirstOrDefault(u => u.correo == txtEmail_IniciarSesion.Text);
                if (usuario == null)
                {
                    /*ponemos los recuadros en rojo, borramos su contenido y en la label de error ponemos que usuario no existe*/
                    passInicioSesion.Clear();
                    txtEmail_IniciarSesion.Clear();
                    passInicioSesion.BorderBrush = Brushes.Red;
                    txtEmail_IniciarSesion.BorderBrush = Brushes.Red;
                    lbl_InicioSesion_error.Content = "El usuario no existe";
                }
                else
                {
                    /*miramos si las contraseñas coinciden*/
                    if (usuario.contraseña == passInicioSesion.Password)
                    {
                        /*si coinciden, abrimos la ventana de gestor*/
                        Window gestor = new Gestor(usuario);
                        this.Hide();
                        gestor.Show();
                    }
                    else
                    {
                        /*si no coinciden, borramos el contenido de la contraseña y ponemos los bordes de los textbox en rojo*/
                        passInicioSesion.Clear();
                        passInicioSesion.BorderBrush = Brushes.Red;
                        txtEmail_IniciarSesion.BorderBrush = Brushes.Red;
                        lbl_InicioSesion_error.Content = "La contraseña no es correcta";
                    }
                }
            }
            
        }

        private void txtEmail_IniciarSesion_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmail_IniciarSesion.Text == "e-mail")
            {
                txtEmail_IniciarSesion.Text = "";
                txtEmail_IniciarSesion.Foreground = Brushes.Black;
            }
            if (passInicioSesion.Password == "")
            {
                passInicioSesion.Visibility = Visibility.Hidden;
                txtContraseña_IniciarSesion.Visibility = Visibility.Visible;
            }
        }

        private void txtEmail_IniciarSesion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) {
                txtContraseña_IniciarSesion.Visibility = Visibility.Hidden;
                passInicioSesion.Visibility = Visibility.Visible;
                passInicioSesion.Focus();
            }
            
        }

        
        private void txtContraseña_IniciarSesion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtContraseña_IniciarSesion.Visibility = Visibility.Hidden;
            passInicioSesion.Visibility = Visibility.Visible;
            passInicioSesion.Focus();
            if (txtEmail_IniciarSesion.Text == "")
            {
                txtEmail_IniciarSesion.Text = "e-mail";
                txtEmail_IniciarSesion.Foreground = Brushes.Gray;
            }

        }

        private void passInicioSesion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (passInicioSesion.Password != "" && txtEmail_IniciarSesion.Text != "")
                {
                    /*cogemos el listado del xml*/
                    List<Usuario> listado = cargarUsuarios();
                    /*comprobamos que los datos introducidos son correctos*/
                    Usuario usuario = listado.FirstOrDefault(u => u.correo == txtEmail_IniciarSesion.Text);
                    if (usuario == null)
                    {
                        /*ponemos los recuadros en rojo, borramos su contenido y en la label de error ponemos que usuario no existe*/
                        passInicioSesion.Clear();
                        txtEmail_IniciarSesion.Clear();
                        passInicioSesion.BorderBrush = Brushes.Red;
                        txtEmail_IniciarSesion.BorderBrush = Brushes.Red;
                        lbl_InicioSesion_error.Content = "El usuario no existe";
                    }
                    else
                    {
                        /*miramos si las contraseñas coinciden*/
                        if (usuario.contraseña == passInicioSesion.Password)
                        {
                            /*si coinciden, abrimos la ventana de gestor*/
                            Window gestor = new Gestor(usuario);
                            this.Hide();
                            gestor.Show();
                        }
                        else
                        {
                            /*si no coinciden, borramos el contenido de la contraseña y ponemos los bordes de los textbox en rojo*/
                            passInicioSesion.Clear();
                            passInicioSesion.BorderBrush = Brushes.Red;
                            txtEmail_IniciarSesion.BorderBrush = Brushes.Red;
                            lbl_InicioSesion_error.Content = "La contraseña no es correcta";
                        }
                    }

                }
            }
        }
    }
}
