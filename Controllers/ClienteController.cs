using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimeraASP.Models;

namespace MiPrimeraASP.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.cliente.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(cliente newCliente)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.cliente.Add(newCliente);
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
                    cliente findCliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findCliente);
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

        public ActionResult Edit(cliente updateCliente)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente objcliente = db.cliente.Find(updateCliente.id);
                    objcliente.nombre = updateCliente.nombre;
                    objcliente.documento = updateCliente.documento;
                    objcliente.email = updateCliente.email;
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

        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente findCliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findCliente);
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
                    cliente findCliente = db.cliente.Find(id);
                    db.cliente.Remove(findCliente);
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

        public ActionResult DetalleClienteCompra()
        {
            var db = new inventarioEntities();
            var query = from cliente in db.cliente
                            join compra in db.compra on cliente.id equals compra.id_cliente
                            select new clienteCompra
                            {
                                clienteNombre = cliente.nombre,
                                compraId = compra.id,
                                emailCliente = cliente.email,
                                fechaCompra = compra.fecha,
                                totalCompra = compra.total
                            };
            
            return View(query);
        }

        public ActionResult uploadCSV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            string filePath = string.Empty;
            if (fileForm != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(fileForm.FileName);
                string extension = Path.GetExtension(fileForm.FileName);
                fileForm.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newCliente = new cliente
                        {
                            nombre = row.Split(';')[0],
                            documento = row.Split(';')[1],
                            email = row.Split(';')[2],
                            
                        };

                        using (var db = new inventarioEntities())
                        {
                            db.cliente.Add(newCliente);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return View();
        }
    }
    
}
