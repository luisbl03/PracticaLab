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
    public partial class EditarInforme : Window
    {
        public Paciente PacienteSeleccionado { get; set; }
        public Informe InformeSeleccionado { get; set; }
        private Page2 paginaPacientes;
        private Citas_Fisio _citasFisioPage;

        public EditarInforme(Page2 page, Paciente paciente, Informe informe)
        {
            paginaPacientes = page;
            PacienteSeleccionado = paciente;
            InformeSeleccionado = informe;
            InitializeComponent();

            
            

            // Asignamos los eventos a los TextBox
            AsignarEventosTextBox(txtDolencias, "Insertar dolencias");
            txtDolencias.Text = InformeSeleccionado.Descripcion;
            //AsignarEventosTextBox(txtTratamiento, "Insertar tratamiento");
        }

        public EditarInforme(Citas_Fisio citasFisioPage, Paciente paciente, Informe informe)
        {
            _citasFisioPage = citasFisioPage;
            PacienteSeleccionado = paciente;
            InformeSeleccionado = informe;
            InitializeComponent();;

            // Asignamos los eventos a los TextBox
            AsignarEventosTextBox(txtDolencias, "Insertar dolencias");
            txtDolencias.Text = InformeSeleccionado.Descripcion;
            //AsignarEventosTextBox(txtTratamiento, "Insertar tratamiento");
        }

        private void AsignarEventosTextBox(TextBox textBox, string textoPredeterminado)
        {
            // Configura el color gris claro cuando el TextBox no tiene foco
            textBox.Foreground = Brushes.Black;
            textBox.Text = textoPredeterminado;

            // Evento GotFocus: se dispara cuando el TextBox recibe el foco
            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == textoPredeterminado)
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = Brushes.Black;
                }
            };

            // Evento LostFocus: se activa cuando no tenemos clickado el textBox
            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Restaura el texto predeterminado y el color cuando el cuadro de texto está vacío
                    textBox.Text = textoPredeterminado;
                    textBox.Foreground = Brushes.Black;
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
                string archivoSeleccionado = openFileDialog.FileName;
                txtRutaArchivo.Text = archivoSeleccionado;

                string nombreArchivo = System.IO.Path.GetFileName(archivoSeleccionado);
                txtMensajeConfirmacion.Text = $"Se ha adjuntado '{nombreArchivo}' como prueba.";
                txtMensajeConfirmacion.Visibility = Visibility.Visible;

            }
        }
        private void btnActualizarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Guardar la descripción inicial antes de cualquier modificación
            InformeSeleccionado.ActualizarDescripcionInicial();

            // Actualizar la descripción del informe en la instancia de InformeSeleccionado
            InformeSeleccionado.Descripcion = txtDolencias.Text;

            // Marcar el informe como guardado
            InformeSeleccionado.MarcarComoGuardado();

            // Cerrar la ventana de edición
            Close();
            if (_citasFisioPage != null)
            {
                _citasFisioPage.UpdateInformesListAfterAdd(PacienteSeleccionado);
            }
            else if (paginaPacientes != null)
            {
                paginaPacientes.UpdateInformesListAfterAdd(PacienteSeleccionado);
            }
        }
    }
}
