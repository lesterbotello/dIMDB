using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dIMDB.App_Code
{
    public class Seguridad : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.PostAcquireRequestState += Application_PostAcquireRequestState;
            /*
            context.PostAcquireRequestState += (sender, e) => {

            };
            */
        }

        private void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var context = app.Context;

            if (!context.Request.RawUrl.Contains("/Login"))
            {
                if (context.Session == null || context.Session["UserName"] == null || (string)context.Session["UserName"] == "")
                {
                    context.Response.Redirect("/Account/Login/");
                }
            }
        }
    }
}