using LogicaDeAplicacion.InterfacesDeCasoDeUso.Categorias;
using ObligatorioDW.InterfacesDeRepositorio;
using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using ObligatorioDW.Entidades;

namespace LogicaDeAplicacion.CasosDeUso.Categorias
{
    public class ObtenerTodasCU : IObtenerTodas
    {
        private IRepositorioCategoria repo { get; set; }
        public ObtenerTodasCU(IRepositorioCategoria repo)
        {
            this.repo = repo;
        }

        public IEnumerable<CategoriaDTO> Ejecutar()
        {
            List<CategoriaDTO> aRetornar = new List<CategoriaDTO>();
            foreach (Categoria categoria in repo.FindAll())
            {
                aRetornar.Add(CategoriaMapper.ToDTO(categoria));
            }

            return aRetornar;
        }
    }
}
