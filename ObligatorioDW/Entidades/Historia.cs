using ObligatorioDW.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioDW.Entidades
{
    public class Historia
    {
        private static int UltimoId = 0;

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public List<Categoria> Categorias { get; set; }
        
        public Capitulo CapituloInicial { get; set; }   //Capitulo por el cual arranca la historia
        
        public List<Capitulo> Capitulos { get; set; } = new List<Capitulo>();
        public Estado Estado { get; set; }


    }
}
