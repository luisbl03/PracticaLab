using System;

namespace PracticaLab
{
    public class Informe
    {
        public string Descripcion { get; set; }
        public DateTime FechaInforme { get; set; }

        // Nueva propiedad para indicar si el informe se ha guardado
        public bool Guardado { get; set; }

        // Nueva propiedad para contener la descripción del informe antes de cualquier modificación
        public string DescripcionInforme { get; set; }

        public Informe()
        {
            Guardado = true; // Inicialmente, el informe se considera guardado
        }

        public void ActualizarDescripcionInicial()
        {
            DescripcionInforme = Descripcion; // Guardar la descripción inicial al cargar el informe
        }

        public void MarcarComoGuardado()
        {
            Guardado = true; // Marcar el informe como guardado después de actualizar la descripción
        }

        public override string ToString()
        {
            return $"{FechaInforme.ToString("dd/MM/yyyy")} - {Descripcion}";
        }
    }
}
