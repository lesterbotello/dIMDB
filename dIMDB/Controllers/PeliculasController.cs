using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dIMDB.Models;
using System.IO;

namespace dIMDB.Controllers
{
    [Authorize]
    public class PeliculasController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();

        // GET: Peliculas
        public ActionResult Index()
        {
            var peliculas = db.Peliculas.Include(p => p.Directores).Include(p => p.Generos);
            return View(peliculas.ToList());
        }

        // GET: Peliculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            return View(peliculas);
        }

        [Authorize(Roles = "Admin")]
        // GET: Peliculas/Create
        public ActionResult Create()
        {
            ViewBag.IDDirector = new SelectList(db.Directores, "IDDirector", "Nombre");
            ViewBag.IDGenero = new SelectList(db.Generos, "IDGenero", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "IDPelicula,Nombre,Sinopsis,IDDirector,Anio,RutaCaratula,RutaTrailer,IDGenero")] Peliculas peliculas)
        {
            if (ModelState.IsValid)
            {
                db.Peliculas.Add(peliculas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDirector = new SelectList(db.Directores, "IDDirector", "Nombre", peliculas.IDDirector);
            ViewBag.IDGenero = new SelectList(db.Generos, "IDGenero", "Nombre", peliculas.IDGenero);
            return View(peliculas);
        }

        // GET: Peliculas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDirector = new SelectList(db.Directores, "IDDirector", "Nombre", peliculas.IDDirector);
            ViewBag.IDGenero = new SelectList(db.Generos, "IDGenero", "Nombre", peliculas.IDGenero);
            return View(peliculas);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IDPelicula,Nombre,Sinopsis,IDDirector,Anio,RutaCaratula,RutaTrailer,IDGenero")] Peliculas peliculas)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/posters/"),
                            string.Format("poster_{0}{1}",
                            peliculas.IDPelicula, 
                            Path.GetExtension(file.FileName)));

                        peliculas.RutaCaratula = string.Format("/Content/Posters/poster_{0}{1}",
                            peliculas.IDPelicula,
                            Path.GetExtension(file.FileName));

                        try
                        {
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);

                            file.SaveAs(path);
                        }
                        catch
                        {

                        }
                    }
                }

                db.Entry(peliculas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDirector = new SelectList(db.Directores, "IDDirector", "Nombre", peliculas.IDDirector);
            ViewBag.IDGenero = new SelectList(db.Generos, "IDGenero", "Nombre", peliculas.IDGenero);
            return View(peliculas);
        }

        // GET: Peliculas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peliculas peliculas = db.Peliculas.Find(id);
            if (peliculas == null)
            {
                return HttpNotFound();
            }
            return View(peliculas);
        }

        [Authorize(Roles = "Admin")]
        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Peliculas peliculas = db.Peliculas.Find(id);
            db.Peliculas.Remove(peliculas);
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

        [ChildActionOnly]
        public ActionResult _ActoresPeliculas(int id)
        {
            //var actores = db.ActoresPeliculas.Where(p => p.IDPelicula == id).ToList();

            var actores = (from a in db.ActoresPeliculas
                           where a.IDPelicula == id
                           select a).ToList();

            return PartialView(actores);
        }

        public ActionResult _AgregarActor(int id)
        {
            var actores = (from a in db.Actores
                           where !(from ap in db.ActoresPeliculas
                                   where ap.IDPelicula == id
                                   select ap.IDActor).Contains(a.IDActor)
                           select a);

            return PartialView(actores);
        }

        [HttpPost]
        public JsonResult AgregarActor(string IDPelicula, string IDActor)
        {
            var actorPelicula = new ActoresPeliculas();
            var iIDPelicula = int.Parse(IDPelicula);
            var iIDActor = int.Parse(IDActor);

            actorPelicula.IDActor = iIDActor;
            actorPelicula.IDPelicula = iIDPelicula;
            db.ActoresPeliculas.Add(actorPelicula);
            db.SaveChanges();

            db.Configuration.ProxyCreationEnabled = false;

            var actor = (from a in db.Actores
                         where a.IDActor == iIDActor
                         select new { a.IDActor, a.Nombre });

            return Json(actor);
        }

        [HttpPost]
        public JsonResult EliminarActor(string IDPelicula, string IDActor)
        {
            var iIDPelicula = int.Parse(IDPelicula);
            var iIDActor = int.Parse(IDActor);

            var actorAEliminar = (from ap in db.ActoresPeliculas
                                  where ap.IDPelicula == iIDPelicula && ap.IDActor == iIDActor
                                  select ap).First();


            db.ActoresPeliculas.Remove(actorAEliminar);
            db.SaveChanges();

            var actorEliminado = (from a in db.Actores where a.IDActor == iIDActor select a);
            return Json(actorEliminado);
        }
    }
}
