using System.Net;
using System.Web.Mvc;
using dIMDB.Models;
using System.Linq;

namespace dIMDB.Controllers
{
    public class ActoresPeliculasController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();


        // GET: ActoresPeliculas/Create
        public ActionResult Create(string peliculaID)
        {
            int iPeliculaID = int.Parse(peliculaID);
            ViewBag.IDPelicula = iPeliculaID;
            ViewBag.NombrePelicula = db.Peliculas.Find(iPeliculaID).Nombre;

            var newActor = new ActoresPeliculas();
            newActor.IDPelicula = iPeliculaID;

            var actores = (from a in db.Actores
                           where !(from ap in db.ActoresPeliculas
                                   where ap.IDPelicula == iPeliculaID
                                   select ap.IDActor).Contains(a.IDActor)
                           select a);

            ViewBag.IDActor = new SelectList(actores, "IDActor", "Nombre");

            return View(newActor);
        }

        // POST: ActoresPeliculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDActor,IDPelicula")] ActoresPeliculas actoresPeliculas)
        {
            if (ModelState.IsValid)
            {
                db.ActoresPeliculas.Add(actoresPeliculas);
                db.SaveChanges();
                return RedirectToAction("Edit", "Peliculas", new { id = actoresPeliculas.IDPelicula });
            }

            ViewBag.IDActor = new SelectList(db.Actores, "IDActor", "Nombre", actoresPeliculas.IDActor);
            ViewBag.IDPelicula = new SelectList(db.Peliculas, "IDPelicula", "Nombre", actoresPeliculas.IDPelicula);
            return View(actoresPeliculas);
        }


        // GET: ActoresPeliculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActoresPeliculas actoresPeliculas = db.ActoresPeliculas.Find(id);
            if (actoresPeliculas == null)
            {
                return HttpNotFound();
            }
            return View(actoresPeliculas);
        }

        // POST: ActoresPeliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActoresPeliculas actoresPeliculas = db.ActoresPeliculas.Find(id);
            var idPelicula = actoresPeliculas.IDPelicula;

            db.ActoresPeliculas.Remove(actoresPeliculas);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Edit", "Peliculas", new { id = idPelicula });
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
