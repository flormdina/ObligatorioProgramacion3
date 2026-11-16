using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioDW.InterfacesDeRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        public IEnumerable<T> FindAll();
        public T FindById(int id);
        public void Create(T nuevo);
        public void Update(T nuevo);
        public void Delete(int id);
    }
}
