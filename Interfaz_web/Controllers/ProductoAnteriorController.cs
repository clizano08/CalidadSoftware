/*using App_VentaSnacks.Models;
using App_VentaSnacks.Security;
using App_VentaSnacks.Services;
using App_VentaSnacks.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace App_VentaSnacks.Controllers
{
    public class ProductoAnteriorController : Controller
    {
        // GET: Producto
        [CustomAuthenticationFilter]
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
       
        public ActionResult List()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
                return View(lista);
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


        // GetProductoByName
        public ActionResult GetProductoByName(string name)
        {
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                var lista = _ServiceProducto.GetProductoByName(name).ToList();

                // Configurar Serialización
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    },
                };


                string json = JsonConvert.SerializeObject(lista, settings);

                return Content(json);
            }
            catch (Exception err)
            {
                ViewBag.Message = err.Message;
                return View();
            }
        }


        // GET: Producto/Details/5
        public ActionResult Edit(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
  
            Producto oProducto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }
     
                oProducto = _ServiceProducto.GetProductoByID(id.Value);

                return View(oProducto);
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
        [ValidateAntiForgeryToken]
        public ActionResult Save(Producto producto, HttpPostedFileBase ImageFile)
        {
            string errores = "";
            try
            {
                

                // Es valido
                if (ModelState.IsValid)
                {
                    IServiceProducto _ServiceProducto = new ServiceProducto();
                    _ServiceProducto.Save(producto);
                }
                else
                {
                   

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();
                    return View("Create", producto);
                }

                // redirigir
                return RedirectToAction("List");
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

        // GET: Bodega/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                IServiceProducto _ServiceProducto = new ServiceProducto();

                Producto oProducto = _ServiceProducto.GetProductoByID(id.Value);

                return View("Delete", oProducto);
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

        public ActionResult DeleteProducto(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceProducto.DeleteProducto(id.Value);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData.Keep();


                    // return View("list");
                }

                // redirigir
                return RedirectToAction("List");
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

        public ActionResult Details(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();

            Producto oProducto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                oProducto = _ServiceProducto.GetProductoByID(id.Value);


                //return View(oProducto);

                // redirigir
                return RedirectToAction("List");
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

    }
}*/