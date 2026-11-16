using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using ObligatorioDW.InterfacesDelRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Lecturas;

public class ObtenerFinalesMasAlcanzadosCU : IObtenerFinalesMasAlcanzados
{
    private readonly IRepositorioLectura _repositorio;

    public ObtenerFinalesMasAlcanzadosCU(IRepositorioLectura repositorio)
    {
        _repositorio = repositorio;
    }

    
    //busca lecturas completdas, las agrupa por historia y cuenta cual de sus finales fue alcanzado mas veces 
    public IEnumerable<(string Historia, string Final, int Cantidad)> Ejecutar() //corregir
    {
        return _repositorio.FindAll()
            .Where(l => l.FechaFinalizacion != null && l.FinalAlcanzado != null)
            .GroupBy(l => l.Historia.Id)
            .Select(grupoHistoria =>
                grupoHistoria
                    .GroupBy(l => l.FinalAlcanzado!.Id)
                    .Select(grupoFinal => (
                        Historia: grupoHistoria.First().Historia.Titulo,
                        Final: grupoFinal.First().FinalAlcanzado!.Titulo,
                        Cantidad: grupoFinal.Count()
                    ))
                    .OrderByDescending(resultado => resultado.Cantidad)
                    .First()
            )
            .OrderBy(resultado => resultado.Historia)
            .ToList();
    }
}