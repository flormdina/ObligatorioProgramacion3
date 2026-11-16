using DTOs.DTOs; // <--- ESTO ES LO QUE LE DICE A C# DÓNDE BUSCAR UsuarioDTO Y LoginDTO
namespace LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;

public interface ILoginUsuario //cambio class por interface
{
    UsuarioDTO Ejecutar(LoginDTO dto);
}