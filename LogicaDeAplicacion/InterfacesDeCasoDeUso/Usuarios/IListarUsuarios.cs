using DTOs.DTOs;
using System.Collections.Generic;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios
{
    public interface IListarUsuarios
    {
        IEnumerable<UsuarioDTO> Ejecutar(); //devolvera lista de usuarios
    }
}