using System;
using System.Collections.Generic;

namespace PracticaLab
{
    public class Paciente
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaCita { get; set; }

        public string RutaFoto { get; set; }

        public List<Informe> Informes { get; set; }
        public List<Informe> InformesTemporales { get; set; }

       public Paciente (string nombre, string apellido1, string apellido2, string dNI, string telefono, string direccion)
        {
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            DNI = dNI;
            Telefono = telefono;
            Direccion = direccion;
            //FechaCita = fechaCita;
            //RutaFoto = rutaFoto;
            Informes = new List<Informe>();
            InformesTemporales = new List<Informe>();
        }   

        public override string ToString()
        {
            string pacienteString = $"{Nombre} {Apellido1} {Apellido2}";

            if (FechaCita.HasValue)
            {
                pacienteString += $" - Cita: {FechaCita.Value.ToString("dd/MM/yyyy HH:mm")}";
            }

            return pacienteString;
        }
    }
}