//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(DetFacturaMetadata))]

    public partial class DetFactura
    {
        public int Id { get; set; }
        public int Producto_id { get; set; }
        public decimal EncFactura_id { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public int Secuencia { get; set; }
    
        public virtual EncFactura EncFactura { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
