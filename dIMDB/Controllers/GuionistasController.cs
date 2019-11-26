using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dIMDB.Models;

namespace dIMDB.Controllers
{
    public class GuionistasController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();

        // GET: Guionistas
        public ActionResult Index()
        {
            return View(db.Gionistas.ToList());
        }

        // GET: Guionistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gionistas gionistas = db.Gionistas.Find(id);
            if (gionistas == null)
            {
                return HttpNotFound();
            }
            return View(gionistas);
        }

        // GET: Guionistas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guionistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGuionista,Nombre,LugarNacimiento,FechaNacimiento")] Gionistas gionistas)
        {
            if (ModelState.IsValid)
            {
                db.Gionistas.Add(gionistas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gionistas);
        }

        // GET: Guionistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gionistas gionistas = db.Gionistas.Find(id);
            if (gionistas == null)
            {
                return HttpNotFound();
            }
            return View(gionistas);
        }

        // POST: Guionistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGuionista,Nombre,LugarNacimiento,FechaNacimiento")] Gionistas gionistas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gionistas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gionistas);
        }

        // GET: Guionistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gionistas gionistas = db.Gionistas.Find(id);
            if (gionistas == null)
            {
                return HttpNotFound();
            }
            return View(gionistas);
        }

        // POST: Guionistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gionistas gionistas = db.Gionistas.Find(id);
            db.Gionistas.Remove(gionistas);
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
