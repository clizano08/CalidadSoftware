using Datos.Repository;
using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas_de_negocio.Services
{
    public class ServiceProducto
    {
        public void DeleteProducto(int id)
        {
            RepositoryProducto repository = new RepositoryProducto();
            repository.DeleteProducto(id);
        }

        public IEnumerable<Producto> GetProducto()
        {
            RepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }

       
        public Producto GetProductoByID(int id)
        {
            RepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }

        public IEnumerable<Producto> GetProductoByName(string name)
        {
            RepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByName(name);
        }

        public Producto Save(Producto producto)
        {
            RepositoryProducto repository = new RepositoryProducto();
            return repository.Save(producto);
        }


    }
}
