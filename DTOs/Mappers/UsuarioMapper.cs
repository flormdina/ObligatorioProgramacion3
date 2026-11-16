using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using ObligatorioDW;
using ObligatorioDW.Entidades;
using ObligatorioDW.ValueObjects;
using ObligatorioDW.ValueObjects.Usuario;

namespace DTOs.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario FromDTO(UsuarioDTO dto)
        {
            return new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Email = dto.Email,
                NombreUsuario = dto.NombreUsuario,
                Password = new Password(dto.Password),
                Rol = dto.Rol,
                Id = dto.Id
            };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                Password = usuario.Password.Value,
                Rol = usuario.Rol,
                Id = usuario.Id
            };
        }
    }
}
