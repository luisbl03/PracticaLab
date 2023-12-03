using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{
    public class Usuario
    {
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public long numTelefono { get; set; }
        public string correo {  get; set; }
        public string contraseña { get; set; }
        public Usuario(string nombre, string apellido1, string apellido2, long telef, string correo, string contraseña)
        {
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            numTelefono = telef;
            this.correo = correo;
            this.contraseña = contraseña;
        }

    }
}
