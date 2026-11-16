using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using ObligatorioDW.Entidades;

namespace DTOs.Mappers
{
    public class CategoriaMapper
    {
        public static Categoria FromDTO(CategoriaDTO dto)
        {
            return new Categoria
            {
                Nombre = dto.Nombre,
                Id = dto.Id
            };
        }

        public static CategoriaDTO ToDTO(Categoria categoria)
        {
            return new CategoriaDTO
            {
                Nombre = categoria.Nombre,
                Id = categoria.Id
            };
        }
    }
}
