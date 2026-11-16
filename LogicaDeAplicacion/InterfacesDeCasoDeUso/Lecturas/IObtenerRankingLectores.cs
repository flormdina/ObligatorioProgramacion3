using DTOs;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;

public interface IObtenerRankingLectores
{
    IEnumerable<RankingDTO> Ejecutar();
}