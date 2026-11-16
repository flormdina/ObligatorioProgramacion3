using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using ObligatorioDW.InterfacesDelRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Lecturas;

public class FinalizarLecturaCU : IFinalizarLectura
{
    private readonly IRepositorioLectura _repositorio;

    public FinalizarLecturaCU(IRepositorioLectura repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar(int lecturaId, int capituloFinalId, int usuarioId)
    {
        _repositorio.Finalizar(lecturaId, capituloFinalId, usuarioId);
    }
}