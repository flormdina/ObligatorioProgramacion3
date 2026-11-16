using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using ObligatorioDW.InterfacesDelRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Lecturas;

public class ObtenerRankingHistoriasCU : IObtenerRankingHistorias
{
    private readonly IRepositorioLectura _repositorio;

    public ObtenerRankingHistoriasCU(IRepositorioLectura repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<(string Titulo, int Cantidad)> Ejecutar()
    {
        return _repositorio.FindAll()
            .Where(l => l.FechaFinalizacion != null)
            .GroupBy(l => l.Historia.Id)
            .Select(grupo => (
                Titulo: grupo.First().Historia.Titulo,
                Cantidad: grupo.Count()
            ))
            .OrderByDescending(resultado => resultado.Cantidad)
            .ToList();
    }
}