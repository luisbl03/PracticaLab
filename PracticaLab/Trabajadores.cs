using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{
    public class Trabajadores
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set;}
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string correo { get; set; }
        public string trabajo { get; set; }

        public Trabajadores(string nombre, string apellido1, string apellido2, string dNI, string telefono, string direccion, string correo, string trabajo)
        {
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            DNI = dNI;
            Telefono = telefono;
            Direccion = direccion;
            this.correo = correo;
            this.trabajo = trabajo;
        }
    }
}
