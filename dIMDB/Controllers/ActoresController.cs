using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dIMDB.Models;
using dIMDB.App_Code;

namespace dIMDB.Controllers
{
    public class ActoresController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();

        // GET: Actores
        public ActionResult Index()
        {
            return View(db.Actores.OrderBy(a => a.Nombre).Take(500).ToList());
            // return View();
        }

        [HttpPost]
        public ActionResult Index(string inpNombre)
        {
            if (inpNombre.Length > 0)
            {
                //var actoresFiltrados = (from a in db.Actores where a.Nombre.Contains(inpNombre)
                //                        orderby a.Nombre
                //                        select a).Take(500);

                var actoresFiltrados = (from a in db.Actores
                                        where a.Nombre.StartsWith(inpNombre)
                                        orderby a.Nombre
                                        select a).Take(500);

                return View(actoresFiltrados.ToList());
            }
            else
                return View(db.Actores.OrderBy(a => a.Nombre).Take(500).ToList());
        }

        // GET: Actores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actores actores = db.Actores.Find(id);
            if (actores == null)
            {
                return HttpNotFound();
            }
            return View(actores);
        }

        // GET: Actores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogAccess]
        public ActionResult Create([Bind(Include = "IDActor,Nombre,LugarNacimiento,FechaNacimiento")] Actores actores)
        {
            if (ModelState.IsValid)
            {
                db.Actores.Add(actores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actores);
        }

        // GET: Actores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actores actores = db.Actores.Find(id);
            if (actores == null)
            {
                return HttpNotFound();
            }
            return View(actores);
        }

        // POST: Actores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogAccess]
        public ActionResult Edit([Bind(Include = "IDActor,Nombre,LugarNacimiento,FechaNacimiento")] Actores actores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actores);
        }

        // GET: Actores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actores actores = db.Actores.Find(id);
            if (actores == null)
            {
                return HttpNotFound();
            }
            return View(actores);
        }

        // POST: Actores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [LogAccess]
        public ActionResult DeleteConfirmed(int id)
        {
            Actores actores = db.Actores.Find(id);
            db.Actores.Remove(actores);
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
