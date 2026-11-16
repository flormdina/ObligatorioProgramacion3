using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers;

public class RankingController : Controller
{
    private readonly IObtenerRankingHistorias _ranking;
    private readonly IObtenerFinalesMasAlcanzados _finales;
    private readonly IObtenerRankingLectores _lectores;
    
    
    
    
    public RankingController(
        IObtenerRankingHistorias ranking,
        IObtenerFinalesMasAlcanzados finales,
        IObtenerRankingLectores lectores)
    {
        _ranking = ranking;
        _finales = finales;
        _lectores = lectores; 
    }

    public IActionResult Index()
    {
        if (!EsAdministrador())
            return RedirectToAction("Login", "Usuario");

        return View(_ranking.Ejecutar());
    }

    public IActionResult Finales()
    {
        if (!EsAdministrador())
            return RedirectToAction("Login", "Usuario");

        return View(_finales.Ejecutar());
    }

    
    public IActionResult Lectores()
    {
        if (!EsAdministrador())
            return RedirectToAction("Login", "Usuario");

        return View(_lectores.Ejecutar());
    }
    
    
    
    private bool EsAdministrador()
    {
        return HttpContext.Session.GetInt32("UsuarioId") != null &&
               HttpContext.Session.GetString("UsuarioRol") == "Administrador";
    }
}