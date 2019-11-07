using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DominicanaLimpia;

namespace DominicanaLimpia.Controllers
{
    public class FormularioMController : Controller
    {
        private DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();

        // GET: FormularioM
        public ActionResult Index()
        {
            return View(db.FormularioM.ToList());
        }

        // GET: FormularioM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioM formularioM = db.FormularioM.Find(id);
            if (formularioM == null)
            {
                return HttpNotFound();
            }
            return View(formularioM);
        }

        // GET: FormularioM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormularioM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormularioId,Desde,Hasta,UsuarioId,ProvinciaId,NumeroFormulario,Estatus,Comentario,P13,P14,P15,P16,P17,P18,P19,P20,P21,P22,P23,P24,P25,P26,P27,P28,P29,P30,P31,P32")] FormularioM formularioM)
        {
            if (ModelState.IsValid)
            {
                db.FormularioM.Add(formularioM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formularioM);
        }

        // GET: FormularioM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioM formularioM = db.FormularioM.Find(id);
            if (formularioM == null)
            {
                return HttpNotFound();
            }
            return View(formularioM);
        }

        // POST: FormularioM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormularioId,Desde,Hasta,UsuarioId,ProvinciaId,NumeroFormulario,Estatus,Comentario,P13,P14,P15,P16,P17,P18,P19,P20,P21,P22,P23,P24,P25,P26,P27,P28,P29,P30,P31,P32")] FormularioM formularioM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formularioM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formularioM);
        }

        // GET: FormularioM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioM formularioM = db.FormularioM.Find(id);
            if (formularioM == null)
            {
                return HttpNotFound();
            }
            return View(formularioM);
        }

        // POST: FormularioM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormularioM formularioM = db.FormularioM.Find(id);
            db.FormularioM.Remove(formularioM);
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
