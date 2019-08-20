﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DominicanaLimpia;
using DominicanaLimpia.Models;

namespace DominicanaLimpia.Controllers
{
    [SessionExpire]
    public class FormularioController : Controller
    {
        private DominicanalimpiaEntities1 db = new DominicanalimpiaEntities1();

        // GET: Formulario
        public ActionResult Index()
        {

            //var qry = from m in db.Formulario
            //          group m by new { m.Idusuario, m.Desde } into grp
            //          where grp.Count() > 1
            //          select grp.Key;

            var distinctClientsPerEvent = db.Formulario.GroupBy(m => m.Desde)
                                               .Select(x => x.FirstOrDefault());

            return View(distinctClientsPerEvent);
        }

        // GET: Formulario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // GET: Formulario/Create
        public ActionResult Create()
        {
            Formulario formulario = new Formulario();
            formulario.Preguntas = db.Preguntas.ToList();
            return View(formulario);
        }

        // POST: Formulario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Formulario formulario)
        {

            if (ModelState.IsValid)
            {
                int contador = 1;

                for (int i = 0; i < formulario.Valores.Count(); i++)
                {

                    Formulario nuevof = new Formulario();
                    nuevof.PreguntaId = contador;
                    nuevof.Hasta = Convert.ToDateTime(formulario.HastaFecha);
                    nuevof.Desde = Convert.ToDateTime(formulario.DesdeFecha);
                    nuevof.Idusuario = Convert.ToInt16(Session["UsuarioId"].ToString());
                    nuevof.Estatus = "A";
                    nuevof.Valor = formulario.Valores[i];


                    db.Formulario.Add(nuevof);
                    db.SaveChanges();
                    contador = contador + 1;
                }

                return RedirectToAction("Index");
            }

            return View(formulario);
        }



        // GET: Formulario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // POST: Formulario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormularioId,PreguntaId,Valor,Desde,Hasta,Comentario,Idusuario,Estatus")] Formulario formulario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formulario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formulario);
        }

        // GET: Formulario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // POST: Formulario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Formulario formulario = db.Formulario.Find(id);
            db.Formulario.Remove(formulario);
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
