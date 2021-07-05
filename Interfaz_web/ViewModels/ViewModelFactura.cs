using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interfaz_web.ViewModels
{
    public class ViewModelFactura
    {
        public int IdFactura { get; set; }
        public string IdUsuario { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string EstadoFactura { get; set; }

        //public decimal Vuelto { get; set; }

        public virtual List<ViewModelFacturaDetalle> FacturaDetalle { get; set; } 

    } 

}