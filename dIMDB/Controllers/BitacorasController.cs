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
    public class BitacorasController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();

        // GET: Bitacoras
        public ActionResult Index()
        {
            return View(db.Bitacora.ToList());
        }

        // GET: Bitacoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bitacora bitacora = db.Bitacora.Find(id);
            if (bitacora == null)
            {
                return HttpNotFound();
            }
            return View(bitacora);
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
