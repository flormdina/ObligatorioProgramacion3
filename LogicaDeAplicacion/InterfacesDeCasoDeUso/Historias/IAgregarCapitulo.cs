namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IAgregarCapitulo
    {
        void Ejecutar(
            int historiaId,
            int capituloAnteriorId,
            string tipo,
            string titulo,
            string texto,
            string textoOpcion);
    }
}