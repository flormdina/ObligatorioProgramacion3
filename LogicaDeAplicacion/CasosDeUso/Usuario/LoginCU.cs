using DTOs.DTOs;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
using ObligatorioDW.InterfacesDeRepositorio;
using ObligatorioDW.Excepciones;
using ObligatorioDW;

namespace LogicaDeAplicacion.CasosDeUso.Usuarios;

public class LoginCU : ILoginUsuario
{
    private IRepositorioUsuario _repoUsuario;


    public LoginCU(IRepositorioUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }


  
    public UsuarioDTO Ejecutar(LoginDTO dto)
    {
        if (dto == null ||
            string.IsNullOrEmpty(dto.Email) ||
            string.IsNullOrEmpty(dto.Password))
        {
            throw new UsuarioException("El email y la contraseña son obligatorios");
        }

        ObligatorioDW.Usuario usuarioEncontrado =
            _repoUsuario.GetByEmail(dto.Email);

        if (usuarioEncontrado == null)
        {
            throw new UsuarioException("Usuario o contraseña incorrectos");
        }

        if (!usuarioEncontrado.Password.Verify(dto.Password))
        {
            throw new UsuarioException("Usuario o contraseña incorrectos");
        }

        return new UsuarioDTO
        {
            Id = usuarioEncontrado.Id,
            NombreUsuario = usuarioEncontrado.NombreUsuario,
            NombreCompleto = usuarioEncontrado.NombreCompleto,
            Email = usuarioEncontrado.Email,
            Rol = usuarioEncontrado.Rol
        };
    }

}