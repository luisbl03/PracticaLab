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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Usuario usuario;
        public Registro()
        {
            InitializeComponent();
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

                listaUsuarios.Add(usuario);

            }
            return listaUsuarios;
        }

        private void screenRegistro_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void registrar()
        {
            /*miramos si el usuario ya existe*/
            List<Usuario> listUsuarios = cargarUsuarios();
            /*Miramos si el usuario ya esta dentro*/
            Usuario u = new Usuario(txtNombre_Registro.Text, txtApellidos_Registro.Text, long.Parse(txtTelefono_Registro.Text), txtCorreo_Registro.Text, txtContraseña_Registro.Text);
            /*miramos si en la lista de usuarios existe un usuario con ese correo*/
            bool existe = false;
            foreach (Usuario usuario in listUsuarios)
            {
                if (usuario.correo.Equals(u.correo))
                {
                    existe = true;
                }
            }
            if (existe)
            {
                MessageBox.Show("El usuario ya existe");
                txtApellidos_Registro.Clear();
                txtCorreo_Registro.Clear();
                txtTelefono_Registro.Clear();
                txtNombre_Registro.Clear();
                txtTelefono_Registro.Clear();
                txtRepiteContraseña_Registro.Clear();
                txtContraseña_Registro.Clear();
            }
            else
            {
                usuario = u;
                Window gestor = new Gestor(u);
                this.Hide();
                gestor.Show();
            }
        }

        private void bttnIniciar_Sesion_Click(object sender, RoutedEventArgs e)
        {
            Window login = new IniciarSesion();
            this.Hide();
            login.Show();
        }

        private void bttnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            /*vamos a comprobar que todos los campos esten rellenos*/
            if (txtNombre_Registro.Text != null && txtApellidos_Registro.Text !=  null && txtCorreo_Registro.Text != null && txtTelefono_Registro.Text != null && txtContraseña_Registro != null && txtRepiteContraseña_Registro.Text != null)
            {
                /*miramos si los campos de la contraseña son iguales*/
                if (txtContraseña_Registro.Text.Equals(txtRepiteContraseña_Registro.Text))
                {
                    registrar();
                }
                else
                {
                    MessageBox.Show("Usuario existente");
                    txtApellidos_Registro.Clear();
                    txtCorreo_Registro.Clear();
                    txtTelefono_Registro.Clear();
                    txtNombre_Registro.Clear();
                    txtTelefono_Registro.Clear();
                    txtRepiteContraseña_Registro.Clear();
                }
            }
            else
            {
                /*vamos a marcar en rojo los campos que faltan*/;
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.BorderBrush = Brushes.Red;
                }
                if (txtNombre_Registro.Text.Equals("Nombre"))
                {
                    txtNombre_Registro.BorderBrush = Brushes.Red;
                }
                if (txtApellidos_Registro.Text == "")
                {
                    txtApellidos_Registro.BorderBrush = Brushes.Red;
                }
                if (txtApellidos_Registro.Text.Equals("Apellidos"))
                {
                    txtApellidos_Registro.BorderBrush = Brushes.Red;
                }
                if (txtTelefono_Registro.Text == "")
                {
                    txtTelefono_Registro.BorderBrush = Brushes.Red;
                }
                if (txtTelefono_Registro.Text.Equals("Telefono"))
                {
                    txtTelefono_Registro.BorderBrush = Brushes.Red;
                }
                if (txtCorreo_Registro.Text == "")
                {
                    txtCorreo_Registro.BorderBrush = Brushes.Red;
                }
                if (txtCorreo_Registro.Text.Equals("Correo"))
                {
                    txtCorreo_Registro.BorderBrush = Brushes.Red;
                }
                if (passRegistro.Password == "")
                {
                    passRegistro.Visibility = Visibility.Hidden;
                    txtContraseña_Registro.Visibility = Visibility.Visible;
                    txtContraseña_Registro.BorderBrush = Brushes.Red;
                }
                if (passRepite_contrasena.Password == "")
                {
                    passRepite_contrasena.Visibility = Visibility.Hidden;
                    txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                    txtRepiteContraseña_Registro.BorderBrush = Brushes.Red;
                }
            }
        }

        private void txtNombre_Registro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNombre_Registro.Foreground != Brushes.Black)
            {
                txtNombre_Registro.Clear();
                txtNombre_Registro.BorderBrush = Brushes.Black;
                txtNombre_Registro.Foreground = Brushes.Black;
            }
            /*miramos si los campos estan vacios, si lo estan, ponemos el texto en gris y ponemos el nombre del campo*/
            if (txtApellidos_Registro.Text == "")
            {
                txtApellidos_Registro.Text = "Apellidos";
                txtApellidos_Registro.Foreground = Brushes.Gray;
            }
            if (txtTelefono_Registro.Text == "")
            {
                txtTelefono_Registro.Text = "Telefono";
                txtTelefono_Registro.Foreground = Brushes.Gray;
            }
            if (txtCorreo_Registro.Text == "")
            {
                txtCorreo_Registro.Text = "Correo";
                txtCorreo_Registro.Foreground = Brushes.Gray;
            }
            if (passRegistro.Password == "" && passRegistro.IsVisible)
            {
                passRegistro.Visibility = Visibility.Hidden;
                txtContraseña_Registro.Visibility = Visibility.Visible;
            }
            if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
            {
                passRepite_contrasena.Visibility = Visibility.Hidden;
                txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
            }
            
        }

        private void txtNombre_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtApellidos_Registro.Focus();
                if (txtApellidos_Registro.Foreground != Brushes.Black)
                {
                    txtApellidos_Registro.Clear();
                    txtApellidos_Registro.Foreground = Brushes.Black;
                }
                /*miramos si los campos estan vacios*/
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.Text = "Nombre";
                    txtNombre_Registro.Foreground = Brushes.Gray;
                }
                if (txtTelefono_Registro.Text == "")
                {
                    txtTelefono_Registro.Text = "Telefono";
                    txtTelefono_Registro.Foreground = Brushes.Gray;
                }
                if (txtCorreo_Registro.Text == "")
                {
                    txtCorreo_Registro.Text = "Correo";
                    txtCorreo_Registro.Foreground = Brushes.Gray;
                }
                if (passRegistro.Password == "" && passRegistro.IsVisible)
                {
                    passRegistro.Visibility = Visibility.Hidden;
                    txtContraseña_Registro.Visibility = Visibility.Visible;
                }
                if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
                {
                    passRepite_contrasena.Visibility = Visibility.Hidden;
                    txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                }
            }
        }

        private void txtApellidos_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtTelefono_Registro.Focus();
                if (txtTelefono_Registro.Foreground != Brushes.Black)
                {
                    txtTelefono_Registro.Clear();
                    txtTelefono_Registro.Foreground= Brushes.Black;
                }
                /*miramos si los campos estan vacios*/
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.Text = "Nombre";
                    txtNombre_Registro.Foreground = Brushes.Gray;
                }
                if (txtApellidos_Registro.Text == "")
                {
                    txtApellidos_Registro.Text = "Apellidos";
                    txtApellidos_Registro.Foreground = Brushes.Gray;
                }
                if (txtCorreo_Registro.Text == "")
                {
                    txtCorreo_Registro.Text = "Correo";
                    txtCorreo_Registro.Foreground = Brushes.Gray;
                }
                if (passRegistro.Password == "" && passRegistro.IsVisible)
                {
                    passRegistro.Visibility = Visibility.Hidden;
                    txtContraseña_Registro.Visibility = Visibility.Visible;
                }
                if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
                {
                    passRepite_contrasena.Visibility = Visibility.Hidden;
                    txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                }
            }
        }

        private void txtTelefono_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtCorreo_Registro.Focus();
                if (txtCorreo_Registro.Foreground!= Brushes.Black)
                {
                    txtCorreo_Registro.Clear();
                    txtCorreo_Registro.Foreground = Brushes.Black;
                }
                /*miramos si los campos estan vacios*/
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.Text = "Nombre";
                    txtNombre_Registro.Foreground = Brushes.Gray;
                }
                if (txtApellidos_Registro.Text == "")
                {
                    txtApellidos_Registro.Text = "Apellidos";
                    txtApellidos_Registro.Foreground = Brushes.Gray;
                }
                if (txtTelefono_Registro.Text == "")
                {
                    txtTelefono_Registro.Text = "Telefono";
                    txtTelefono_Registro.Foreground = Brushes.Gray;
                }
                if (passRegistro.Password == "" && passRegistro.IsVisible)
                {
                    passRegistro.Visibility = Visibility.Hidden;
                    txtContraseña_Registro.Visibility = Visibility.Visible;
                }
                if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
                {
                    passRepite_contrasena.Visibility = Visibility.Hidden;
                    txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                }
            }
        }

        private void txtCorreo_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtContraseña_Registro.Visibility = Visibility.Hidden;
                passRegistro.Visibility = Visibility.Visible;
                passRegistro.Focus();
                /*miramos si los campos estan vacios*/
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.Text = "Nombre";
                    txtNombre_Registro.Foreground = Brushes.Gray;
                }
                if (txtApellidos_Registro.Text == "")
                {
                    txtApellidos_Registro.Text = "Apellidos";
                    txtApellidos_Registro.Foreground = Brushes.Gray;
                }
                if (txtTelefono_Registro.Text == "")
                {
                    txtTelefono_Registro.Text = "Telefono";
                    txtTelefono_Registro.Foreground = Brushes.Gray;
                }
                if (txtCorreo_Registro.Text == "")
                {
                    txtCorreo_Registro.Text = "Correo";
                    txtCorreo_Registro.Foreground = Brushes.Gray;
                }
                if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
                {
                    passRepite_contrasena.Visibility = Visibility.Hidden;
                    txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                }
            }
        }

        private void txtApellidos_Registro_GotFocus(object sender, RoutedEventArgs e)
        {
            /*limpiamos el campo, ponemos el texto en negro y si los demas estan vacios, ponemos su color en gris y ponemos como texto su tipo*/
            if (txtApellidos_Registro.Foreground != Brushes.Black)
            {
                txtApellidos_Registro.Clear();
                txtApellidos_Registro.Foreground = Brushes.Black;
            }
            /*miramos si los demas campos estan vacios*/
            if (txtNombre_Registro.Text == "")
            {
                txtNombre_Registro.Text = "Nombre";
                txtNombre_Registro.Foreground = Brushes.Gray;
            }
            if (txtTelefono_Registro.Text == "")
            {
                txtTelefono_Registro.Text = "Telefono";
                txtTelefono_Registro.Foreground = Brushes.Gray;
            }
            if (txtCorreo_Registro.Text == "")
            {
                txtCorreo_Registro.Text = "Correo";
                txtCorreo_Registro.Foreground = Brushes.Gray;
            }
            if (passRegistro.Password == "" && passRegistro.IsVisible)
            {
                passRegistro.Visibility = Visibility.Hidden;
                txtContraseña_Registro.Visibility = Visibility.Visible;
            }
            if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
            {
                passRepite_contrasena.Visibility = Visibility.Hidden;
                txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
            }
        }

        private void txtTelefono_Registro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTelefono_Registro.Foreground != Brushes.Black)
            {
                txtTelefono_Registro.Clear();
                txtTelefono_Registro.Foreground = Brushes.Black;
            }
            /*miramos si los demas campos estan vacios*/
            if (txtNombre_Registro.Text == "")
            {
                txtNombre_Registro.Text = "Nombre";
                txtNombre_Registro.Foreground = Brushes.Gray;
            }
            if (txtApellidos_Registro.Text == "")
            {
                txtApellidos_Registro.Text = "Apellidos";
                txtApellidos_Registro.Foreground = Brushes.Gray;
            }
            if (txtCorreo_Registro.Text == "")
            {
                txtCorreo_Registro.Text = "Correo";
                txtCorreo_Registro.Foreground = Brushes.Gray;
            }
            if (passRegistro.Password == "" && passRegistro.IsVisible)
            {
                passRegistro.Visibility = Visibility.Hidden;
                txtContraseña_Registro.Visibility = Visibility.Visible;
            }
            if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
            {
                passRepite_contrasena.Visibility = Visibility.Hidden;
                txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
            }
        }

        private void txtCorreo_Registro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtCorreo_Registro.Foreground != Brushes.Black)
            {
                txtCorreo_Registro.Clear();
                txtCorreo_Registro.Foreground = Brushes.Black;
            }
            /*miramos si los demas campos estan vacios*/
            if (txtNombre_Registro.Text == "")
            {
                txtNombre_Registro.Text = "Nombre";
                txtNombre_Registro.Foreground = Brushes.Gray;
            }
            if (txtApellidos_Registro.Text == "")
            {
                txtApellidos_Registro.Text = "Apellidos";
                txtApellidos_Registro.Foreground = Brushes.Gray;
            }
            if (txtTelefono_Registro.Text == "")
            {
                txtTelefono_Registro.Text = "Telefono";
                txtTelefono_Registro.Foreground = Brushes.Gray;
            }
            if (passRegistro.Password == "" && passRegistro.IsVisible)
            {
                passRegistro.Visibility = Visibility.Hidden;
                txtContraseña_Registro.Visibility = Visibility.Visible;
            }
            if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
            {
                passRepite_contrasena.Visibility = Visibility.Hidden;
                txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
            }
        }

        private void txtContraseña_Registro_GotFocus(object sender, RoutedEventArgs e)
        {
            txtContraseña_Registro.Visibility = Visibility.Hidden;
            passRegistro.Visibility = Visibility.Visible;
            passRegistro.Focus();
            /*miramos si los demas campos estan vacios*/
            if (txtNombre_Registro.Text == "")
            {
                txtNombre_Registro.Text = "Nombre";
                txtNombre_Registro.Foreground = Brushes.Gray;
            }
            if (txtApellidos_Registro.Text == "")
            {
                txtApellidos_Registro.Text = "Apellidos";
                txtApellidos_Registro.Foreground = Brushes.Gray;
            }
            if (txtTelefono_Registro.Text == "")
            {
                txtTelefono_Registro.Text = "Telefono";
                txtTelefono_Registro.Foreground = Brushes.Gray;
            }
            if (txtCorreo_Registro.Text == "")
            {
                txtCorreo_Registro.Text = "Correo";
                txtCorreo_Registro.Foreground = Brushes.Gray;
            }
            if (passRepite_contrasena.Password == "" && passRepite_contrasena.IsVisible)
            {
                passRepite_contrasena.Visibility = Visibility.Hidden;
                txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
            }
        }

        private void txtRepiteContraseña_Registro_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRepiteContraseña_Registro.Visibility = Visibility.Hidden;
            passRepite_contrasena.Visibility = Visibility.Visible;
            passRepite_contrasena.Focus();
            /*miramos si los demas campos estan vacios*/
            if (txtNombre_Registro.Text == "")
            {
                txtNombre_Registro.Text = "Nombre";
                txtNombre_Registro.Foreground = Brushes.Gray;
            }
            if (txtApellidos_Registro.Text == "")
            {
                txtApellidos_Registro.Text = "Apellidos";
                txtApellidos_Registro.Foreground = Brushes.Gray;
            }
            if (txtTelefono_Registro.Text == "")
            {
                txtTelefono_Registro.Text = "Telefono";
                txtTelefono_Registro.Foreground = Brushes.Gray;
            }
            if (txtCorreo_Registro.Text == "")
            {
                txtCorreo_Registro.Text = "Correo";
                txtCorreo_Registro.Foreground = Brushes.Gray;
            }
            if (passRegistro.Password == "" && passRegistro.IsVisible)
            {
                passRegistro.Visibility = Visibility.Hidden;
                txtContraseña_Registro.Visibility = Visibility.Visible;
            }
        }

        private void passRepite_contrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (passRegistro.Password.Equals(passRepite_contrasena.Password))
                {
                    registrar();
                }
            }
        }

        private void passRegistro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtRepiteContraseña_Registro.Visibility = Visibility.Hidden;
                passRepite_contrasena.Visibility = Visibility.Visible;
                passRepite_contrasena.Focus();

                /*miramos si los campos estan vacios*/
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.Text = "Nombre";
                    txtNombre_Registro.Foreground = Brushes.Gray;
                }
                if (txtApellidos_Registro.Text == "")
                {
                    txtApellidos_Registro.Text = "Apellidos";
                    txtApellidos_Registro.Foreground = Brushes.Gray;
                }
                if (txtTelefono_Registro.Text == "")
                {
                    txtTelefono_Registro.Text = "Telefono";
                    txtTelefono_Registro.Foreground = Brushes.Gray;
                }
                if (txtCorreo_Registro.Text == "")
                {
                    txtCorreo_Registro.Text = "Correo";
                    txtCorreo_Registro.Foreground = Brushes.Gray;
                }
                if (passRegistro.Password == "" && passRegistro.IsVisible)
                {
                    passRegistro.Visibility = Visibility.Hidden;
                    txtContraseña_Registro.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
