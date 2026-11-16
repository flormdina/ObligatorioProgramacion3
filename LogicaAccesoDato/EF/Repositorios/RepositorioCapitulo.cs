using LogicaAccesoDato.EF;
using ObligatorioDW.Entidades;
using ObligatorioDW.InterfacesDeRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDato.EF.Repositorios
{
    public class RepositorioCapitulo : IRepositorioCapitulo
    {
        private readonly ObligatorioContext _db;

        public RepositorioCapitulo(ObligatorioContext db)
        {
            _db = db;
        }

        
        public Capitulo FindById(int id)
        {
            return _db.Capitulos
                .OfType<CapituloIntermedio>()
                .Include(c => c.Opciones)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Create(Capitulo capitulo)
        {
            _db.Capitulos.Add(capitulo);
            _db.SaveChanges();
        }
    }
}