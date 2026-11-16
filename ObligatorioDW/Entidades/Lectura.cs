using System;

namespace ObligatorioDW.Entidades
{
    public class Lectura
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public Historia Historia { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        public CapituloFinal? FinalAlcanzado { get; set; }
    }
}