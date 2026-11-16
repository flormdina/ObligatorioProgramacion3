using DTOs.DTOs;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
using ObligatorioDW.InterfacesDeRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaDeAplicacion.CasosDeUso.Usuarios
{
    public class ListarUsuariosCU : IListarUsuarios
    {
        private IRepositorioUsuario repositorio;

        public ListarUsuariosCU(IRepositorioUsuario repo)
        {
            repositorio = repo;
        }

        public IEnumerable<UsuarioDTO> Ejecutar()
        {
            return repositorio.FindAll().Select(u => new UsuarioDTO
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Email = u.Email,
                NombreUsuario = u.NombreUsuario,
                Rol = u.Rol
            }).ToList();
        }
    }
}