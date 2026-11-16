using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioDW.Entidades
{
    public class Capitulo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }

        public static implicit operator Capitulo(Historia v)
        {
            throw new NotImplementedException();
        }
    }
}
