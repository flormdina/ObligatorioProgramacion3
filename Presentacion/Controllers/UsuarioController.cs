using DTOs.DTOs;
using LogicaDeAplicacion.CasosDeUso.Usuario;
using LogicaDeAplicacion.CasosDeUso.Usuarios;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        // (Tipo de dato / Nnombre de variable)
        private IAgregarUsuario agregarUsuario;//Llamamos al Caso de Uso

        //Llamo a la interfaz porque este cU dberia ser utilizado a traves de su interfaz
        private ILoginUsuario login;
        
        private IListarUsuarios listarUsuarios;



        public UsuarioController(IAgregarUsuario agregarUsuario,
            ILoginUsuario login,
            IListarUsuarios listarUsuarios)
        {
            this.agregarUsuario = agregarUsuario;
            this.login = login;
            this.listarUsuarios = listarUsuarios;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginDTO dto)
        {
            try
            {
                UsuarioDTO usuarioLogueado = login.Ejecutar(dto);

                // Solo los administradores pueden ingresar a esta aplicación.
                if (usuarioLogueado.Rol != "Administrador")
                {
                    ViewBag.Error = "Acceso exclusivo para administradores.";
                    return View(dto);
                }

                // Guardamos los datos del administrador en sesión.
                HttpContext.Session.SetInt32("UsuarioId", usuarioLogueado.Id);
                HttpContext.Session.SetString("UsuarioNombre", usuarioLogueado.NombreCompleto);
                HttpContext.Session.SetString("UsuarioRol", usuarioLogueado.Rol);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(dto);
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }




       // Si no hay sesión iniciada: lo mandamos al Login
       // Si hay sesión iniciada: puede acceder a la página
       public ActionResult Index()
       {
           if (HttpContext.Session.GetString("UsuarioRol") != "Administrador")
           {
               return RedirectToAction("Login");
           }

           IEnumerable<UsuarioDTO> usuarios = listarUsuarios.Ejecutar();

           return View(usuarios);
       }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       
        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Administrador")
            {
                return RedirectToAction("Login");
            }

            return View();
        }
        
        
        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTO usuario)
        {
            // Verificamos que haya un administrador con sesión iniciada
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login");
            }

            try
            {
                agregarUsuario.Ejecutar(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(usuario);
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
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

