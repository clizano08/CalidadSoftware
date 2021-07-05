using Datos.Models;
using Interfaz_web.Security;
using Reglas_de_negocio.Services;
//using App_VentaSnacks.Utils;
using Interfaz_web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Interfaz_web;

namespace App_VentaSnacks.Controllers
{

   [CustomAuthenticationFilter]
    public class FacturaController : Controller
    {
        
        public ActionResult Index()
        {

           
            List<int> numeros = new List<int>();
            int i;
            for (i = 1; i <= 10; i++)
            {
                numeros.Add(i);
            }

            ViewBag.Descuento = numeros;



            List<string> TipoPago = new List<string>()
            {
                "Seleccionar",
                "Efectivo",
                "Tarjeta"
                
            };

            ViewBag.TipoPago = TipoPago;


            try
            {
               

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        public ActionResult Save(EncFactura encabezado)
        {



            Usuario user = null;
            try
            {
                // Si no existe la sesión no hay nada
                if (Session["CarritoCompras"] == null)
                {
                    // Validaciones de datos requeridos.
                    TempData["Message"] = "No existen datos por factura!";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
                else
                {

                    var listaDetalle = (List<ViewModelFacturaDetalle>)Session["CarritoCompras"];

                    foreach (var item in listaDetalle)
                    {
                        DetFactura facturaDetalle = new DetFactura();
                        facturaDetalle.Id = item.Id;
                        facturaDetalle.Secuencia = item.Secuencia;
                        facturaDetalle.Producto_id = item.IdProducto;
                        facturaDetalle.Impuesto = item.Impuesto;
                        facturaDetalle.Precio = item.Precio;
                        facturaDetalle.Cantidad = item.Cantidad;
                        facturaDetalle.Total = (item.Cantidad * item.Precio) + item.Impuesto;
                        encabezado.DetFactura.Add(facturaDetalle);
                    }
                }



                user = (Usuario)Session["User"];
                //encabezado.Usuario = user;
                encabezado.Usuario_id = user.Id;


                var prueba = encabezado;


                ServiceFactura _ServiceFactura = new ServiceFactura();
                EncFactura factura = (EncFactura)_ServiceFactura.Save(encabezado);

                // Limpia el Carrito de compras
                Session["CarritoCompras"] = null;




                return View("ReporteFactura",factura);
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error: " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }


        public ActionResult CargarTabla(int? codigoproducto, int? cantidadproducto)
        {
            ViewModelRespuesta respuesta = new ViewModelRespuesta();
            List<ViewModelFacturaDetalle> lista = new List<ViewModelFacturaDetalle>();
            ViewModelFacturaDetalle detalleViewModel = new ViewModelFacturaDetalle();
            ServiceProducto _ServiceProducto = new ServiceProducto();
            ServiceImpuesto _ServiceImpuesto = new ServiceImpuesto();
            // Configuración del Serializador  
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };

            if (Session["CarritoCompras"] != null)
            {
                lista = (List<ViewModelFacturaDetalle>)Session["CarritoCompras"];
            }

            // Agregue la lista a la sesión
            Session["CarritoCompras"] = lista;

            // Serializador Newtonsoft.Json
            string json = JsonConvert.SerializeObject(lista, settings);

            return Content(json);

        }







        /// <summary>
        /// Agrega el producto al Carrito de compras que está en una sessión
        /// </summary>
        /// <param name="codigoproducto"></param>
        /// <param name="cantidadproducto"></param>
        /// <returns></returns>
        public ActionResult AddProducto(int? codigoproducto, int? cantidadproducto)
        {
            ViewModelRespuesta respuesta = new ViewModelRespuesta();
            List<ViewModelFacturaDetalle> lista = new List<ViewModelFacturaDetalle>();
            ViewModelFacturaDetalle detalleViewModel = new ViewModelFacturaDetalle();
            ServiceProducto _ServiceProducto = new ServiceProducto();
            ServiceImpuesto _ServiceImpuesto = new ServiceImpuesto();

            bool isError = false;
            string mensaje = "";

            // Configuración del Serializador  
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };


            if (codigoproducto == null)
            {
                respuesta = new ViewModelRespuesta()
                {
                    Mensaje = "Error general"
                };
                return Content(JsonConvert.SerializeObject(respuesta, settings));
            };
            

            if (cantidadproducto == null)
            {
                respuesta = new ViewModelRespuesta()
                {
                    Mensaje = "Error general"
                };

                return Content(JsonConvert.SerializeObject(respuesta, settings));
            };



            //Producto oProducto = _ServiceProducto.GetProductoByIDWithInventary(codigoproducto.Value, cantidadproducto.Value, ref isError, ref mensaje);
            Producto oProducto = _ServiceProducto.GetProductoByID(codigoproducto.Value);


            if (isError == true)
            {
                respuesta = new ViewModelRespuesta()
                {
                    Mensaje = mensaje
                };
                var errorJson = JsonConvert.SerializeObject(respuesta, settings); ;
                return Content(errorJson);
            }


            if (oProducto != null)
            {
                // Lee el impuesto
                Impuesto oImpuesto =_ServiceImpuesto.GetImpuesto();

                // Pobla el viewmodel
                detalleViewModel.Secuencia = 0;
                detalleViewModel.IdProducto = oProducto.Id;
                detalleViewModel.DescripcionProducto = oProducto.Nombre;
                detalleViewModel.Precio = oProducto.Precio  ;
                detalleViewModel.Impuesto = (oProducto.Precio *  cantidadproducto.Value) * (( (decimal)oImpuesto.Porcentaje /100 ));
                detalleViewModel.Cantidad =  cantidadproducto.Value ;
                //detalleViewModel.Imagen = oProducto.Imagen;

                // No existe la sesión ?
                if (Session["CarritoCompras"] != null)
                {
                    
                    lista = (List<ViewModelFacturaDetalle>)Session["CarritoCompras"];
                    detalleViewModel.Secuencia = lista.Count + 1;
                    lista.Add(detalleViewModel);
                }
                else
                {
                    detalleViewModel.Secuencia = lista.Count + 1;
                    lista.Add(detalleViewModel);
                }
            }
            // Agregue la lista a la sesión
            Session["CarritoCompras"] = lista;
            
            // Serializador Newtonsoft.Json
            string json = JsonConvert.SerializeObject(lista, settings);

            return Content(json);

        }

        public JsonResult DeleteItem(int? id)
        {
            List<ViewModelFacturaDetalle> lista = new List<ViewModelFacturaDetalle>();
            int idx = 0;

            if (id == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

            if (Session["CarritoCompras"] != null)
            {
                lista = (List<ViewModelFacturaDetalle>)Session["CarritoCompras"];
                // la posición del producto en la lista
                idx = lista.FindIndex(p => p.Secuencia == id);
                // remuevalo con base al indice.
                lista.RemoveAt(idx);
                // salve la lista en el carrito de compras
                Session["CarritoCompras"] = lista;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearShoppingCar()
        {

            // Limpia el Carrito de compras
            Session["CarritoCompras"] = null;

            return RedirectToAction("Index");
        }






        [HttpGet]
        public decimal CalcularDescuento(int descuento)
        {

            List<ViewModelFacturaDetalle> list = null;

            if (Session["CarritoCompras"] != null)
            {

                list = (List<ViewModelFacturaDetalle>)Session["CarritoCompras"];
              
            }
          

            int dif = (from x in list
                       select x.IdProducto).Distinct().Count();


            // encFactura.Total = encFactura.Subtotal;

            decimal total = 0;
            decimal descuentoTotal = 0;

            foreach (var item in list)
            {
                 total += item.Precio*item.Cantidad+item.Impuesto;
            }


            decimal desc = (Decimal)descuento;

            if (dif >= 3 && total >= 10000)
            {


                descuentoTotal = total * (desc/100);

            }


            return descuentoTotal;

        }

        [HttpGet]
        public decimal CalcularPromocion()
        {


            List<ViewModelFacturaDetalle> list = null;
            if (Session["CarritoCompras"] != null)
            {

                list = (List<ViewModelFacturaDetalle>)Session["CarritoCompras"];

            }

            ServiceProducto _serviceProducto = new ServiceProducto();
            Producto prod = _serviceProducto.GetProductoByName("Gelatina").First();
            decimal promo = 0;
            int contador = 0;
            foreach (var item in list)
            {
                if (item.IdProducto == prod.Id)
                    contador++;
                if (item.Cantidad == 2 && item.IdProducto == prod.Id)
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




        [HttpGet]
        public bool VerificarTarjeta(string numtTarjeta)
        {
            ServiceTarjeta _ServiceTarjeta = new ServiceTarjeta();
            return _ServiceTarjeta.ValidarTarjeta(numtTarjeta);

        }

    }
}