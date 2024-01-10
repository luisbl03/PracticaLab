using System;

namespace PracticaLab
{
    public class Informe
    {
        public string Descripcion { get; set; }
        public DateTime FechaInforme { get; set; }

        //Popiedad para indicar si el informe se ha guardado
        public bool Guardado { get; set; }

        //Propiedad para contener la descripción del informe antes de cualquier modificación
        public string DescripcionInforme { get; set; }

        public Informe()
        {
            Guardado = true; // Inicialmente el informe se considera guardado
        }

        public void ActualizarDescripcionInicial()
        {
            DescripcionInforme = Descripcion; // Guarda la descripción inicial al cargar el informe
        }

        public void MarcarComoGuardado()
        {
            Guardado = true; // Marca el informe como guardado después de actualizar la descripción
        }

        public override string ToString()
        {
            return $"{FechaInforme.ToString("dd/MM/yyyy")} - {Descripcion}";
        }
    }
}
