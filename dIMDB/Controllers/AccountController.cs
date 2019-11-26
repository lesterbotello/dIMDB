using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dIMDB.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace dIMDB.Controllers
{
    public class AccountController : Controller
    {
        dIMDBEntities db = new dIMDBEntities();

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("~/Home/");

            ViewBag.ReturnUrl = returnUrl;

            return View();
            //if (Session["UserName"] != null && ((string)Session["UserName"] != string.Empty))
            //    return Redirect("~/Home/");
            //else
            //    return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.Find(model.Email, model.Password);

            if (user != null)
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { }, userIdentity);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nombre y/o contraseña inválido");
                return View(model);
            }

            //var usr = db.Usuarios.
            //    Where(u => u.UserName == userName && u.Password == pwd).
            //    FirstOrDefault();

            //if (usr == null)
            //{
            //    ViewBag.Err = "Usuario o contraseña inválidos.";
            //    return View();
            //}
            //else
            //{
            //    Session["UserName"] = usr.UserName;
            //    return Redirect("~/Home/");
            //}
        }

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = model.Email };
            var usr = manager.Create(user, model.Password);

            if (usr != null)
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { }, userIdentity);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Err = "Ocurrio un error al crear el usuario";
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}