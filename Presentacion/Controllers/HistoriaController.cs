using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using Microsoft.AspNetCore.Http;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Categorias;
using Microsoft.AspNetCore.Mvc;
using DTOs.DTOs;
using ObligatorioDW.Enums;

namespace Presentacion.Controllers
{
    public class HistoriaController : Controller
    {
        private readonly IAgregarHistoria agregarHistoria;
        private readonly IObtenerTodas obtenerCategorias;
        private readonly IEditarHistoria editarHistoria;
        private readonly IEliminarHistoria eliminarHistoria;
       
        private readonly IObtenerTodasHistorias obtenerHistorias;
        
        private readonly IAgregarCapitulo agregarCapitulo;
        
        public HistoriaController(
            IAgregarHistoria agregarHistoria,
            IObtenerTodas obtenerCategorias,
            IObtenerTodasHistorias obtenerHistorias,
            IEditarHistoria editarHistoria,
            IEliminarHistoria eliminarHistoria,
            IAgregarCapitulo agregarCapitulo)
        {
            this.agregarHistoria = agregarHistoria;
            this.obtenerCategorias = obtenerCategorias;
            this.obtenerHistorias = obtenerHistorias;
            this.editarHistoria = editarHistoria;
            this.eliminarHistoria = eliminarHistoria;
            this.agregarCapitulo = agregarCapitulo;
        }
        
        
        
        
        // GET: HistoriaController
        public ActionResult Index()
        {
            var historias = obtenerHistorias.Ejecutar();

            return View(historias);
        }

        // GET: HistoriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Categorias = obtenerCategorias.Ejecutar();

            return View();
        }

        // POST: HistoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HistoriaDTO dto, List<int> categoriasSeleccionadas)
        {
            try
            {
                // Asegurar que la historia nueva se crea en estado Borrador
                dto.Estado = Estado.Borrador;
                agregarHistoria.Ejecutar(dto, categoriasSeleccionadas);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Categorias = obtenerCategorias.Ejecutar();

                ModelState.AddModelError("", ex.Message);

                return View(dto);
            }
        }

        

        
        public ActionResult Edit(int id)
        {
            var historia = obtenerHistorias.Ejecutar()
                .FirstOrDefault(h => h.Id == id);

            if (historia == null)
                return NotFound();

            ViewBag.Categorias = obtenerCategorias.Ejecutar();

            return View(historia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            int id,
            HistoriaDTO dto,
            List<int> categoriasSeleccionadas)
        {
            try
            {
                dto.Id = id;

                editarHistoria.Ejecutar(dto, categoriasSeleccionadas);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Categorias = obtenerCategorias.Ejecutar();

                ModelState.AddModelError("", ex.Message);

                return View(dto);
            }
        }


       
        public ActionResult Delete(int id)
        {
            var historia = obtenerHistorias.Ejecutar()
                .FirstOrDefault(h => h.Id == id);

            if (historia == null)
                return NotFound();

            return View(historia);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                eliminarHistoria.Ejecutar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var historia = obtenerHistorias.Ejecutar()
                    .FirstOrDefault(h => h.Id == id);

                if (historia == null)
                    return NotFound();

                return View("Delete", historia);
            }
        }
        
        
        
        
        public ActionResult AgregarCapitulo(int id)
        {
            var historia = obtenerHistorias.Ejecutar()
                .FirstOrDefault(h => h.Id == id);

            if (historia == null)
                return NotFound();

            ViewBag.Historia = historia;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarCapitulo(
            int id,
            int capituloAnteriorId,
            string tipo,
            string titulo,
            string texto,
            string textoOpcion)
        {
            try
            {
                agregarCapitulo.Ejecutar(
                    id,
                    capituloAnteriorId,
                    tipo,
                    titulo,
                    texto,
                    textoOpcion);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var historia = obtenerHistorias.Ejecutar()
                    .FirstOrDefault(h => h.Id == id);

                if (historia == null)
                    return NotFound();

                ViewBag.Historia = historia;

                return View();
            }
        }


    }
}
