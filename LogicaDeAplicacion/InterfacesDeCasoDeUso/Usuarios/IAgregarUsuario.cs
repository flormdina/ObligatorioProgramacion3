using DTOs.DTOs;
using ObligatorioDW;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios
{
    public interface IAgregarUsuario
    {
        public void Ejecutar(UsuarioDTO dto);
    }
}
