using ObligatorioDW.Enums;
using ObligatorioDW.InterfacesDelDominio;
using ObligatorioDW.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ObligatorioDW
{
    public class Usuario : IValidable
    {
        private static int UltimoId = 0;
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }

        //ValueObject de password
        public Password Password { get; set; }
        public string Rol { get; set; }

        //Validaciones

        public void Validar()
        {
            ValidarNombreCompleto();
            ValidarEmail();
            ValidarNombreUsuario();
            Password.Validar(); //Lamamos a las validaciones del ValueObject Password
        }
        public void ValidarNombreCompleto()
        {
            if (string.IsNullOrWhiteSpace(NombreCompleto))
            {
                throw new ArgumentException("El nombre completo no puede estar vacío.");
            }
        }
        public void ValidarEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new ArgumentException("El email no puede estar vacío.");
            }
        }
        public void ValidarNombreUsuario()
        {
            if (string.IsNullOrWhiteSpace(NombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            }
        }
    }
}
