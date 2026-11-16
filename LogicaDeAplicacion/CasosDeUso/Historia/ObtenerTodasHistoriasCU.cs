using DTOs.DTOs;
using DTOs.Mappers;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using ObligatorioDW.InterfacesDeRepositorio;
using System;
using System.Collections.Generic;
using System.Text;
using ObligatorioDW.Entidades;


namespace LogicaDeAplicacion.CasosDeUso.Historia
{
    public class ObtenerTodasHistoriasCU : IObtenerTodasHistorias
    {
        private IRepositorioHistoria repo { get; set; }
        public ObtenerTodasHistoriasCU(IRepositorioHistoria repo)
        {
            this.repo = repo;
        }

        public IEnumerable<HistoriaDTO> Ejecutar()
        {
            List<HistoriaDTO> aRetornar = new List<HistoriaDTO>();
            foreach (ObligatorioDW.Entidades.Historia historia in repo.FindAll())
            {
                aRetornar.Add(HistoriaMapper.ToDTO(historia));
            }

            return aRetornar;
        }
    }
}

