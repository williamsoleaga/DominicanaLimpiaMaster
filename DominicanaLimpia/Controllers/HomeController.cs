using DominicanaLimpia.Models;
using DominicanaLimpia.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominicanaLimpia.Controllers
{
    [SessionExpire]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session["UsuarioId"] = null;
            Session["NombreUsuario"] = null;
            Session["RodId"] = null;
            return RedirectToAction("Index", "Login");
        }


        public ActionResult ReporteIndex()
        {
            ViewBag.TipoReporte = new SelectList(LlenarReporteFormulario(), "Indice", "Value");
            return View("~/Views/Reportes/Reportes.cshtml");
        }


        

        public List<Combo> LlenarReporteFormulario()
        {
            List<Combo> Combo = new List<Combo>();

            Combo.Add(new Combo { Indice = 1, Value = "METAS POBLACION EN GENERAL" });
            Combo.Add(new Combo { Indice = 2, Value = "METAS DE HOGARES"});
            Combo.Add(new Combo { Indice = 3, Value = "METAS DE COMERCIOS"});
            Combo.Add(new Combo { Indice = 4, Value = "METAS DE ESCUELAS"});
            Combo.Add(new Combo { Indice = 5, Value = "METAS DE ESTUDIANTES" });
            Combo.Add(new Combo { Indice = 6, Value = "METAS DOCENTES Y PERSONAL ADMINISTRATIVO"});
            Combo.Add(new Combo { Indice = 7, Value = "METAS POBLACIÓN ESCUELAS" });
            Combo.Add(new Combo { Indice = 8, Value = "METAS PUNTOS LIMPIOS" });

            return Combo;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}