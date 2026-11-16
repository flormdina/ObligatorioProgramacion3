using System.Security.Claims;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Lector")]
public class LecturasController : ControllerBase
{
    private readonly IIniciarLectura _iniciarLectura;
    private readonly IFinalizarLectura _finalizarLectura;
    private readonly IObtenerHistorialLecturas _obtenerHistorial;

    public LecturasController(
        IIniciarLectura iniciarLectura,
        IFinalizarLectura finalizarLectura,
        IObtenerHistorialLecturas obtenerHistorial)
    {
        _iniciarLectura = iniciarLectura;
        _finalizarLectura = finalizarLectura;
        _obtenerHistorial = obtenerHistorial;
    }

    [HttpPost("iniciar/{historiaId}")]
    public IActionResult Iniciar(int historiaId)
    {
        string? idUsuario = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(idUsuario, out int usuarioId))
        {
            return Unauthorized();
        }

        try
        {
            int lecturaId = _iniciarLectura.Ejecutar(
                usuarioId, historiaId);

            return Ok(new { lecturaId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("finalizar/{lecturaId}/{capituloFinalId}")]
    public IActionResult Finalizar(int lecturaId, int capituloFinalId)
    {
        string? idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idUsuario, out int usuarioId))
            return Unauthorized();

        try
        {
            _finalizarLectura.Ejecutar(lecturaId, capituloFinalId, usuarioId);

            return Ok("Lectura finalizada correctamente.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("historial")]
    public IActionResult Historial(int mes, int anio)
    {
        string? idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(idUsuario, out int usuarioId))
            return Unauthorized();

        if (mes < 1 || mes > 12 || anio < 1)
            return BadRequest("El mes o el año no son válidos.");

        var lecturas = _obtenerHistorial.Ejecutar(usuarioId, mes, anio);

        var resultado = lecturas.Select(l => new
        {
            id = l.Id,
            historia = l.Historia.Titulo,
            fechaInicio = l.FechaInicio,
            fechaFinalizacion = l.FechaFinalizacion,
            completada = l.FechaFinalizacion != null,
            finalAlcanzado = l.FinalAlcanzado?.Titulo
        });

        return Ok(resultado);
    }
}