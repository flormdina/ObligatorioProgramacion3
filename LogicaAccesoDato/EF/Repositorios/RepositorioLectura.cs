
using Microsoft.EntityFrameworkCore;
using ObligatorioDW;
using ObligatorioDW.Entidades;
using ObligatorioDW.InterfacesDelRepositorio;


namespace LogicaAccesoDato.EF.Repositorios
{
    public class RepositorioLectura : IRepositorioLectura
    {
        private readonly ObligatorioContext _context;

        public RepositorioLectura(ObligatorioContext context)
        {
            _context = context;
        }

        public void Add(Lectura lectura)
        {
            _context.Set<Lectura>().Add(lectura);
            _context.SaveChanges();
        }

        public Lectura? FindById(int id)
        {
            return _context.Set<Lectura>()
                .Include(l => l.Usuario)
                .Include(l => l.Historia)
                .Include(l => l.FinalAlcanzado)
                .FirstOrDefault(l => l.Id == id);
        }

        public void Update(Lectura lectura)
        {
            _context.Set<Lectura>().Update(lectura);
            _context.SaveChanges();
        }

        public IEnumerable<Lectura> FindAll()
        {
            return _context.Set<Lectura>()
                .Include(l => l.Usuario)
                .Include(l => l.Historia)
                .Include(l => l.FinalAlcanzado)
                .ToList();
        }

        public Lectura Iniciar(int usuarioId, int historiaId)
        {
            
            Usuario? usuario = _context.Set<Usuario>()
                .FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null)
                throw new ArgumentException("El usuario no existe.");

            Historia? historia = _context.Set<Historia>()
                .FirstOrDefault(h => h.Id == historiaId);

            if (historia == null)
                throw new ArgumentException("La historia no existe.");

            if (historia.Estado.ToString().ToUpper() != "PUBLICADA")
            {
                throw new ArgumentException("La historia no está publicada.");
            }
            
            var lectura = new Lectura
            {
                Usuario = usuario,
                Historia = historia,
                FechaInicio = DateTime.Now,
                FechaFinalizacion = null,
                FinalAlcanzado = null
            };

            _context.Set<Lectura>().Add(lectura);
            _context.SaveChanges();

            return lectura;
        }
        
        
        public void Finalizar(int lecturaId, int capituloFinalId, int usuarioId)
        {
            // Buscamos la lectura que queremos finalizar
            Lectura? lectura = _context.Set<Lectura>()
                .Include(l => l.Usuario)
                .Include(l => l.Historia)
                .FirstOrDefault(l => l.Id == lecturaId);

            if (lectura == null)
                throw new ArgumentException("La lectura no existe.");

            // Comprobamos que la lectura pertenece al usuario
            if (lectura.Usuario.Id != usuarioId)
                throw new ArgumentException("Esta lectura no pertenece al usuario.");

            // Evitamos finalizar dos veces la misma lectura
            if (lectura.FechaFinalizacion != null)
                throw new ArgumentException("La lectura ya fue finalizada.");

            // Buscamos el capítulo final
            CapituloFinal? capituloFinal = _context.Set<CapituloFinal>()
                .FirstOrDefault(c => c.Id == capituloFinalId);

            if (capituloFinal == null)
                throw new ArgumentException("El capítulo final no existe.");

            // Comprobamos que el final pertenece a esta historia
            bool pertenece = _context.Set<Historia>()
                .Include(h => h.Capitulos)
                .Any(h => h.Id == lectura.Historia.Id &&
                          h.Capitulos.Any(c => c.Id == capituloFinalId));

            if (!pertenece)
                throw new ArgumentException("El capítulo final no pertenece a esta historia.");

            // Guardamos la finalización
            lectura.FechaFinalizacion = DateTime.Now;
            lectura.FinalAlcanzado = capituloFinal;

            // Aumentamos el contador de ese final
            capituloFinal.Contador++;

            _context.SaveChanges();
        }
    }
}
