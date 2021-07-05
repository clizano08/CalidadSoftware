using Datos.Models;
using Datos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas_de_negocio.Services
{
    public class ServiceFactura
    {
        public void DeleteFactura(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EncFactura> GetFactura()
        {
            throw new NotImplementedException();
        }

        public EncFactura GetFacturaByID(int id)
        {
            RepositoryFactura _RepositoryFactura = new RepositoryFactura();
            return _RepositoryFactura.GetFacturaByID(id);

        }

        public int GetNextConsecutivo()
        {
            RepositoryFactura _RepositoryFactura = new RepositoryFactura();
            return _RepositoryFactura.GetNextConsecutivo();
        }

        public EncFactura Save(EncFactura facturaEncabezado)
        {
            
            RepositoryFactura _RepositoryFactura = new RepositoryFactura();
            EncFactura fTemp = _RepositoryFactura.Save(facturaEncabezado);
            Descuento(fTemp);

            return _RepositoryFactura.Update(fTemp);
        }

        public EncFactura GetSecuenciaFactura(int id)
        {
            RepositoryFactura _RepositoryFactura = new RepositoryFactura();
            return _RepositoryFactura.GetSecuenciaFactura(id);
        }



        public void Descuento(EncFactura encFactura)
        {

           

           
            int dif = (from x in encFactura.DetFactura
                        select x.Producto_id).Distinct().Count();

       
            encFactura.Total = encFactura.Subtotal;


            if (dif >= 3 && encFactura.Subtotal>=10000)
            {
                encFactura.Descuento = encFactura.Subtotal * (encFactura.Descuento/100);
                encFactura.Total = encFactura.Subtotal - encFactura.Descuento;
            }
            else
            {

                EncFactura factura = encFactura;
                encFactura.Descuento = 0;
            }
           
            encFactura.Total -= Promocion((int)encFactura.Id);
            
        }

        public decimal Promocion(int Id_Factura)
        {
            
            ServiceFactura  _serviceFactura = new ServiceFactura();
            ServiceProducto _serviceProducto = new ServiceProducto();


            EncFactura encFactura = _serviceFactura.GetFacturaByID(Id_Factura);
            Producto prod = _serviceProducto.GetProductoByName("Gelatina").First();
            decimal promo = 0;
            int contador = 0;
            foreach (var item in encFactura.DetFactura)
            {
                if (item.Producto.Nombre == prod.Nombre)
                    contador++;
                if(item.Cantidad ==2 && item.Producto.Nombre == prod.Nombre)
                {
                    contador = 2;
                }
            }

            if (contador == 2)
            {
                promo = prod.Precio;
            }

            return promo;
        }

        public EncFactura Update(EncFactura facturaEncabezado)
        {
            RepositoryFactura _RepositoryFactura = new RepositoryFactura();
            return _RepositoryFactura.Update(facturaEncabezado);
        }
    }
}
