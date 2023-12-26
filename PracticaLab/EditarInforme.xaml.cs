using Microsoft.Win32;
using System;
using System.IO;
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

namespace PracticaLab
{
    /// <summary>
    /// Lógica de interacción para EditarInforme.xaml
    /// </summary>
    public partial class EditarInforme : Window
    {
        public EditarInforme()
        {
            InitializeComponent();

            //Asignamos los eventos a los TextBox
            AsignarEventosTextBox(txtPatologiasPrevias, "Insertar patologías previas");
            AsignarEventosTextBox(txtDolencias, "Insertar dolencias");
            AsignarEventosTextBox(txtTratamiento, "Insertar tratamiento");
        }

        private void AsignarEventosTextBox(TextBox textBox, string textoPredeterminado)
        {
            // Configura el color gris claro cuando el TextBox no tiene foco
            textBox.Foreground = Brushes.LightGray;
            textBox.Text = textoPredeterminado;

            // Evento GotFocus: se dispara cuando el TextBox recibe el foco
            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == textoPredeterminado)
                {
                    textBox.Text = string.Empty; // Borra el texto predeterminado
                    textBox.Foreground = Brushes.Black; // Cambia el color del texto
                }
            };

            // Evento LostFocus: se dispara cuando el TextBox pierde el foco
            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Restaura el texto predeterminado y el color cuando el cuadro de texto está vacío
                    textBox.Text = textoPredeterminado;
                    textBox.Foreground = Brushes.LightGray;
                }
            };
        }
        private void btnSubirArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                // Aquí puedes utilizar openFileDialog.FileName para obtener la ruta del archivo seleccionado
                string archivoSeleccionado = openFileDialog.FileName;

                // Puedes realizar acciones adicionales, como mostrar el archivo o almacenar la ruta en una lista, según tus necesidades
            }
        }



    }
}
