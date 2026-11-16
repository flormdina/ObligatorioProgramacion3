
using DTOs.DTOs;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using ObligatorioDW.Entidades;
using ObligatorioDW.Enums;
using ObligatorioDW.InterfacesDeRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Historia
{
    public class EditarHistoriaCU : IEditarHistoria
    {
        private readonly IRepositorioHistoria _repo;
        private readonly IRepositorioCategoria _repoCategorias;

        public EditarHistoriaCU(
            IRepositorioHistoria repo,
            IRepositorioCategoria repoCategorias)
        {
            _repo = repo;
            _repoCategorias = repoCategorias;
        }

        public void Ejecutar(HistoriaDTO dto, List<int> categoriasSeleccionadas)
        {
            if (dto == null)
                throw new ArgumentException("La historia está vacía.");

            ObligatorioDW.Entidades.Historia historia = _repo.FindById(dto.Id);

            if (historia == null)
                throw new ArgumentException("La historia no existe.");

            if (categoriasSeleccionadas == null || categoriasSeleccionadas.Count == 0)
                throw new ArgumentException("Debe seleccionar al menos una categoría.");

           
            if (dto.Estado == Estado.Publicada)
            {
                var capitulos = historia.Capitulos.ToList();

                if (!capitulos.Any(c => c.Id == historia.CapituloInicial.Id))
                    capitulos.Add(historia.CapituloInicial);

                if (!capitulos.Any(c => c is CapituloFinal))
                    throw new ArgumentException(
                        "La historia debe tener al menos un capítulo final.");

                foreach (var capitulo in capitulos)
                {
                    if (capitulo is CapituloIntermedio intermedio)
                    {
                        if (intermedio.Opciones == null ||
                            intermedio.Opciones.Count == 0)
                        {
                            throw new ArgumentException(
                                "Todos los capítulos intermedios deben tener al menos una opción.");
                        }

                        foreach (var opcion in intermedio.Opciones)
                        {
                            if (opcion.CapituloDestino == null)
                                throw new ArgumentException(
                                    "Todas las opciones deben tener un capítulo de destino.");

                            bool destinoPertenece =
                                capitulos.Any(c => c.Id == opcion.CapituloDestino.Id);

                            if (!destinoPertenece)
                                throw new ArgumentException(
                                    "El destino de una opción no pertenece a esta historia.");
                        }
                    }
                }
            }

            
            
            
            
            
            
            
            
            
            
            
            historia.Titulo = dto.Titulo;
            historia.Sinopsis = dto.Sinopsis;
            
            if (dto.CapituloInicial != null)
            {
                bool hayOtrosCapitulos = historia.Capitulos
                    .Any(c => c.Id != historia.CapituloInicial.Id);

                if (hayOtrosCapitulos)
                {
                    if (dto.CapituloInicial.Titulo != historia.CapituloInicial.Titulo ||
                        dto.CapituloInicial.Texto != historia.CapituloInicial.Texto)
                    {
                        throw new ArgumentException(
                            "No se puede modificar el capítulo inicial porque la historia ya tiene otros capítulos.");
                    }
                }
                else
                {
                    historia.CapituloInicial.Titulo = dto.CapituloInicial.Titulo;
                    historia.CapituloInicial.Texto = dto.CapituloInicial.Texto;
                }
            }
            
            historia.Categorias.Clear();

            foreach (int id in categoriasSeleccionadas)
            {
                Categoria categoria = _repoCategorias.FindById(id);

                if (categoria == null)
                    throw new ArgumentException("La categoría no existe.");

                historia.Categorias.Add(categoria);
            }

            historia.Estado = dto.Estado;

            _repo.Update(historia);
        }
    }
}
