using System;
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
using DominicanaLimpia.ModelViews;
using DominicanaLimpia.Utilidades;

namespace DominicanaLimpia.Controllers
{
    [SessionExpire]
    public class FormularioController : Controller
    {
        private DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();

        // GET: Formulario
        public ActionResult Index()
        {

            var  distinctClientsPerEvent = db.FormularioM.GroupBy(m => m.Desde)
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
      

            int idusuario = Convert.ToInt32(Session["UsuarioId"].ToString());
            var usuario = db.Usuarios.Where(x => x.UsuarioId ==  idusuario).ToList().FirstOrDefault();
            var myInClause =  usuario.MunicipiosId.Split(',');
            var results = db.Municipios.Where(r => myInClause.Contains(r.MunicipioId.ToString()));
            ViewBag.Municipios = new SelectList(results, "MunicipioId", "Provincia_Nombre");


           var _Escuelas = usuario.MunicipiosId.Split(',');

            var resultadoEsc = db.Escuelas.Where(r => _Escuelas.Contains(r.MunicipioId.ToString())).ToList();

            ViewBag.EscuelaGrid = new SelectList(resultadoEsc, "EscuelaId", "Descripcion");
            
            
            ViewBag.Pregunta16 = new SelectList(LlenarComboSiNo(), "Indice", "Value");
            ViewBag.Escuelas13 = new SelectList(db.Escuelas.Where(r=> _Escuelas.Contains(r.MunicipioId.ToString())).ToList(), "EscuelaId", "Descripcion");

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

                try
                {
                    var idnumero = 0;
                    var numeroformulario = db.FormularioM.ToList().LastOrDefault();

                    if (numeroformulario == null)
                    {
                        idnumero = 1;
                    }
                    else
                    {
                        idnumero = Convert.ToInt32(numeroformulario.NumeroFormulario + 1);
                    }
                    
                    FormularioM nuevoform = new FormularioM();
                    nuevoform.Desde = Convert.ToDateTime(formulario.DesdeFecha);
                    nuevoform.Hasta = Convert.ToDateTime(formulario.HastaFecha);
                    nuevoform.UsuarioId = Convert.ToInt16(Session["UsuarioId"].ToString());
                    nuevoform.ProvinciaId = formulario.ProvinciaId;
                    nuevoform.Comentario = formulario.Comentario;
                    nuevoform.Estatus = "A";
                    nuevoform.NumeroFormulario = idnumero;
                    nuevoform.P13 = formulario.Valores[12];
                    nuevoform.P14 = formulario.Valores[13];
                    nuevoform.P15 = formulario.Valores[14];
                    nuevoform.P16 = formulario.Valores[15];
                    nuevoform.P17 = formulario.Valores[16];
                    nuevoform.P18 = formulario.Valores[17];
                    nuevoform.P19 = formulario.Valores[18];
                    nuevoform.P20 = formulario.Valores[19];
                    nuevoform.P21 = formulario.Valores[20];
                    nuevoform.P22 = formulario.Valores[21];
                    nuevoform.P23 = formulario.Valores[22];
                    nuevoform.P24 = formulario.Valores[23];
                    nuevoform.P25 = formulario.Valores[24];
                    nuevoform.P26 = formulario.Valores[25];
                    nuevoform.P27 = formulario.Valores[26];
                    nuevoform.P28 = formulario.Valores[27];
                    nuevoform.P29 = formulario.Valores[28];
                    nuevoform.P30 = formulario.Valores[29];
                    nuevoform.P31 = formulario.Valores[30];
                    nuevoform.P32 = formulario.Valores[31];

                    db.FormularioM.Add(nuevoform);
                    db.SaveChanges();

                    return View("~/Views/Formulario/Exito.cshtml");

                }catch(Exception ex)
                {
                    throw ex.InnerException;
                }
            }

            return View(formulario);
        }


        public List<Combo> LlenarComboSiNo()
        {
            List<Combo> Combo = new List<Combo>();

            Combo.Add(new Combo { Indice = 1, Value = "Si" });
            Combo.Add(new Combo { Indice = 2, Value = "No" });
            return Combo;
        }


        public JsonResult GuardarFormulario(Formulario formulario)
        {
            var error = false;

            try
            {

              

                    var idnumero = 0;
                    var numeroformulario = db.FormularioM.ToList().LastOrDefault();

                    if (numeroformulario == null)
                    {
                        idnumero = 1;
                    }
                    else
                    {
                        idnumero = Convert.ToInt32(numeroformulario.NumeroFormulario + 1);
                    }



                FormularioM nuevoform = new FormularioM();
                nuevoform.Desde = Convert.ToDateTime(formulario.DesdeFecha);
                nuevoform.Hasta = Convert.ToDateTime(formulario.HastaFecha);
                nuevoform.UsuarioId = Convert.ToInt16(Session["UsuarioId"].ToString());
                nuevoform.ProvinciaId = formulario.ProvinciaId;
                nuevoform.Comentario = formulario.Comentario;
                nuevoform.Estatus = "A";
                nuevoform.NumeroFormulario = idnumero;
                nuevoform.P13 = formulario.Valores[12];
                nuevoform.P14 = formulario.Valores[13];
                nuevoform.P15 = formulario.Valores[14];
                nuevoform.P16 = formulario.Pregunta16;
                nuevoform.P17 = formulario.Valores[15];
                nuevoform.P18 = formulario.Valores[16];
                nuevoform.P19 = formulario.Valores[17];
                nuevoform.P20 = formulario.Valores[18];
                nuevoform.P21 = formulario.Valores[19];
                nuevoform.P22 = formulario.Valores[20];
                nuevoform.P23 = formulario.Valores[21];
                nuevoform.P24 = formulario.Valores[22];
                nuevoform.P25 = formulario.Valores[23];
                nuevoform.P26 = formulario.Valores[24];
                nuevoform.P27 = formulario.Valores[25];
                nuevoform.P28 = formulario.Valores[26];
                nuevoform.P29 = formulario.Valores[27];
                nuevoform.P30 = formulario.Valores[28];
                nuevoform.P31 = formulario.Valores[29];
                nuevoform.P32 = formulario.Valores[30];
                db.FormularioM.Add(nuevoform);
                db.SaveChanges();

                Objetivo1 Objetivos = new Objetivo1();

                    foreach (var item in formulario.ObjetivoLista)
                    {
                        Objetivos.FormularioId = idnumero;
                        Objetivos.EscuelaId = item.IdEscuela;
                        Objetivos.UsuarioId = Convert.ToInt16(Session["UsuarioId"].ToString());
                        Objetivos.p1 = item.P1;
                        Objetivos.p2 = item.P2;
                        Objetivos.p3 = item.P3;
                        Objetivos.p4 = item.P4;
                        Objetivos.p5 = item.P5;
                        Objetivos.p6 = item.P6;
                        Objetivos.p7 = item.P7;
                        Objetivos.p8 = item.P8;
                        Objetivos.p9 = item.P9;
                        Objetivos.p10 = item.P10;
                        Objetivos.p11 = item.P11;
                        Objetivos.p12 = item.P12;
                    Objetivos.MunicipioId = formulario.ProvinciaId;
                    Objetivos.Desde = Convert.ToDateTime(formulario.DesdeFecha);
                    Objetivos.Hasta = Convert.ToDateTime(formulario.HastaFecha);


                    db.Objetivo1.Add(Objetivos);
                        db.SaveChanges();
                    }

                   

                    //Agregamos los comentarios de las preguntas
                    DescripcionP descripciones = new DescripcionP();
                    descripciones.UsuarioId = Convert.ToInt16(Session["UsuarioId"].ToString());
                    descripciones.FormularioId = idnumero;
                    descripciones.Dp18 = formulario.DescripcionP[0].TrimEnd();
                    descripciones.Dp19 = formulario.DescripcionP[1].TrimEnd();
                    descripciones.Dp20 = formulario.DescripcionP[2].TrimEnd();
                    descripciones.Dp27 = formulario.DescripcionP[3].TrimEnd();
                    descripciones.Dp28 = formulario.DescripcionP[4].TrimEnd();
                    descripciones.Dp29 = formulario.DescripcionP[5].TrimEnd();
                    descripciones.Dp30 = formulario.DescripcionP[6].TrimEnd();
                    descripciones.Dp31 = formulario.DescripcionP[7].TrimEnd();
                    descripciones.Dp32 = formulario.DescripcionP[8].TrimEnd();
                    db.DescripcionP.Add(descripciones);
                    db.SaveChanges();

                    error = false;
               
            }
            catch (Exception ex)
            {
                error = true;
            }

            return Json(new { error });
        }


        public ActionResult Exito()
        {
             return View("~/Views/Formulario/Exito.cshtml");
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
