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

        public override string ToString()
        {
            return $"{FechaInforme.ToString("dd/MM/yyyy")} - {Descripcion}";
        }
    }

}
