using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{   
    public class Nomina
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public string DNI_trabajador { get; set; }

        public Nomina(DateTime fecha, double monto, string dNI_trabajador)
        {
            Fecha = fecha;
            Monto = monto;
            DNI_trabajador = dNI_trabajador;
        }

    }
}
