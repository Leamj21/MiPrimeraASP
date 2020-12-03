using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimeraASP.Models;
using Rotativa;

namespace MiPrimeraASP.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.compra.ToList());
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra newcompra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.compra.Add(newcompra);
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
                    compra findcompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findcompra);
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

        public ActionResult Edit(compra updatecompra)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra objcompra = db.compra.Find(updatecompra.id);
                    objcompra.fecha = updatecompra.fecha;
                    objcompra.total = updatecompra.total;
                    objcompra.id_usuario = updatecompra.id_usuario;
                    objcompra.id_cliente = updatecompra.id_cliente;
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

        public static string NombreUsuario(int? idUser)
        {
            using (var db = new inventarioEntities())
            {
                return db.usuario.Find(idUser).nombre;
            }
        }

        public ActionResult listaUsuarios()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreCliente(int? NombreCliente)
        {
            using (var db = new inventarioEntities())
            {
                return db.cliente.Find(NombreCliente).nombre;
            }
        }

        public ActionResult listaclientes()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra findcompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findcompra);
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
                    compra findcompra = db.compra.Find(id);
                    db.compra.Remove(findcompra);
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

        public ActionResult reporteCompra()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.compra.ToList());
            }
        }
        public ActionResult ImprimirReporte()
        {
            return new ActionAsPdf("reporteCompra") { FileName = "Reporte.pdf" };
        }
    }
}