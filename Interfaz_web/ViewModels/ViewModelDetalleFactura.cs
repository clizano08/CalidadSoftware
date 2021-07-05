using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interfaz_web.ViewModels
{
    public class ViewModelFacturaDetalle
    {
        public int Id { get; set; }
        public int Secuencia { get; set; } 
        public int IdProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int IdFactura { get; set; }
        public int Cantidad { get; set; }  
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }


    }
}