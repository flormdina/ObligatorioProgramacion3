using Microsoft.EntityFrameworkCore;
using ObligatorioDW.Entidades;
using ObligatorioDW.InterfacesDeRepositorio;
using System;
using LogicaAccesoDato.EF;
using System.Collections.Generic;
using System.Text;
using DTOs;

namespace LogicaAccesoDato.EF.Repositorios
{
    public class RepositorioHistoria : IRepositorioHistoria
    {
        
        
        private ObligatorioContext _db;

        public RepositorioHistoria(ObligatorioContext db)
        {
            _db = db;
        }
        
        
        
        public void Add(Historia historias)
        {
            throw new NotImplementedException();
        }

        
        public List<(int HistoriaId, string Historia, string Final, int Cantidad)> ObtenerFinalesPorHistoria()
        {
            throw new NotImplementedException();
        }


        public void Create(Historia nuevo)
        {
            if (nuevo == null)
            {
                throw new ArgumentException("La historia está vacía.");
            }

            _db.Historias.Add(nuevo);
            _db.SaveChanges();
        }
        
        
       
        
        public void Delete(int id)
        {
            Historia historia = FindById(id);

            if (historia == null)
                throw new ArgumentException("La historia no existe.");

            bool tieneLecturas = _db.Lecturas
                .Any(l => l.Historia.Id == id);

            if (tieneLecturas)
                throw new ArgumentException(
                    "No se puede eliminar la historia porque tiene lecturas asociadas");

            _db.Historias.Remove(historia);
            _db.SaveChanges();
        }

       
        
        public List<Historia> FindAll()
        {
            var historias = _db.Historias
                .Include(h => h.Categorias)
                .Include(h => h.CapituloInicial)
                .Include(h => h.Capitulos)
                .ToList();

            foreach (var historia in historias)
            {
                var capitulos = historia.Capitulos.ToList();

                if (historia.CapituloInicial != null &&
                    !capitulos.Any(c => c.Id == historia.CapituloInicial.Id))
                {
                    capitulos.Add(historia.CapituloInicial);
                }

                foreach (var capitulo in capitulos)
                {
                    if (capitulo is CapituloIntermedio intermedio)
                    {
                        _db.Entry(intermedio)
                            .Collection(c => c.Opciones)
                            .Load();

                        foreach (var opcion in intermedio.Opciones)
                        {
                            _db.Entry(opcion)
                                .Reference(o => o.CapituloDestino)
                                .Load();
                        }
                    }
                }
            }

            return historias;
        }


        
        //permite recuperar las opciones de los capítulos y sus destinos,
        //que necesitamos comprobar antes de publicar
        public Historia FindById(int id)
        {
            Historia historia = _db.Historias
                .Include(h => h.Categorias)
                .Include(h => h.CapituloInicial)
                .Include(h => h.Capitulos)
                .FirstOrDefault(h => h.Id == id);

            if (historia == null)
                return null;

            var capitulos = historia.Capitulos.ToList();

            if (!capitulos.Any(c => c.Id == historia.CapituloInicial.Id))
                capitulos.Add(historia.CapituloInicial);

            foreach (var capitulo in capitulos)
            {
                if (capitulo is CapituloIntermedio intermedio)
                {
                    _db.Entry(intermedio)
                        .Collection(c => c.Opciones)
                        .Load();

                    foreach (var opcion in intermedio.Opciones)
                    {
                        _db.Entry(opcion)
                            .Reference(o => o.CapituloDestino)
                            .Load();
                    }
                }
            }

            return historia;
        }

        
        public void Update(Historia nuevo)
        {
            if (nuevo == null)
            {
                throw new ArgumentException("La historia no existe.");
            }

            _db.Historias.Update(nuevo);
            _db.SaveChanges();
        }

        IEnumerable<Historia> IRepositorio<Historia>.FindAll()
        {
            return FindAll();
        }
    }
    
    
    /*public List<(int HistoriaId, string Historia, string Final, int Cantidad)>
        ObtenerFinalesPorHistoria()
    {
        var historias = _db.Historias
            .Include(h => h.Capitulos)
            .ToList();

        var cantidades = _db.Lecturas
            .Where(l => l.FechaFinalizacion != null &&
                        l.FinalAlcanzado != null)
            .GroupBy(l => l.FinalAlcanzado!.Id)
            .Select(g => new
            {
                FinalId = g.Key,
                Cantidad = g.Count()
            })
            .ToList();

        var resultado = historias
            .SelectMany(h => h.Capitulos
                .OfType<CapituloFinal>()
                .Select(f => (
                    HistoriaId: h.Id,
                    Historia: h.Titulo,
                    Final: f.Titulo,
                    FinalId: f.Id
                )))
            .Select(f => (
                f.HistoriaId,
                f.Historia,
                f.Final,
                Cantidad: cantidades
                    .Where(c => c.FinalId == f.FinalId)
                    .Select(c => c.Cantidad)
                    .FirstOrDefault()
            ))
            .OrderBy(r => r.Historia)
            .ThenByDescending(r => r.Cantidad)
            .ToList();

        return resultado;
    } */
}
