
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using ObligatorioDW.InterfacesDeRepositorio;
using System;
using System.Collections.Generic;
using DTOs.DTOs;
using DTOs.Mappers;
using ObligatorioDW.Entidades;
using ObligatorioDW.Enums;

namespace LogicaDeAplicacion.CasosDeUso.Historia
{
    public class AgregarHistoriaCU : IAgregarHistoria
    {
        private readonly IRepositorioHistoria _repo;
        private readonly IRepositorioCategoria _repoCategorias;

        public AgregarHistoriaCU(
            IRepositorioHistoria repo,
            IRepositorioCategoria repoCategorias)
        {
            _repo = repo;
            _repoCategorias = repoCategorias;
        }

        public void Ejecutar(HistoriaDTO dto, List<int> categoriasSeleccionadas)
        {
            if (dto == null)
            {
                throw new ArgumentException("La historia está vacía.");
            }

            if (dto.CapituloInicial == null)
            {
                throw new ArgumentException("Debe ingresar un capítulo inicial.");
            }

            if (categoriasSeleccionadas == null || categoriasSeleccionadas.Count == 0)
            {
                throw new ArgumentException("Debe seleccionar al menos una categoría.");
            }

            var historia = HistoriaMapper.FromDTO(dto);

            historia.Estado = Estado.Borrador;

            historia.CapituloInicial = new CapituloIntermedio
            {
                Titulo = dto.CapituloInicial.Titulo,
                Texto = dto.CapituloInicial.Texto,
                Opciones = new List<Opcion>()
            };

            historia.Categorias = new List<Categoria>();

            foreach (int id in categoriasSeleccionadas)
            {
                var categoria = _repoCategorias.FindById(id);

                if (categoria == null)
                {
                    throw new ArgumentException("La categoría no existe.");
                }

                historia.Categorias.Add(categoria);
            }

            _repo.Create(historia);
        }
    }
}
