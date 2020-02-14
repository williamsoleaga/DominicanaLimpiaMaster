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


        private DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();


        public ActionResult Index()
        {

            if (Session["RodId"].ToString() == "5" || Session["RodId"].ToString() == "1" || Session["RodId"].ToString() == "4")
            {
                var Desde = Convert.ToDateTime("30 Jun, 2019");
                var Hasta = DateTime.Now.Date;
                int[] ProvinciasSum = new int[23];


                var requests = db.Objetivo1.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();


                //METAS DE ESCUELAS
                requests = requests.GroupBy(i => i.EscuelaId).Select(group => group.First()).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p1);
                }

                decimal sumatoriatotalcuadro = 0;
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    sumatoriatotalcuadro = sumatoriatotalcuadro + ProvinciasSum[i];
                }

                var kelokeEscuela = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / 200));
                kelokeEscuela = kelokeEscuela * 100;
                Session["SumaEscuelas"] = kelokeEscuela;


                //FormularioGeneral
                ProvinciasSum = new int[23];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                    ProvinciasSum[i] = Convert.ToInt32(ProvinciasSum[i] + ((int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p7) * 3.3));
                    //  ProvinciasSum[i] =  (int)(ProvinciasSum[i] * 3.3);

                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p8);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p9);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p10);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);

                    //con el maestro de formulario ahora
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P18);
                    // ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P19);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P22);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P23);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P29);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P32);

                }


                sumatoriatotalcuadro = 0;
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    sumatoriatotalcuadro = sumatoriatotalcuadro + ProvinciasSum[i];
                }
                var ResultadoGeneral = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / 160680));
                ResultadoGeneral = ResultadoGeneral * 100;
                Session["ResultadoGeneral"] = ResultadoGeneral;


                //Puntos limpios
                ProvinciasSum = new int[23];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P13);
                }

                sumatoriatotalcuadro = 0;
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    sumatoriatotalcuadro = sumatoriatotalcuadro + ProvinciasSum[i];
                }

                var Resultadopuntoslimpios = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / 600));
                Resultadopuntoslimpios = Resultadopuntoslimpios * 100;
                Session["Resultadopuntoslimpios"] = Resultadopuntoslimpios;


                //poblacion estudiantil


                ProvinciasSum = new int[23];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }

                sumatoriatotalcuadro = 0;
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    sumatoriatotalcuadro = sumatoriatotalcuadro + ProvinciasSum[i];
                }

                var ResultadosPEstudiantil = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / 92896));
                ResultadosPEstudiantil = ResultadosPEstudiantil * 100;
                Session["Resultadopuntoslimpios"] = ResultadosPEstudiantil;
            }



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


        public ActionResult ReportePorActividad()
        {
            ViewBag.TipoReporte = new SelectList(LlenarReporteFormularioActividad(), "Indice", "Value");
            return View("~/Views/Reportes/ReportePorActividad.cshtml");
        }

        public List<Combo> LlenarReporteFormularioActividad()
        {
            List<Combo> Combo = new List<Combo>();
            Combo.Add(new Combo { Indice = 1, Value = "1- Reporte por actividad" });
            return Combo;
        }


            public List<Combo> LlenarReporteFormulario()
        {
            List<Combo> Combo = new List<Combo>();

            Combo.Add(new Combo { Indice = 4, Value = "1- METAS DE ESCUELAS" });
            Combo.Add(new Combo { Indice = 9, Value = "2- META DOCENTES" });
            Combo.Add(new Combo { Indice = 6, Value = "3- METAS PERSONAL ADMINISTRATIVO" });
            Combo.Add(new Combo { Indice = 10, Value = "4- META ESTUDIANTES INICIAL" });
            Combo.Add(new Combo { Indice = 11, Value = "5- META ESTUDIANTES PRIMARIA" });
            Combo.Add(new Combo { Indice = 12, Value = "6- META ESTUDIANTES SECUNDARIA" });
            Combo.Add(new Combo { Indice = 5, Value = "7- METAS DE ESTUDIANTES" });
            Combo.Add(new Combo { Indice = 7, Value = "8- METAS POBLACIÓN ESCUELAS" });
            Combo.Add(new Combo { Indice = 2, Value = "9- METAS DE HOGARES" });
            Combo.Add(new Combo { Indice = 13, Value = "10- PARTICIPANTES JUNTAS DE VECINOS" });
            Combo.Add(new Combo { Indice = 14, Value = "11- PARTICIPANTES DE COMUNIDAD EN GENERAL" });
            Combo.Add(new Combo { Indice = 15, Value = "12- PARTICIPANTES DIADESOL" });
            Combo.Add(new Combo { Indice = 17, Value = "13.1- DIADESOL NEUMATICOS" });
            Combo.Add(new Combo { Indice = 16, Value = "13.2- DIADESOL PLASTICOS" });
            Combo.Add(new Combo { Indice = 18, Value = "14- PARTICIPANTES TALLERES DE RECICLAR" });

            Combo.Add(new Combo { Indice = 36, Value = "15- PUNTOS LIMPIOS ESCOLARES " });

            Combo.Add(new Combo { Indice = 19, Value = "16- PUNTOS LIMPIOS COMUNITARIOS" });
            Combo.Add(new Combo { Indice = 3, Value = "17- PUNTOS LIMPIOS DE COMERCIOS" });
            Combo.Add(new Combo { Indice = 8, Value = "18- CREACION DE RUTAS Y FRECUENCIAS" });   //aqui va el 18 ojo revisar klk con este
            Combo.Add(new Combo { Indice = 21, Value = "19- ENTREGA DE MONTOCARGAS" });
            Combo.Add(new Combo { Indice = 20, Value = "20- RECICLADORES CAPACITADOS" });
            Combo.Add(new Combo { Indice = 22, Value = "21- RECICLADORES IDENTIFICADOS" });
            Combo.Add(new Combo { Indice = 23, Value = "22- EMPRESAS RECICLADORAS" });
            Combo.Add(new Combo { Indice = 27, Value = "23- CAMIONES DE RECOLECCION SEPARADAS" });
            Combo.Add(new Combo { Indice = 24, Value = "24- CAPACITADOS Y SENSIBILIZADOS- BRIGADAS DE RECOLECCIÓN SEPARADA" });
            Combo.Add(new Combo { Indice = 25, Value = "25- CAPACITADOS ICAM" });
            Combo.Add(new Combo { Indice = 26, Value = "26- VERTEDEROS ACTUALES" });
            Combo.Add(new Combo { Indice = 28, Value = "27- VERTEDEROS ELIMINADOS" });
            Combo.Add(new Combo { Indice = 29, Value = "28- VERTEDEROS NUEVOS" });
            Combo.Add(new Combo { Indice = 31, Value = "29- CENTRO DE ACOPIOS CONSTRUIDO" });
            Combo.Add(new Combo { Indice = 30, Value = "30- CENTRO DE ACOPIOS FUNCIONANDO" });
            Combo.Add(new Combo { Indice = 32, Value = "31- CAPACITADOS CENTROS DE ACOPIOS" });
            Combo.Add(new Combo { Indice = 33, Value = "32- ABONERAS FUNCIONANDO" });
            //no se sabe con que es que se hace ese calculo
            Combo.Add(new Combo { Indice = 34, Value = "33- ABONERAS CONSTRUIDAS" });
            Combo.Add(new Combo { Indice = 35, Value = "34- CAPACITADOS ABONERAS" });
            Combo.Add(new Combo { Indice = 1, Value = "35- METAS POBLACION EN GENERAL" });

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