using ObligatorioDW.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioDW.InterfacesDeRepositorio
{
    public interface IRepositorioHistoria : IRepositorio<Historia>
    {
        
        
        List<Historia> FindAll();
        Historia FindById(int id);
        void Add(Historia historias);
        
        List<(int HistoriaId, string Historia, string Final, int Cantidad)>
            ObtenerFinalesPorHistoria();
    }
}
