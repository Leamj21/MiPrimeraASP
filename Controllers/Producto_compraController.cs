using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimeraASP.Models;

namespace MiPrimeraASP.Controllers
{
    [Authorize]
    public class Producto_compraController : Controller
    {
        // GET: Producto_compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto_compra.ToList());
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_compra newPyC)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto_compra.Add(newPyC);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findPyC = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findPyC);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(producto_compra updatePyC)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra objPyC = db.producto_compra.Find(updatePyC.id);
                    objPyC.id_compra = updatePyC.id_compra;
                    objPyC.id_producto = updatePyC.id_producto;
                    objPyC.cantidad = updatePyC.cantidad;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;

            }
        }

        public static int NombreCompra(int? idcompra)
        {
            using (var db = new inventarioEntities())
            {
                return db.compra.Find(idcompra).id;
            }
        }

        public ActionResult listaCompra()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.compra.ToList());
            }
        }

        public static string NombreProducto(int? idproducto)
        {
            using (var db = new inventarioEntities())
            {
                return db.producto.Find(idproducto).nombre;
            }
        }

        public ActionResult listaProducto()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findPyC = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findPyC);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findPyC = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findPyC);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                throw;
            }

        }
    }
}