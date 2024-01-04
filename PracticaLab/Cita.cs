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
        public string DNI_paciente { get; set; }
        public string motivo { get; set; }
        public DateTime fecha { get; set; }
        public string correo_fisio { get; set; }
        public string nombre_fisio { get; set; }
        public Cita(string nombre,string dni,string motivo, DateTime fecha, string correo, string fisio)
        {
            this.nombre_paciente = nombre;
            this.DNI_paciente = dni;
            this.motivo = motivo;
            this.fecha = fecha;
            this.correo_fisio = correo;
            this.nombre_fisio = fisio;

        }




    }
}
