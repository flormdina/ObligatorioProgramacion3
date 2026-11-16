using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioDW.Entidades
{
    public class Opcion
    {
        public int Id { get; set; }

        public string Texto { get; set; }

        public Capitulo CapituloDestino { get; set; }
    }
}