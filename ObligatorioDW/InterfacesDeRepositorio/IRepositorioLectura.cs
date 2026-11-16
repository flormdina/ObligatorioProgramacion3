using ObligatorioDW.Entidades;

namespace ObligatorioDW.InterfacesDelRepositorio
{
    public interface IRepositorioLectura
    {
        void Add(Lectura lectura);

        Lectura? FindById(int id);

        void Update(Lectura lectura);

        IEnumerable<Lectura> FindAll();
        
        Lectura Iniciar(int usuarioId, int historiaId);
        
        void Finalizar(int lecturaId, int capituloFinalId, int usuarioId);
    }
}