namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;

public interface IObtenerRankingHistorias
{
    IEnumerable<(string Titulo, int Cantidad)> Ejecutar();
}