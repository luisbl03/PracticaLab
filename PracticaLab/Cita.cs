using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{
    public class Cita
    {
        public string nombre_paciente { get; set; }
        public string motivo { get; set; }
        public DateTime fecha { get; set; }
        public string correo_fisio { get; set; }
        public Cita(string nombre,string motivo, DateTime fecha, string correo)
        {
            this.nombre_paciente = nombre;
            this.motivo = motivo;
            this.fecha = fecha;
            this.correo_fisio = correo;

        }




    }
}
