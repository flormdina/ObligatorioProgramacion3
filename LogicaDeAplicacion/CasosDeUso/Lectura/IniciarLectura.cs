using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using ObligatorioDW.InterfacesDelRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Lecturas;

public class IniciarLecturaCU : IIniciarLectura
{
    private readonly IRepositorioLectura _repositorio;

    public IniciarLecturaCU(IRepositorioLectura repositorio)
    {
        _repositorio = repositorio;
    }

    public int Ejecutar(int usuarioId, int historiaId)
    {
        var lectura = _repositorio.Iniciar(usuarioId, historiaId);

        return lectura.Id;
    }
}