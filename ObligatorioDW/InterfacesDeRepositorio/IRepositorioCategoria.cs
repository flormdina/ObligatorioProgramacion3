using System;
using System.Collections.Generic;
using System.Text;
using ObligatorioDW.Entidades;

namespace ObligatorioDW.InterfacesDeRepositorio
{
    public interface IRepositorioCategoria : IRepositorio<Categoria>
    {
        public Categoria FindByName(string name);
    }
}
