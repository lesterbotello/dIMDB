using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dIMDB.Models;
using System.Web.Mvc;

namespace dIMDB.App_Code
{
    public class LogAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (filterContext.HttpContext.Session["UserName"] != null && (string)filterContext.HttpContext.Session["UserName"] != "")
            {
                using (var db = new dIMDBEntities())
                {
                    var rastro = new Bitacora()
                    {
                        Fecha = DateTime.Now,
                        UserName = (string)filterContext.HttpContext.Session["UserName"],
                        IPAddress = filterContext.HttpContext.Request.UserHostAddress,
                        ActionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "."
                            + filterContext.ActionDescriptor.ActionName
                    };

                    db.Bitacora.Add(rastro);
                    db.SaveChanges();
                }
            }
        }
    }
}