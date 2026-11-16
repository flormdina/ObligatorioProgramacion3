using ObligatorioDW;
using ObligatorioDW.InterfacesDeRepositorio;
using System;
using System.Collections.Generic;
using System.Text;
using DTOs;
using DTOs.DTOs;
using DTOs.Mappers;

namespace LogicaAccesoDato.EF.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        // 1. Variable privada para almacenar el contexto
        private ObligatorioContext _db { get; set; }

        // 2. Constructor que RECIBE el contexto 
        public RepositorioUsuario(ObligatorioContext db)
        {
            _db = db;
        }

        private static List<Usuario> usuarios = new List<Usuario>();

        public void Add(Usuario nuevo)
        {
            if (nuevo == null)
            {
                throw new ArgumentException("El usuario esta vacio.");
            }

            nuevo.Validar();
            UsuarioUnico(nuevo);
            RepositorioUsuario.usuarios.Add(nuevo);
        }

        public void UsuarioUnico(Usuario usuario) //Que no se repita un usuario
        {
            foreach (Usuario u in usuarios)
            {
                if (u.Email == usuario.Email)
                {
                    throw new ArgumentException("Usuario ya existente.");
                }
            }
        }

        public Usuario FindById(int id) //Buscar usuario por ID
        {
            foreach (Usuario u in usuarios)
            {
                if (u.Id == id) return u;
            }

            throw new ArgumentException("Usuario no encontrado.");
        }

        public void Create(Usuario nuevo)
        {
            if (nuevo == null)
            {
                throw new ArgumentException("El usuario está vacío.");
            }

            nuevo.Validar();

            // Comprobamos si ya existe un usuario con ese email
            Usuario existente = GetByEmail(nuevo.Email);

            if (existente != null)
            {
                throw new ArgumentException("Ya existe un usuario con ese email.");
            }

            _db.Usuarios.Add(nuevo);
            _db.SaveChanges();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //_db.Usuarios: accede a la tabla Usuarios de nuestra base de datos Lectura2026
        // .ToList(): obtiene todos los usuarios y los devuelve en una lista
        // return: devuelve esa lista al caso de uso ListarUsuariosCU
        public IEnumerable<Usuario> FindAll()
        {
            return _db.Usuarios.ToList();
        }

        public void Update(Usuario nuevo)
        {
            throw new NotImplementedException();
        }

        public Usuario GetByEmail(string email)
        {
            //Busca en la tabla de Usuarios el primero que coincida con el email que recibi
            return _db.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        //RF11 – Usuario con más historias leídas – Rol Administrador Ranking de usuarios
        // según la cantidad de historias distintas que completaron (leyeron hasta un final),
        // ordenados de mayor a menor cantidad.


        //contar historias distintas que leyo// RF11 - Ranking de usuarios por historias distintas completadas
        public IEnumerable<RankingDTO> UserWithMoreSotries()
        {
            return _db.Lecturas
                .Where(l => l.FechaFinalizacion != null)
                .GroupBy(l => l.Usuario.Id)
                .OrderByDescending(grupo =>
                    grupo.Select(l => l.Historia.Id).Distinct().Count()
                )
                .Select(grupo => new RankingDTO
                {
                    Usuario = UsuarioMapper.ToDTO(grupo.Select(l => l.Usuario).FirstOrDefault()),
                    Cantidad = grupo.Select(l => l.Historia.Id).Distinct().Count()
                })
                .ToList();

        }
    }
}

