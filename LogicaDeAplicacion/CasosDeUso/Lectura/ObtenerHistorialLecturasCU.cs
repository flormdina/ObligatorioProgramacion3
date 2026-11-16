using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using ObligatorioDW.Entidades;
using ObligatorioDW.InterfacesDelRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Lecturas;

public class ObtenerHistorialLecturasCU : IObtenerHistorialLecturas
{
    private readonly IRepositorioLectura _repositorio;

    public ObtenerHistorialLecturasCU(IRepositorioLectura repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<Lectura> Ejecutar(int usuarioId, int mes, int anio)
    {
        return _repositorio.FindAll()
            .Where(l => l.Usuario.Id == usuarioId &&
                        l.FechaInicio.Month == mes &&
                        l.FechaInicio.Year == anio)
            .ToList();
    }
}