using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{
    public class Informe
    {
        public string Descripcion { get; set; }
        public DateTime FechaInforme { get; set; }

        public bool Guardado { get; set; } // Nueva propiedad para indicar si el informe se ha guardado

        public override string ToString()
        {
            return $"{FechaInforme.ToString("dd/MM/yyyy")} - {Descripcion}";
        }
    }

}
