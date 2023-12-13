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
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
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
                var usuario = new Usuario("", "", "", 0, "", "");
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

        private void screenRegistro_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void escrbirFichero()
        {
            /*miramos si el usuario ya existe*/
            List<Usuario> listUsuarios = cargarUsuarios();
            /*Miramos si el usuario ya esta dentro*/
            string[] apellidos = txtApellidos_Registro.Text.Split(' ');
            Usuario u = new Usuario(txtNombre_Registro.Text, apellidos[0], apellidos[1], long.Parse(txtTelefono_Registro.Text), txtCorreo_Registro.Text, txtContraseña_Registro.Text);
            /*comprobamos si existe en la lista de usuarios*/
            bool existe = listUsuarios.Contains(u);
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
                /*añadimos el usuario al fichero xml*/
                XmlDocument doc = new XmlDocument();
                var fichero = Application.GetResourceStream(new Uri("Datos/usuarios.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
                XmlNode usuario = doc.CreateElement("Usuario");
                XmlNode Nombre = doc.CreateElement("Nombre");
                Nombre.InnerText = txtNombre_Registro.Text;
                XmlNode apellido1 = doc.CreateElement("Apellido1");
                apellido1.InnerText = apellidos[0];
                XmlNode apellido2 = doc.CreateElement("Apellido2");
                apellido2.InnerText = apellidos[1];
                XmlNode telefono = doc.CreateElement("telefono");
                telefono.InnerText = txtTelefono_Registro.Text;
                XmlNode correo = doc.CreateElement("correo");
                correo.InnerText = txtCorreo_Registro.Text;
                XmlNode contraseña = doc.CreateElement("contraseña");
                contraseña.InnerText = txtContraseña_Registro.Text;
                usuario.AppendChild(Nombre);
                usuario.AppendChild(apellido1);
                usuario.AppendChild(apellido2);
                usuario.AppendChild(telefono);
                usuario.AppendChild(correo);
                usuario.AppendChild(contraseña);
                XmlNode raiz = doc.DocumentElement;
                raiz.AppendChild(usuario);
                try
                {
                    doc.Save(fichero.Stream);
                    MessageBox.Show("Usuario insertado con exito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
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
                    escrbirFichero();
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
                MessageBox.Show("Faltan campos");
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
            }
        }

        private void txtCorreo_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtContraseña_Registro.Focus();
                if (txtContraseña_Registro.Foreground != Brushes.Black)
                {
                    txtContraseña_Registro.Clear();
                    txtContraseña_Registro.Foreground = Brushes.Black;
                }
            }
        }

        private void txtRepiteContraseña_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtContraseña_Registro.Text.Equals(txtRepiteContraseña_Registro.Text))
                {
                    escrbirFichero();
                }
            }
        }

        private void txtContraseña_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter)
            {
                txtRepiteContraseña_Registro.Focus();
                if (txtRepiteContraseña_Registro.Foreground != Brushes.Black)
                {
                    txtRepiteContraseña_Registro.Clear();
                    txtRepiteContraseña_Registro.Foreground = Brushes.Black;
                }
            }
        }
    }
}
