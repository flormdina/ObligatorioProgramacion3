using ObligatorioDW.Entidades;

namespace ObligatorioDW.InterfacesDeRepositorio
{
    public interface IRepositorioCapitulo
    {
        Capitulo FindById(int id);
        void Create(Capitulo capitulo);
    }
}