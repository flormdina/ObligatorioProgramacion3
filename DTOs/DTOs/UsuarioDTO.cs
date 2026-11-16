
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }

        //Value Object Password
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
