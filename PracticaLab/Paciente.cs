﻿using System;

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