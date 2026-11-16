using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IAgregarHistoria
    {
        void Ejecutar(HistoriaDTO dto, List<int> categoriasSeleccionadas);
    }
}
