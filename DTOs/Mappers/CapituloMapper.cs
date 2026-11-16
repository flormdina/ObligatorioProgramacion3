using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using ObligatorioDW;
using ObligatorioDW.Entidades;

namespace DTOs.Mappers
{
    public class CapituloMapper
    {
        public static Capitulo FromDTO(CapituloDTO dto)
        {
            return new Capitulo
            {
                Titulo = dto.TItulo,
                Texto = dto.Texto,
                Id = dto.Id
            };
        }
        public static CapituloDTO ToDTO(Capitulo capitulo)
        {
            return new CapituloDTO
            {
                TItulo = capitulo.Titulo,
                Texto = capitulo.Texto,
                Id = capitulo.Id
            };
        }

    }
}
