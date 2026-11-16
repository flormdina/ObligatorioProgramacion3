using System;
using System.Collections.Generic;
using System.Text;
using ObligatorioDW.Entidades;
using ObligatorioDW.InterfacesDeRepositorio;

namespace LogicaAccesoDato.EF.Repositorios
{
    public class RepositorioCategoria : IRepositorioCategoria
    {
        private readonly ObligatorioContext context;

        public RepositorioCategoria(ObligatorioContext db)
        {
            context = db;
        }

        public IEnumerable<Categoria> FindAll()
        {
            return context.Categorias;
        }

        public Categoria FindById(int id)
        {
            return context.Categorias.Find(id);
        }

        public void Create(Categoria nuevo)
        {
            throw new NotImplementedException();
        }

        public void Update(Categoria nuevo)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Categoria FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
