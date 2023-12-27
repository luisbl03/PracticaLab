using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{
    internal class Personal
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public long numTelefono { get; set; }
        public string correo { get; set; }
        public int edad { get; set; }
        public string trabajo { get; set; }
        public Personal(string nombre, string apellidos, long telef, string correo, int edad, string trabajo)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            numTelefono = telef;
            this.correo = correo;
            this.edad = edad;
            this.trabajo = trabajo;
        }

    }
}
