
using DTOs.DTOs;
using System.Collections.Generic;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IEditarHistoria
    {
        void Ejecutar(HistoriaDTO dto, List<int> categoriasSeleccionadas);
    }
}
