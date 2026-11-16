
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTOs.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
namespace WebApi.Controllers;

using WebApi.Servicios;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginUsuario _loginUsuario;
    private readonly IConfiguration _configuration;

    public AuthController(
        ILoginUsuario loginUsuario,
        IConfiguration configuration,
        GeneradorJwt generadorJwt)
    {
        _loginUsuario = loginUsuario;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Debe ingresar email y contraseña.");
        }

        try
        {
            // Reutilizamos el caso de uso de login existente.
            var usuario = _loginUsuario.Ejecutar(new LoginDTO
            {
                Email = request.Email,
                Password = request.Password
            });

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas.");

            var token = GeneradorJwt.Generar(
                usuario.Id,
                usuario.Email,
                usuario.Rol.ToString(),
                _configuration
            );

            return Ok(new
            {
                token = token,
                usuario = usuario.Email,
                rol = usuario.Rol.ToString()
            });

        }
        catch
        {
            return StatusCode(500, "Error inesperado");
        }
    }
}
