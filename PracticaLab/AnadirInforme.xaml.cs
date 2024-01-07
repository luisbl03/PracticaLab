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
    /// Lógica de interacción para AñadirInforme.xaml
    /// </summary>
    public partial class AnadirInforme : Window
    {
        private Citas_Fisio _citasFisioPage;
        private Page2 _page2;
        private Paciente _pacienteSeleccionado;
        private bool informeGuardado = false;

        public AnadirInforme(Page2 page2, Paciente pacienteSeleccionado)
        {
            _page2 = page2;
            _pacienteSeleccionado = pacienteSeleccionado;
            InitializeComponent();

            //Asignamos los eventos a los TextBox
            AsignarEventosTextBox(txtPatologiasPrevias, "Insertar patologías previas");
            AsignarEventosTextBox(txtDolencias, "Insertar dolencias");
            AsignarEventosTextBox(txtTratamiento, "Insertar tratamiento");
            _pacienteSeleccionado = pacienteSeleccionado;
        }

        public AnadirInforme(Citas_Fisio citasFisioPage, Paciente pacienteSeleccionado)
        {
            _citasFisioPage = citasFisioPage;
            _pacienteSeleccionado = pacienteSeleccionado;
            InitializeComponent();

            //Asignamos los eventos a los TextBox
            AsignarEventosTextBox(txtPatologiasPrevias, "Insertar patologías previas");
            AsignarEventosTextBox(txtDolencias, "Insertar dolencias");
            AsignarEventosTextBox(txtTratamiento, "Insertar tratamiento");
            _pacienteSeleccionado = pacienteSeleccionado;
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
                string archivoSeleccionado = openFileDialog.FileName;
                txtRutaArchivo.Text = archivoSeleccionado;

                string nombreArchivo = System.IO.Path.GetFileName(archivoSeleccionado);
                txtMensajeConfirmacion.Text = $"Se ha adjuntado '{nombreArchivo}' como prueba.";
                txtMensajeConfirmacion.Visibility = Visibility.Visible;

            }
        }

        private void añadirInforme_Closed(object sender, EventArgs e)
        {
            if (!informeGuardado)
            {
                MessageBoxResult result = MessageBox.Show("¿Seguro que quieres cancelar?", "Cancelar", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se ha seleccionado un paciente
            if (_pacienteSeleccionado != null)
            {
                // Crear un nuevo informe
                Informe nuevoInforme = new Informe
                {
                    Descripcion = txtDolencias.Text,
                    FechaInforme = DateTime.Now,
                    Guardado = true
                };

                // Agregar el nuevo informe al paciente
                _pacienteSeleccionado.Informes.Add(nuevoInforme);

                // Actualizar la lista visual de informes en Page2
                _page2.listViewInformes.Items.Add(new
                {
                    IdInforme = _pacienteSeleccionado.Informes.Count,
                    FechaInforme = nuevoInforme.FechaInforme.ToString("dd/MM/yyyy"),
                    Descripcion = nuevoInforme.Descripcion
                });
                informeGuardado = true;
                // Cerrar la ventana actual
                this.Close();
                informeGuardado = true;

                // Actualizar la lista de informes en Page2 después de agregar un nuevo informe
                if (_citasFisioPage != null)
                {
                    _citasFisioPage.UpdateInformesListAfterAdd(_pacienteSeleccionado);
                }
                else if (_page2 != null)
                {
                    _page2.UpdateInformesListAfterAdd(_pacienteSeleccionado);
                }
            }
            else
            {
                // Manejar el caso en que no se haya seleccionado un paciente
                MessageBox.Show("Selecciona un paciente antes de guardar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
