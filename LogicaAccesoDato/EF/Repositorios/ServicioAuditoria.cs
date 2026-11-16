using ObligatorioDW.Entidades;
using DTOs.DTOs;
namespace LogicaAccesoDato.EF.Repositorios;

public class ServicioAuditoria
{
    private readonly ObligatorioContext _db;

    public ServicioAuditoria(ObligatorioContext db)
    {
        _db = db;
    }

    public void Registrar(int historiaId, int administradorId, string accion)
    {
        Auditoria auditoria = new Auditoria
        {
            HistoriaId = historiaId,
            AdministradorId = administradorId,
            Accion = accion,
            Fecha = DateTime.Now
        };

        _db.Auditorias.Add(auditoria);
        _db.SaveChanges();
    }
  
    
    // para RF05 - Auditoria
    public IEnumerable<AuditoriaDTO> Auditoria()
    {
        return _db.Auditorias
            .OrderByDescending(a => a.Fecha)
            .Select(a => new AuditoriaDTO
            {
                Id = a.Id,
                HistoriaId = a.HistoriaId,
                AdministradorId = a.AdministradorId,
                Accion = a.Accion,
                Fecha = a.Fecha
            })
            .ToList();
    }
}