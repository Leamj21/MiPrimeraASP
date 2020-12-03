using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimeraASP.Models;

namespace MiPrimeraASP.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto.ToList());
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto newProduct)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto.Add(newProduct);
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
                    producto findProduct = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProduct);
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

        public ActionResult Edit(producto updateProd)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto objProd = db.producto.Find(updateProd.id);
                    objProd.nombre = updateProd.nombre;
                    objProd.percio_unitario = updateProd.percio_unitario;
                    objProd.descripcion = updateProd.descripcion;
                    objProd.cantidad = updateProd.cantidad;
                    objProd.id_proveedor = updateProd.id_proveedor;
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

        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        public ActionResult listaProveedores()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto findProduct = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProduct);
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
                    producto findProduct = db.producto.Find(id);
                    db.producto.Remove(findProduct);
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