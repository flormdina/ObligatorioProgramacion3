namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;

public interface IObtenerFinalesMasAlcanzados
{
    IEnumerable<(string Historia, string Final, int Cantidad)> Ejecutar();
}