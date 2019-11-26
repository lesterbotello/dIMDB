using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dIMDB.Models;

namespace dIMDB.Areas.Blog.Controllers
{
    public class HomeController : Controller
    {
        private dIMDBEntities db = new dIMDBEntities();
        // GET: Blog/Home
        public ActionResult Index()
        {
            return View(db.Articulos.OrderByDescending(a => a.FechaPublicacion).ToList());
        }
    }
}