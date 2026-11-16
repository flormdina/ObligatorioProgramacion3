using System;
using System.Collections.Generic;
using System.Text;
using ObligatorioDW.Entidades;

namespace ObligatorioDW.InterfacesDeRepositorio
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        void Add(Usuario usuario);
        public void UsuarioUnico(Usuario verificar);

        Usuario GetByEmail(string email);

        //public Usuario BuscarPorNombreUsuario(string nombreUsuario);  
        
        //IEnumerable<RankingDTO> UserWithMoreSotries();
    }
}
