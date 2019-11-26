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
    public class DirectoresController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();

        // GET: Directores
        public ActionResult Index()
        {
            return View(db.Directores.ToList());
        }

        // GET: Directores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directores directores = db.Directores.Find(id);
            if (directores == null)
            {
                return HttpNotFound();
            }
            return View(directores);
        }

        // GET: Directores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDirector,Nombre,LugarNacimiento,FechaNacimiento")] Directores directores)
        {
            if (ModelState.IsValid)
            {
                db.Directores.Add(directores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(directores);
        }

        // GET: Directores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directores directores = db.Directores.Find(id);
            if (directores == null)
            {
                return HttpNotFound();
            }
            return View(directores);
        }

        // POST: Directores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDirector,Nombre,LugarNacimiento,FechaNacimiento")] Directores directores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(directores);
        }

        // GET: Directores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directores directores = db.Directores.Find(id);
            if (directores == null)
            {
                return HttpNotFound();
            }
            return View(directores);
        }

        // POST: Directores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Directores directores = db.Directores.Find(id);
            db.Directores.Remove(directores);
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
