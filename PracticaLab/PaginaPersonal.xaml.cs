using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ObservableCollection<Nomina> Nominas { get; set; }

        public Page1()
        {
            InitializeComponent();
            // Crear un objeto XmlDocument
            XmlDocument xmlDocPersonal = new XmlDocument();

            //almacenamos la informacion de "pacientes.xml" en la variable fichero
            // var fichero = Application.GetResourceStream(new Uri(" \\Datos\\personal.xml", UriKind.Relative));

            // Cargar el documento XML desde el archivo
            // xmlDocPersonal.Load(fichero.Stream);

            // Obtener la lista de nodos del personale
            // XmlNodeList personal = xmlDocPersonal.SelectNodes("/Personal/Personal");
            Nominas = new ObservableCollection<Nomina>();
            datagridNominas.ItemsSource = Nominas;
        }
        private void AgregarNomina_Click(object sender, RoutedEventArgs e)
        {

            // Ejemplo de cómo agregar una nómina ficticia a la lista
            // Inicializar la lista de nóminas
            Nominas.Add(new Nomina { Fecha = DateTime.Now, Monto = 2500.0 });

        }
        private void EliminarNomina_Click(object sender, RoutedEventArgs e)
        {
            // Obtener las nóminas seleccionadas en el DataGrid
            var nominasSeleccionadas = datagridNominas.SelectedItems.Cast<Nomina>().ToList();

            // Eliminar las nóminas seleccionadas de la colección
            foreach (var nominaSeleccionada in nominasSeleccionadas)
            {
                Nominas.Remove(nominaSeleccionada);
            }
        }

    }
    public class Nomina
        {
            public DateTime Fecha { get; set; }
            public double Monto { get; set; }
        }

       
  
}
