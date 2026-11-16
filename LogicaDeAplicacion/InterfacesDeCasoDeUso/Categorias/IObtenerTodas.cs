using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Categorias
{
    public interface IObtenerTodas
    {
        public IEnumerable<CategoriaDTO> Ejecutar();
    }
}
