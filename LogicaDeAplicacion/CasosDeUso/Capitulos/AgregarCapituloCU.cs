
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using ObligatorioDW.Entidades;
using ObligatorioDW.InterfacesDeRepositorio;

namespace LogicaDeAplicacion.CasosDeUso.Capitulos
{
    public class AgregarCapituloCU : IAgregarCapitulo
    {
        private readonly IRepositorioHistoria _repoHistorias;
        private readonly IRepositorioCapitulo _repoCapitulos;

        public AgregarCapituloCU(
            IRepositorioHistoria repoHistorias,
            IRepositorioCapitulo repoCapitulos)
        {
            _repoHistorias = repoHistorias;
            _repoCapitulos = repoCapitulos;
        }

        public void Ejecutar(
            int historiaId,
            int capituloAnteriorId,
            string tipo,
            string titulo,
            string texto,
            string textoOpcion)
        {
            var historia = _repoHistorias.FindById(historiaId);

            if (historia == null)
                throw new ArgumentException("La historia no existe.");

            if (string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(texto) ||
                string.IsNullOrWhiteSpace(textoOpcion))
                throw new ArgumentException("Debe completar todos los campos.");

            var anterior = _repoCapitulos.FindById(capituloAnteriorId)
                as CapituloIntermedio;

            if (anterior == null)
                throw new ArgumentException(
                    "El capítulo anterior debe ser intermedio.");

            bool pertenece = historia.CapituloInicial.Id == anterior.Id ||
                historia.Capitulos.Any(c => c.Id == anterior.Id);

            if (!pertenece)
                throw new ArgumentException(
                    "El capítulo anterior no pertenece a esta historia.");

            Capitulo nuevo;

            if (tipo == "Intermedio")
            {
                nuevo = new CapituloIntermedio
                {
                    Titulo = titulo,
                    Texto = texto,
                    Opciones = new List<Opcion>()
                };
            }
            else if (tipo == "Final")
            {
                nuevo = new CapituloFinal
                {
                    Titulo = titulo,
                    Texto = texto,
                    Contador = 0
                };
            }
            else
            {
                throw new ArgumentException("Tipo de capítulo inválido.");
            }

            if (anterior.Opciones == null)
                anterior.Opciones = new List<Opcion>();

            anterior.Opciones.Add(new Opcion
            {
                Texto = textoOpcion,
                CapituloDestino = nuevo
            });

            historia.Capitulos.Add(nuevo);

            _repoCapitulos.Create(nuevo);
        }
    }
}
