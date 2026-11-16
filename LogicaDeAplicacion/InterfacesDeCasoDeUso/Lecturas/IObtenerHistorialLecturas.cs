using ObligatorioDW.Entidades;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;

public interface IObtenerHistorialLecturas
{
    IEnumerable<Lectura> Ejecutar(int usuarioId, int mes, int anio);
}