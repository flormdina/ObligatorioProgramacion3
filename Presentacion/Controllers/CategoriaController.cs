using LogicaAccesoDato.EF.Repositorios;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Categorias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class CategoriaController : Controller
    {
        private IObtenerTodas obtenerTodasCU
        {
            get; set;
        }

        public CategoriaController(IObtenerTodas obtenerTodas)
        {
            this.obtenerTodasCU = obtenerTodas;
        }

        // GET: CategoriaController1
        public ActionResult Index()
        {

            return View(obtenerTodasCU.Ejecutar());
        }

        // GET: CategoriaController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriaController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriaController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriaController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
