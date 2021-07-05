using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Datos.Models;
using Interfaz_web.Security;

namespace Interfaz_web.Controllers
{

    [CustomAuthenticationFilter]
    public class EncFacturaController : Controller
    {
        private MyContext db = new MyContext();

        // GET: EncFactura
        public ActionResult Index()
        {
            var encFactura = db.EncFactura.Include(e => e.Usuario);
            return View(encFactura.ToList());
        }

        // GET: EncFactura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncFactura encFactura = db.EncFactura.Find(id);
            if (encFactura == null)
            {
                return HttpNotFound();
            }
            return View(encFactura);
        }

        // GET: EncFactura/Create
        public ActionResult Create()
        {
            ViewBag.Usuario_id = new SelectList(db.Usuario, "Id", "Nombre");
            return View();
        }

        // POST: EncFactura/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Usuario_id,NombreCliente,Total,Fecha,Estado,TipoPago,Subtotal,Descuento")] EncFactura encFactura)
        {
            if (ModelState.IsValid)
            {
                db.EncFactura.Add(encFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Usuario_id = new SelectList(db.Usuario, "Id", "Nombre", encFactura.Usuario_id);
            return View(encFactura);
        }

        // GET: EncFactura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncFactura encFactura = db.EncFactura.Find(id);
            if (encFactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuario_id = new SelectList(db.Usuario, "Id", "Nombre", encFactura.Usuario_id);
            return View(encFactura);
        }

        // POST: EncFactura/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Usuario_id,NombreCliente,Total,Fecha,Estado,TipoPago,Subtotal,Descuento")] EncFactura encFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Usuario_id = new SelectList(db.Usuario, "Id", "Nombre", encFactura.Usuario_id);
            return View(encFactura);
        }

        // GET: EncFactura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncFactura encFactura = db.EncFactura.Find(id);
            if (encFactura == null)
            {
                return HttpNotFound();
            }
            return View(encFactura);
        }

        // POST: EncFactura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            EncFactura encFactura = db.EncFactura.Find(id);
            db.EncFactura.Remove(encFactura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
