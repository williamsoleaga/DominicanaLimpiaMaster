using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DominicanaLimpia;
using DominicanaLimpia.Models;
using DominicanaLimpia.ModelViews;
using DominicanaLimpia.Utilidades;

namespace DominicanaLimpia.Controllers
{
    [SessionExpire]
    public class UsuariosController : Controller
    {

        

        private DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.Where(x=>x.Estatus.Contains("A")).ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {

            ViewBag.Accounts = new SelectList(db.Roles, "RolId", "Nombre_Rol");
            ViewBag.Municipios = new SelectList(db.Municipios, "MunicipioId", "Provincia_Nombre");
            ViewBag.Responsables = new SelectList(db.Usuarios.Where(x =>x.RolId == 5), "UsuarioId", "Nombre_Completo");

            return View();
        }






        [HttpPost]
        public ActionResult ObtenerMunicipiosporresponsables(string ResponsableId)
        {
            List<string> _a = null;
            int resp = Convert.ToInt32(ResponsableId);

            if (resp != 0)
            {
                var MunicipiosResponsables = db.Usuarios.Where(x => x.UsuarioId == resp).ToList();
                var muni = MunicipiosResponsables.FirstOrDefault().MunicipiosId;
                var test = db.Municipios.Where(r => muni.Contains(r.MunicipioId.ToString())).ToList();
                _a = test.Select(a => "<option  value='" + a.MunicipioId + "'>" + a.Provincia_Nombre + "</option>'").ToList();
            }
            else
            {
                var test = db.Municipios.ToList();
                _a = test.Select(a => "<option  value='" + a.MunicipioId + "'>" + a.Provincia_Nombre + "</option>'").ToList();
            }

            return Content(String.Join("", _a));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuarios usuarios)
        {

  
            if (ModelState.IsValid)
            {
                if(usuarios.RolId == 2 || usuarios.RolId == 5)
                {
                    for (int i = 0; i < usuarios.Municipios.Count(); i++)
                    {
                        usuarios.MunicipiosId = usuarios.MunicipiosId + usuarios.Municipios[i].ToString() + ", 0";
                    }
                }

                //si no es corrdinador quitale a los responsables
                if(usuarios.RolId != 2  )
                {
                    usuarios.ResponsableId = 0;
                }

                char[] charsToTrim = {' '};
                usuarios.Estatus = "A";
                usuarios.Usuario = usuarios.Usuario.ToLower();

                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }

            if (usuarios.RolId == 2 || usuarios.RolId == 5)
            {
                int[] CountryIDs = usuarios.MunicipiosId.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                ViewBag.Municipios = new MultiSelectList(db.Municipios.ToList(), "MunicipioId", "Provincia_Nombre", CountryIDs);
            }
            else
            {
                ViewBag.Municipios = new SelectList(db.Municipios, "MunicipioId", "Provincia_Nombre");
            }

            if (usuarios.RolId == 2)
            {
                ViewBag.Responsables = new SelectList(db.Usuarios.Where(x=>x.RolId == 5).ToList(), "UsuarioId", "Nombre_Completo", usuarios.ResponsableId);
            }

            ViewBag.Accounts = new SelectList(db.Roles, "RolId", "Nombre_Rol",usuarios.RolId);
            usuarios.Clave = usuarios.Clave.Trim();
            return View(usuarios);
        }



        public ActionResult EditarMiPerfin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }

            if (usuarios.RolId == 2 || usuarios.RolId == 5)
            {
                int[] CountryIDs = usuarios.MunicipiosId.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                ViewBag.Municipios = new MultiSelectList(db.Municipios.ToList(), "MunicipioId", "Provincia_Nombre", CountryIDs);
            }
            else
            {
                ViewBag.Municipios = new SelectList(db.Municipios, "MunicipioId", "Provincia_Nombre");
            }

            if (usuarios.RolId == 2)
            {
                ViewBag.Responsables = new SelectList(db.Usuarios.Where(x => x.RolId == 5).ToList(), "UsuarioId", "Nombre_Completo", usuarios.ResponsableId);
            }

            ViewBag.Accounts = new SelectList(db.Roles, "RolId", "Nombre_Rol", usuarios.RolId);
            usuarios.Clave = usuarios.Clave.Trim();
            return View("~/Views/Usuarios/MiPerfil.cshtml", usuarios);
           
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMiPerfin(Usuarios usuarios)
        {

            Usuarios  UsuariosMaster = db.Usuarios.Find(usuarios.UsuarioId);
            UsuariosMaster.Clave = usuarios.Clave.Trim();
            db.Entry(UsuariosMaster).State = EntityState.Modified;
            db.SaveChanges();
            return View("~/Views/Home/Index.cshtml");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if (usuarios.RolId == 2 || usuarios.RolId == 5)
                {
                    for (int i = 0; i < usuarios.Municipios.Count(); i++)
                    {
                        usuarios.MunicipiosId = usuarios.MunicipiosId + usuarios.Municipios[i].ToString() + ", 0";
                    }
                }
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        [ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            usuarios.Estatus = "I";

            //db.Usuarios.Remove(usuarios);
            db.Entry(usuarios).State = EntityState.Modified;
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
