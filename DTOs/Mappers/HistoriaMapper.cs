using ObligatorioDW.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace DTOs.Mappers
{
    public class HistoriaMapper
    {
        public static Historia FromDTO(HistoriaDTO dto)
        {
            return new Historia
            {
                Titulo = dto.Titulo,
                Sinopsis = dto.Sinopsis,
                Categorias = dto.Categorias,
                CapituloInicial = dto.CapituloInicial,
                Estado = dto.Estado,
                Id = dto.Id
            };
        }
        public static HistoriaDTO ToDTO(Historia historia)
        {
            return new HistoriaDTO
            {
                Titulo = historia.Titulo,
                Sinopsis = historia.Sinopsis,
                Categorias = historia.Categorias,
                CapituloInicial = historia.CapituloInicial,
                Capitulos = historia.Capitulos,
                Estado = historia.Estado,
                Id = historia.Id
            };
        }
    }
}
