using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using ObligatorioDW;
using DTOs.DTOs;
using ObligatorioDW.ValueObjects.Usuario;
using ObligatorioDW.InterfacesDeRepositorio;
using DTOs.Mappers;

namespace LogicaDeAplicacion.CasosDeUso.Usuario
{
    public class AgregarUsuarioCU : IAgregarUsuario
    {
        private IRepositorioUsuario repositorio;
        public AgregarUsuarioCU(IRepositorioUsuario repo)
        {
            repositorio = repo;
        }
        public void Ejecutar(UsuarioDTO dto)
        {
            ObligatorioDW.Usuario usuario = new ObligatorioDW.Usuario();
            usuario.Id = dto.Id;
            usuario.NombreCompleto = dto.NombreCompleto;
            usuario.Email = dto.Email;
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Password = new Password(dto.Password);
            usuario.Rol = dto.Rol;
            UsuarioMapper.FromDTO(dto);

            repositorio.Create(usuario);
        }
        public void Ejecutar(ObligatorioDW.Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
