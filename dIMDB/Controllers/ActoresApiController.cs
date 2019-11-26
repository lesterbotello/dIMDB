using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dIMDB.Models;

namespace dIMDB.Controllers
{
    public class ActoresApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                var db = new dIMDBEntities();
                db.Configuration.ProxyCreationEnabled = false;

                var actores = db.Actores.Select(a => new
                {
                    IDActor = a.IDActor,
                    Nombre = a.Nombre,
                    LugarNacimiento = a.LugarNacimiento,
                    FechaNacimiento = a.FechaNacimiento
                });

                return Ok(actores.ToList());
            }
            catch
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var db = new dIMDBEntities())
                {
                    db.Configuration.ProxyCreationEnabled = false;

                    var actores = from a in db.Actores
                                  where a.IDActor == id
                                  select new
                                  {
                                      IDActor = a.IDActor,
                                      Nombre = a.Nombre,
                                      LugarNacimiento = a.LugarNacimiento,
                                      FechaNacimiento = a.FechaNacimiento
                                  };

                    return Ok(actores.ToList());
                }
            }
            catch
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Post(Actores actor)
        {
            try
            {
                using (var db = new dIMDBEntities())
                {
                    db.Actores.Add(actor);
                    db.SaveChanges();
                }
            }
            catch
            {
                return InternalServerError();
            }

            return Ok(actor.IDActor);
        }

        public IHttpActionResult Put(Actores actor)
        {
            Actores a;
            try
            {
                using (var db = new dIMDBEntities())
                {
                    a = db.Actores.Find(actor.IDActor);

                    if (a != null)
                    {
                        a.Nombre = actor.Nombre;
                        a.LugarNacimiento = actor.LugarNacimiento;
                        a.FechaNacimiento = actor.FechaNacimiento;
                    }

                    db.SaveChanges();
                }
            }
            catch
            {
                return InternalServerError();
            }

            return Ok(a);
        }

        public IHttpActionResult Delete(int id)
        {
            Actores a;
            try
            {
                using (var db = new dIMDBEntities())
                {
                    a = db.Actores.Find(id);

                    db.Actores.Remove(a);
                    db.SaveChanges();
                }
            }
            catch
            {
                return InternalServerError();
            }

            return Ok(a);
        }
    }
}
