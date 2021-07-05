using Datos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repository
{
    public class RepositoryProducto
    {


   
        public void DeleteProducto(int id)
        {
            int returno;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                Producto oProducto = new Producto()
                {
                     Id = id
                };
                ctx.Entry(oProducto).State = EntityState.Deleted;
                returno = ctx.SaveChanges();
            }
        }
 
        public IEnumerable<Producto> GetProducto()
        {
            IEnumerable<Producto> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Producto.ToList();
            }

            return lista;
        }

        public Producto GetProductoByID(int id)
        {
            Producto oProducto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = ctx.Producto.
                            //Include("Bodega").
                            Where( p=> p.Id == id).FirstOrDefault();
            }
            return oProducto;
        }

        public IEnumerable<Producto> GetProductoByName(string name)
        {

            IEnumerable<Producto> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Producto.ToList<Producto>().
                                                      FindAll(p => p.Nombre.ToLower().
                                                      Contains(name.ToLower()));
            }
            return lista; 
        }

        public Producto Save(Producto producto)
        {
            int retorno = 0;
            Producto oProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = GetProductoByID((int)producto.Id);
                if (oProducto == null)
                {
                    ctx.Producto.Add(producto);
                }
                else
                {
                    ctx.Entry(producto).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges(); 
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)producto.Id);

            return oProducto;
        }
    }
}
