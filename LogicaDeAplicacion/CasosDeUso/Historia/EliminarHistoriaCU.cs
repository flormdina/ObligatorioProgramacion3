using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using ObligatorioDW.InterfacesDeRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Historia
{
    public class EliminarHistoriaCU : IEliminarHistoria
    {
        private readonly IRepositorioHistoria _repo;

        public EliminarHistoriaCU(IRepositorioHistoria repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            _repo.Delete(id);
        }
    }
}