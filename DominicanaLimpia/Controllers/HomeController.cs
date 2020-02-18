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

            if (Session["RodId"].ToString() == "5" || Session["RodId"].ToString() == "1" || Session["RodId"].ToString() == "4" || Session["RodId"].ToString() == "2")
            {
                var Desde = Convert.ToDateTime("07/01/2019");
                var Hasta = DateTime.Now.Date;
                int[] ProvinciasSum = new int[23];
                decimal sumatoriatotalcuadro = 0;
                int NumeroporcentajeIndividual = 0;
                bool EsIndividual = false;
                var TotalADividir = 0;
                var requests = db.Objetivo1.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                int MetaIndividual = 0;




                if (Session["RodId"].ToString() == "2")
                {

                    EsIndividual = true;
                    var munn = Session["MunicipiosAsignados"].ToString().Split(',');
                    int[] marks = new int[10];
                    for (int i = 0; i < munn.Length; i++)
                    {
                        if (munn[i] != "0")
                        {
                            marks[i] = Convert.ToInt32(munn[i]);
                        }
                    }
                    FormularioM = FormularioM.Where(r => marks.ToList().Contains(Convert.ToInt32(r.ProvinciaId))).ToList();
                    requests = requests.Where(r => marks.ToList().Contains(Convert.ToInt32(r.MunicipioId))).ToList();


                    List<SelectListItem> General = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="1", Value="7415"},
                    new SelectListItem() {Text="2", Value="8017"},
                    new SelectListItem() {Text="3", Value="5070"},
                    new SelectListItem() {Text="4", Value="16030"},
                    new SelectListItem() {Text="5", Value="1715"},
                    new SelectListItem() {Text="6", Value="4413"},
                    new SelectListItem() {Text="7", Value="9789"},
                    new SelectListItem() {Text="8", Value="1327"},
                    new SelectListItem() {Text="9", Value="3109"},
                    new SelectListItem() {Text="10", Value="6766"},
                    new SelectListItem() {Text="11", Value="11519"},
                    new SelectListItem() {Text="12", Value="3156"},
                    new SelectListItem() {Text="13", Value="5469"},
                    new SelectListItem() {Text="14", Value="5613"},
                    new SelectListItem() {Text="15", Value="06456"},
                    new SelectListItem() {Text="16", Value="11873"},
                    new SelectListItem() {Text="17", Value="3214"},
                    new SelectListItem() {Text="18", Value="24413"},
                    new SelectListItem() {Text="19", Value="264"},
                    new SelectListItem() {Text="20", Value="4336"},
                    new SelectListItem() {Text="21", Value="2029"},
                    new SelectListItem() {Text="22", Value="18689"},
                };

                    //este es la lista de escuelas
                 List<SelectListItem> Escuelas = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="1", Value="10"},
                    new SelectListItem() {Text="2", Value="20"},
                    new SelectListItem() {Text="3", Value="10"},
                    new SelectListItem() {Text="4", Value="16"},
                    new SelectListItem() {Text="5", Value="6"},
                    new SelectListItem() {Text="6", Value="5"},
                    new SelectListItem() {Text="7", Value="13"},
                    new SelectListItem() {Text="8", Value="4"},
                    new SelectListItem() {Text="9", Value="4"},
                    new SelectListItem() {Text="10", Value="4"},
                    new SelectListItem() {Text="11", Value="5"},
                    new SelectListItem() {Text="12", Value="5"},
                    new SelectListItem() {Text="13", Value="9"},
                    new SelectListItem() {Text="14", Value="12"},
                    new SelectListItem() {Text="15", Value="15"},
                    new SelectListItem() {Text="16", Value="5"},
                    new SelectListItem() {Text="17", Value="5"},
                    new SelectListItem() {Text="18", Value="15"},
                    new SelectListItem() {Text="19", Value="3"},
                    new SelectListItem() {Text="20", Value="5"},
                    new SelectListItem() {Text="21", Value="7"},
                    new SelectListItem() {Text="22", Value="22"},
                };

                    var dd = General.Where(x => x.Text == marks[0].ToString()).FirstOrDefault();
                    NumeroporcentajeIndividual = Convert.ToInt32(dd.Value);

                }


                #region
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


                if (EsIndividual == true)
                {
                    TotalADividir = NumeroporcentajeIndividual;
                }
                else
                {
                    TotalADividir = 160680;
                }

                var ResultadoGeneral = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / TotalADividir));
                ResultadoGeneral = ResultadoGeneral * 100;
                Session["ResultadoGeneral"] = ResultadoGeneral;

                #endregion



                #region

                //METAS DE ESCUELAS
                ProvinciasSum = new int[23];
               var requestsEscuela = requests.GroupBy(i => i.EscuelaId).Select(group => group.First()).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requestsEscuela.Where(x => x.MunicipioId == i).Sum(z => z.p1);
                }

                sumatoriatotalcuadro = 0;
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    sumatoriatotalcuadro = sumatoriatotalcuadro + ProvinciasSum[i];
                }

                if (EsIndividual == false)
                {
                    TotalADividir = 200;
                }

                var kelokeEscuela = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / TotalADividir));
                kelokeEscuela = kelokeEscuela * 100;
                Session["SumaEscuelas"] = kelokeEscuela;

                #endregion


                #region

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

                if (EsIndividual == false)
                {
                    TotalADividir = 849;
                }

                var Resultadopuntoslimpios = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / TotalADividir));
                Resultadopuntoslimpios = Resultadopuntoslimpios * 100;
                Session["Resultadopuntoslimpios"] = Resultadopuntoslimpios;
                #endregion


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

                if (EsIndividual == false)
                {
                    TotalADividir = 92896;
                }

                var ResultadosPEstudiantil = Convert.ToDouble(String.Format("{0:0.00}", sumatoriatotalcuadro / TotalADividir));
                ResultadosPEstudiantil = ResultadosPEstudiantil * 100;
                Session["ResultadosPEstudiantil"] = ResultadosPEstudiantil;
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