using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IObtenerTodasHistorias
    {
        public IEnumerable<HistoriaDTO> Ejecutar();

    }
}
