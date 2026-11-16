namespace ObligatorioDW.Entidades;



public class Auditoria
{
    public int Id { get; set; }
    public int HistoriaId { get; set; }
    public int AdministradorId { get; set; }
    public string Accion { get; set; } = "";
    public DateTime Fecha { get; set; }

}