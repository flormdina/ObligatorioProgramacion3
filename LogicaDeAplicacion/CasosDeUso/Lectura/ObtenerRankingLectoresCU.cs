using DTOs;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using ObligatorioDW.InterfacesDeRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Lecturas;

public class ObtenerRankingLectoresCU : IObtenerRankingLectores
{
    private readonly IRepositorioUsuario _repositorio;

    public ObtenerRankingLectoresCU(IRepositorioUsuario repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<RankingDTO> Ejecutar()
    {
        //return _repositorio.UserWithMoreSotries();
        return null;
    }
}