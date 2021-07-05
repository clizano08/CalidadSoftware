using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Models
{
    internal partial class EncFacturaMetadata
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "No Factura")]
        public int Id { get; set; }
        [Display(Name = "Nombre del Cliente")]
        public string NombreCliente { get; set; }
        [Display(Name = "Tipo de Pago")]
        public string TipoPago { get; set; }
    }
    internal partial class DetFacturaMetadata
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "No Detalle")]
        public int Id { get; set; }
        [Display(Name = "Código")]
        public int EncFactura_id { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Código")]
        public int Producto_id { get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Cantidad")]
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Identificación")]
        public string Id { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }

    internal partial class ProductoMetadata
    {
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

    }
}
