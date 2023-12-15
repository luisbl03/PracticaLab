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
        public string apellidos { get; set; }
        public long numTelefono { get; set; }
        public string correo {  get; set; }
        public string contraseña { get; set; }
        public Usuario(string nombre, string apellidos, long telef, string correo, string contraseña)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            numTelefono = telef;
            this.correo = correo;
            this.contraseña = contraseña;
        }

    }
}
