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
        private void setDefualt_properties()
        {
            txtApellidos_Registro.BorderBrush = Brushes.Black;
            txtCorreo_Registro.BorderBrush = Brushes.Black;
            txtNombre_Registro.BorderBrush = Brushes.Black;
            txtTelefono_Registro.BorderBrush = Brushes.Black;
            txtContraseña_Registro.BorderBrush = Brushes.Black;
            txtRepiteContraseña_Registro.BorderBrush = Brushes.Black;
            chkBx_Terminos.BorderBrush = Brushes.Black;

            chkBx_Terminos.Foreground = Brushes.Black;
            passRepite_contrasena.BorderBrush = Brushes.Black;
            passRepite_contrasena.Foreground = Brushes.Black;
            passRegistro.BorderBrush = Brushes.Black;
            passRegistro.Foreground = Brushes.Black;

            lblError_apellidos.Content = "";
            lblError_correo.Content = "";
            lblError_Nombre.Content = "";
            lblError_repite.Content = "";
            lblError_telefono.Content = "";
            lblError_contrasena.Content = "";

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
            setDefualt_properties();
            if (txtNombre_Registro.Text != "Nombre" && txtApellidos_Registro.Text != "Apellidos" && txtCorreo_Registro.Text != "Correo" && txtTelefono_Registro.Text != "Telefono" && passRegistro.Password != "" && passRepite_contrasena.Password != "" && chkBx_Terminos.IsChecked == true)
            {
                if (txtTelefono_Registro.Text.Length == 9)
                {
                    if (txtCorreo_Registro.Text.Contains("@"))
                    {
                        chkBx_Terminos.BorderBrush = Brushes.Black;
                        chkBx_Terminos.Foreground = Brushes.Black;
                        /*miramos si los campos de la contraseña son iguales*/
                        if (passRegistro.Password.Equals(passRepite_contrasena.Password))
                        {
                            registrar();
                        }
                        else
                        {
                            passRepite_contrasena.BorderBrush = Brushes.Red;
                            passRepite_contrasena.Foreground = Brushes.Red;
                        }
                    }
                    else
                    {
                        txtCorreo_Registro.BorderBrush = Brushes.Red;
                        lblError_correo.Foreground = Brushes.Red;
                        lblError_correo.Content = "Correo no valido";
                    }
                }
                else
                {
                    txtTelefono_Registro.BorderBrush = Brushes.Red;
                    lblError_telefono.Foreground = Brushes.Red;
                    lblError_telefono.Content = "Telefono no valido";
                }
            }
            else
            {
                /*vamos a marcar en rojo los campos que faltan*/
                if (chkBx_Terminos.IsChecked == false)
                {
                    chkBx_Terminos.BorderBrush = Brushes.Red;
                    chkBx_Terminos.Foreground = Brushes.Red;
                }
                if (txtNombre_Registro.Text == "")
                {
                    txtNombre_Registro.BorderBrush = Brushes.Red;
                    lblError_Nombre.Foreground = Brushes.Red;
                    lblError_Nombre.Content = "Campo obligatorio";
                }
                if (txtNombre_Registro.Text.Equals("Nombre"))
                {
                    txtNombre_Registro.BorderBrush = Brushes.Red;
                    lblError_Nombre.Foreground = Brushes.Red;
                    lblError_Nombre.Content = "Campo obligatorio";
                }
                if (txtApellidos_Registro.Text == "")
                {
                    txtApellidos_Registro.BorderBrush = Brushes.Red;
                    lblError_apellidos.Foreground = Brushes.Red;
                    lblError_apellidos.Content = "Campo obligatorio";
                }
                if (txtApellidos_Registro.Text.Equals("Apellidos"))
                {
                    txtApellidos_Registro.BorderBrush = Brushes.Red;
                    lblError_apellidos.Foreground = Brushes.Red;
                    lblError_apellidos.Content = "Campo obligatorio";
                }
                if (txtTelefono_Registro.Text == "")
                {
                    txtTelefono_Registro.BorderBrush = Brushes.Red;
                    lblError_telefono.Foreground = Brushes.Red;
                    lblError_telefono.Content = "Campo obligatorio";
                }
                if (txtTelefono_Registro.Text.Equals("Telefono"))
                {
                    txtTelefono_Registro.BorderBrush = Brushes.Red;
                    lblError_telefono.Foreground = Brushes.Red;
                    lblError_telefono.Content = "Campo obligatorio";
                }
                if (txtCorreo_Registro.Text == "")
                {
                    txtCorreo_Registro.BorderBrush = Brushes.Red;
                    lblError_correo.Foreground = Brushes.Red;
                    lblError_correo.Content = "Campo obligatorio";
                }
                if (txtCorreo_Registro.Text.Equals("Correo"))
                {
                    txtCorreo_Registro.BorderBrush = Brushes.Red;
                    lblError_correo.Foreground = Brushes.Red;
                    lblError_correo.Content = "Campo obligatorio";
                }
                if (passRegistro.Password == "")
                {
                    passRegistro.Visibility = Visibility.Hidden;
                    txtContraseña_Registro.Visibility = Visibility.Visible;
                    txtContraseña_Registro.BorderBrush = Brushes.Red;
                    lblError_contrasena.Foreground = Brushes.Red;
                    lblError_contrasena.Content = "Campo obligatorio";
                }
                if (passRepite_contrasena.Password == "")
                {
                    passRepite_contrasena.Visibility = Visibility.Hidden;
                    txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                    txtRepiteContraseña_Registro.BorderBrush = Brushes.Red;
                    lblError_repite.Foreground = Brushes.Red;
                    lblError_repite.Content = "Campo obligatorio";
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
                setDefualt_properties();
                if (txtNombre_Registro.Text != "Nombre" && txtApellidos_Registro.Text != "Apellidos" && txtCorreo_Registro.Text != "Correo" && txtTelefono_Registro.Text != "Telefono" && passRegistro.Password != "" && passRepite_contrasena.Password != "" && chkBx_Terminos.IsChecked == true)
                {
                    if (txtTelefono_Registro.Text.Length == 9)
                    {
                        if (txtCorreo_Registro.Text.Contains("@"))
                        {
                            chkBx_Terminos.BorderBrush = Brushes.Black;
                            chkBx_Terminos.Foreground = Brushes.Black;
                            /*miramos si los campos de la contraseña son iguales*/
                            if (passRegistro.Password.Equals(passRepite_contrasena.Password))
                            {
                                registrar();
                            }
                            else
                            {
                                passRepite_contrasena.BorderBrush = Brushes.Red;
                                passRepite_contrasena.Foreground = Brushes.Red;
                            }
                        }
                        else
                        {
                            txtCorreo_Registro.BorderBrush = Brushes.Red;
                            lblError_correo.Foreground = Brushes.Red;
                            lblError_correo.Content = "Correo no valido";
                        }
                    }
                    else
                    {
                        txtTelefono_Registro.BorderBrush = Brushes.Red;
                        lblError_telefono.Foreground = Brushes.Red;
                        lblError_telefono.Content = "Telefono no valido";
                    }
                }
                else
                {
                    /*vamos a marcar en rojo los campos que faltan*/
                    if (chkBx_Terminos.IsChecked == false)
                    {
                        chkBx_Terminos.BorderBrush = Brushes.Red;
                        chkBx_Terminos.Foreground = Brushes.Red;
                    }
                    if (txtNombre_Registro.Text == "")
                    {
                        txtNombre_Registro.BorderBrush = Brushes.Red;
                        lblError_Nombre.Foreground = Brushes.Red;
                        lblError_Nombre.Content = "Campo obligatorio";
                    }
                    if (txtNombre_Registro.Text.Equals("Nombre"))
                    {
                        txtNombre_Registro.BorderBrush = Brushes.Red;
                        lblError_Nombre.Foreground = Brushes.Red;
                        lblError_Nombre.Content = "Campo obligatorio";
                    }
                    if (txtApellidos_Registro.Text == "")
                    {
                        txtApellidos_Registro.BorderBrush = Brushes.Red;
                        lblError_apellidos.Foreground = Brushes.Red;
                        lblError_apellidos.Content = "Campo obligatorio";
                    }
                    if (txtApellidos_Registro.Text.Equals("Apellidos"))
                    {
                        txtApellidos_Registro.BorderBrush = Brushes.Red;
                        lblError_apellidos.Foreground = Brushes.Red;
                        lblError_apellidos.Content = "Campo obligatorio";
                    }
                    if (txtTelefono_Registro.Text == "")
                    {
                        txtTelefono_Registro.BorderBrush = Brushes.Red;
                        lblError_telefono.Foreground = Brushes.Red;
                        lblError_telefono.Content = "Campo obligatorio";
                    }
                    if (txtTelefono_Registro.Text.Equals("Telefono"))
                    {
                        txtTelefono_Registro.BorderBrush = Brushes.Red;
                        lblError_telefono.Foreground = Brushes.Red;
                        lblError_telefono.Content = "Campo obligatorio";
                    }
                    if (txtCorreo_Registro.Text == "")
                    {
                        txtCorreo_Registro.BorderBrush = Brushes.Red;
                        lblError_correo.Foreground = Brushes.Red;
                        lblError_correo.Content = "Campo obligatorio";
                    }
                    if (txtCorreo_Registro.Text.Equals("Correo"))
                    {
                        txtCorreo_Registro.BorderBrush = Brushes.Red;
                        lblError_correo.Foreground = Brushes.Red;
                        lblError_correo.Content = "Campo obligatorio";
                    }
                    if (passRegistro.Password == "")
                    {
                        passRegistro.Visibility = Visibility.Hidden;
                        txtContraseña_Registro.Visibility = Visibility.Visible;
                        txtContraseña_Registro.BorderBrush = Brushes.Red;
                        lblError_contrasena.Foreground = Brushes.Red;
                        lblError_contrasena.Content = "Campo obligatorio";
                    }
                    if (passRepite_contrasena.Password == "")
                    {
                        passRepite_contrasena.Visibility = Visibility.Hidden;
                        txtRepiteContraseña_Registro.Visibility = Visibility.Visible;
                        txtRepiteContraseña_Registro.BorderBrush = Brushes.Red;
                        lblError_repite.Foreground = Brushes.Red;
                        lblError_repite.Content = "Campo obligatorio";
                    }
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

        private void txtTelefono_Registro_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTelefono_Registro.Text.Length != 9)
            {
                txtTelefono_Registro.BorderBrush = Brushes.Red;
            }
            else
            {
                txtTelefono_Registro.BorderBrush = Brushes.Black;
            }
        }
    }
}
