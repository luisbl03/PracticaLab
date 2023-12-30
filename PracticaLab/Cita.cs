using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaLab
{
    public class Cita
    {
        public string DNI_paciente { get; set; }
        public string motivo { get; set; }
        public DateTime fecha { get; set; }
        public string correo_fisio { get; set; }
        public Cita(string dni,string motivo, DateTime fecha, string correo)
        {
            this.DNI_paciente = dni;
            this.motivo = motivo;
            this.fecha = fecha;
            this.correo_fisio = correo;

        }




    }
}
