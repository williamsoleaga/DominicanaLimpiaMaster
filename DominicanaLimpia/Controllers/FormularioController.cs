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
using RotativaHQ.MVC5;

namespace DominicanaLimpia.Controllers
{
    [SessionExpire]
    public class FormularioController : Controller
    {
        private DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();

        // GET: Formulario
        public ActionResult Index()
        {

            string responsables = "";
            var distinctClientsPerEvent = db.FormularioM.GroupBy(m => m.Desde)
                                                              .Select(x => x.FirstOrDefault());

            distinctClientsPerEvent = db.FormularioM.Where(x => x.FormularioId != 0);

            //si es Responsable de territorio
            if (Session["RodId"].ToString() == "5")
            {
                int idusuario = Convert.ToInt32(Session["UsuarioId"].ToString());
                var MisCoordinadores = db.Usuarios.Where(x => x.ResponsableId == idusuario).ToList();
                foreach (var item in MisCoordinadores)
                {
                    responsables = responsables + "," + item.UsuarioId;
                }

                distinctClientsPerEvent = db.FormularioM.Where(r => responsables.Contains(r.UsuarioId.ToString()));
            }

            //si es coordinador
            if (Session["RodId"].ToString() == "2")
            {
                int idusuario = Convert.ToInt32(Session["UsuarioId"].ToString());

                distinctClientsPerEvent = db.FormularioM.Where(x => x.UsuarioId == idusuario);
            }


            // var   Modelo = distinctClientsPerEvent.Where(x=>x)
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

            var escuelas = db.Escuelas.Where(x => x.EscuelaId == 202).FirstOrDefault();
            resultadoEsc.Add(escuelas);


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
              
                var splitDesde = formulario.DesdeFecha.Split('/');
                var splitHasta = formulario.HastaFecha.Split('/');

                formulario.DesdeFecha = splitDesde[1]+"/"+splitDesde[0] +"/"+splitDesde[2];
                formulario.HastaFecha = splitHasta[1] + "/" + splitHasta[0] + "/" + splitHasta[2];

                FormularioM nuevoform = new FormularioM();
                nuevoform.Desde = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", formulario.DesdeFecha));
                nuevoform.Hasta = Convert.ToDateTime(String.Format("{0:MM/dd/yyyy}", formulario.HastaFecha));
                nuevoform.UsuarioId = Convert.ToInt16(Session["UsuarioId"].ToString());
                nuevoform.ProvinciaId = formulario.ProvinciaId;
                nuevoform.Comentario = formulario.Comentario;
                nuevoform.Estatus = "I";
                nuevoform.NumeroFormulario = 1;
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
         
                var numeroformulario = db.FormularioM.ToList().LastOrDefault();

                Objetivo1 Objetivos = new Objetivo1();

                    foreach (var item in formulario.ObjetivoLista)
                    {
                        Objetivos.FormularioId = numeroformulario.FormularioId;
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

                        Objetivos.P111 = item.P111;
                        Objetivos.P112 = item.P112;

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
                    descripciones.FormularioId = numeroformulario.FormularioId;
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
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Formulario formulario = db.Formulario.Find(id);
        //    db.Formulario.Remove(formulario);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            FormularioM formulario = db.FormularioM.Find(id);
            db.FormularioM.Remove(formulario);
            db.SaveChanges();

            //eliminando los objetivos
            var objetivo = db.Objetivo1.Where(x => x.FormularioId == id).ToList();
            foreach (var item in objetivo)
            {
                db.Objetivo1.Remove(item);
                db.SaveChanges();
            }
            DescripcionP Descipcion = db.DescripcionP.Find(id);
            db.DescripcionP.Remove(Descipcion);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditarFormularioM(int? id)
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
            return View("~/Views/Formulario/EditarFM.cshtml", formularioM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFormularioM(FormularioM formularioM)
        {

            if (ModelState.IsValid)
            {
                db.Entry(formularioM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        public ActionResult ValidarFormulario(int? id)
        {

            FormularioM UsuariosMaster = db.FormularioM.Find(id);
            UsuariosMaster.Estatus = "A";
            db.Entry(UsuariosMaster).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult VerFormulario(int? id)
        {

            FormularioModelView Fmv = new FormularioModelView();
            Fmv.Preguntas = db.Preguntas.ToList();
            Fmv.objetivo1 = db.Objetivo1.Where(x => x.FormularioId == id).ToList();
            Fmv.formularioM = db.FormularioM.Where(x => x.FormularioId == id).ToList();
            Fmv.Valores = new int[33];
            Fmv.Comentarios = new string[33];
            ViewBag.FechaDesde = Fmv.formularioM[0].Desde.ToString();
            ViewBag.FechaHasta = Fmv.formularioM[0].Hasta.ToString();

            Fmv.formularioM[0].Comentario = (Fmv.formularioM[0].Comentario == null) ? "" : Fmv.formularioM[0].Comentario;
            foreach (var item in Fmv.formularioM)
            {
                Fmv.Valores[13] = (int)item.P13;
                Fmv.Valores[14] = (int)item.P14;
                Fmv.Valores[15] = (int)item.P15;
                Fmv.Valores[16] = (int)item.P16;
                Fmv.Valores[17] = (int)item.P17;
                Fmv.Valores[18] = (int)item.P18;
                Fmv.Valores[19] = (int)item.P19;
                Fmv.Valores[20] = (int)item.P20;
                Fmv.Valores[21] = (int)item.P21;
                Fmv.Valores[22] = (int)item.P22;
                Fmv.Valores[23] = (int)item.P23;
                Fmv.Valores[24] = (int)item.P24;
                Fmv.Valores[25] = (int)item.P25;
                Fmv.Valores[26] = (int)item.P26;
                Fmv.Valores[27] = (int)item.P27;
                Fmv.Valores[28] = (int)item.P28;
                Fmv.Valores[29] = (int)item.P29;
                Fmv.Valores[30] = (int)item.P30;
                Fmv.Valores[31] = (int)item.P31;
                Fmv.Valores[32] = (int)item.P32;

            }
            foreach (var item in db.DescripcionP.Where(x=>x.FormularioId == id).ToList())
            {
                Fmv.Comentarios[18] = item.Dp18;
                Fmv.Comentarios[19] = item.Dp19;
                Fmv.Comentarios[20] = item.Dp20;
                Fmv.Comentarios[27] = item.Dp27;
                Fmv.Comentarios[28] = item.Dp28;
                Fmv.Comentarios[29] = item.Dp29;
                Fmv.Comentarios[30] = item.Dp30;
                Fmv.Comentarios[31] = item.Dp31;
                Fmv.Comentarios[32] = item.Dp32;

            }


            int idusuario = (int)Fmv.formularioM[0].UsuarioId;
            var responsable1 = db.Usuarios.Where(x => x.UsuarioId == idusuario).FirstOrDefault();
            var responsable = db.Usuarios.Where(x => x.UsuarioId == responsable1.ResponsableId).FirstOrDefault();
            Session["NombreResponsableReporte"] = responsable.Nombre_Completo;

            int idprovincia = (int)Fmv.formularioM[0].ProvinciaId;
            Session["NombreProvincia"] = db.Municipios.Where(x => x.MunicipioId == idprovincia).Select(x => x.Provincia_Nombre).FirstOrDefault();



            return new ViewAsPdf("VistaFormulario", Fmv);

           


        }

    }
}
