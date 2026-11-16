using DTOs.DTOs;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoriasController : ControllerBase
{
    public IObtenerTodasHistorias _obtenerHistorias;
    public IAgregarHistoria _agregarHistoria;

    public HistoriasController(
        IObtenerTodasHistorias obtenerHistorias,
        IAgregarHistoria agregarHistoria
        )
    {
        _obtenerHistorias = obtenerHistorias;
        _agregarHistoria = agregarHistoria;
    }

    
    [HttpGet]
    public ActionResult<IEnumerable<HistoriaDTO>> ObtenerHistorias()
    {
        var historias = _obtenerHistorias.Ejecutar()
            .Where(h => h.Estado == ObligatorioDW.Enums.Estado.Publicada)
            .ToList();

        return Ok(historias);
    }

    
    [HttpGet("{id}")]
    public IActionResult ObtenerHistoriaPorId(int id)
    {
        var historia = _obtenerHistorias.Ejecutar()
            .FirstOrDefault(h =>
                h.Id == id &&
                h.Estado == ObligatorioDW.Enums.Estado.Publicada);

        if (historia == null)
            return NotFound();

        var capitulos = historia.Capitulos.ToList();

        if (!capitulos.Any(c => c.Id == historia.CapituloInicial.Id))
            capitulos.Add(historia.CapituloInicial);

        var resultado = new
        {
            historia.Id,
            historia.Titulo,
            historia.Sinopsis,

            CapituloInicialId = historia.CapituloInicial.Id,

            Capitulos = capitulos.Select(c => new
            {
                c.Id,
                c.Titulo,
                c.Texto,

                Opciones = c is ObligatorioDW.Entidades.CapituloIntermedio intermedio
                    ? intermedio.Opciones.Select(o => new
                    {
                        o.Id,
                        o.Texto,
                        CapituloDestinoId = o.CapituloDestino.Id
                    }).ToList()
                    : new List<object>().Select(o => new
                    {
                        Id = 0,
                        Texto = "",
                        CapituloDestinoId = 0
                    }).ToList()
            }).ToList()
        };

        return Ok(resultado);
    }

    [HttpPost()]
    public ActionResult<IEnumerable<HistoriaDTO>> AgregarHistoria([FromBody]HistoriaDTO dto)
    {
        _agregarHistoria.Ejecutar(dto, new List<int>{1});
        return Created();
    }
}