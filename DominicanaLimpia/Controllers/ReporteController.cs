using DominicanaLimpia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominicanaLimpia.Controllers
{

    [SessionExpire]
    public class ReporteController : Controller
    {

        private DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();

        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult BuscarReporte(int TipoReporte, string Desdef, string Hastaf)
        {



            var splitDesde = Desdef.Split('/');
            var splitHasta = Hastaf.Split('/');

          DateTime Desde = Convert.ToDateTime(splitDesde[1] + "/" + splitDesde[0] + "/" + splitDesde[2]);
          DateTime Hasta = Convert.ToDateTime(splitHasta[1] + "/" + splitHasta[0] + "/" + splitHasta[2]);



          int[] ProvinciasSum = new int[23];
          var  requests =  db.Objetivo1.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
            

            //Metas de hogares sumatoria de la pretunta 8
            if (TipoReporte == 1)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p7);
                    ProvinciasSum[i] =  (int)(ProvinciasSum[i] * 3.3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p8);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p9);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p10);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);

                    //con el maestro de formulario ahora
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P18);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P19);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P22);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P13);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P29);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P32);

                }
            }


            //Metas de hogares sumatoria de la pretunta 8
            if (TipoReporte == 2)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p8);
                }
            }

            //metas de comercios
            if (TipoReporte == 3)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P16);
                }
            }

            //METAS DE ESCUELAS
            if (TipoReporte == 4)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p1);
                }
            }

            //METAS DE ESCUELAS
            if (TipoReporte == 5)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }
            }

            //METAS DE ESCUELAS
            if (TipoReporte == 6)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                }
            }


            //METAS DE ESCUELAS
            if (TipoReporte == 7)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }
            }


            //metas de comercios
            if (TipoReporte == 8)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P13);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P14);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P15);
                }
            }

            //Metas docentes sumatoria de la pretunta p1
            if (TipoReporte == 9)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                }
            }


            if (TipoReporte == 10)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                }
            }

            if (TipoReporte == 11)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                }
            }



            if (TipoReporte == 12)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }
            }

            if (TipoReporte == 13)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p10);
                }
            }

            if (TipoReporte == 14)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p11);
                }
            }

            if (TipoReporte == 15)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);
                }
            }

            if (TipoReporte == 16)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = 0;//comentar hasta que resuelva el tema de los plasticos  (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);
                }
            }


            if (TipoReporte == 17)
            {
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = 0;//comentar hasta que resuelva el tema de los plasticos  (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);
                }
            }


            if (TipoReporte == 18)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P14);
                }
            }

            if (TipoReporte == 19)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P16);
                }
            }


            if (TipoReporte == 20)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P20);
                }
            }

            if (TipoReporte == 21)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P19);
                }
            }


            if (TipoReporte == 22)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P21);
                }
            }

            if (TipoReporte == 23)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P22);
                }
            }

            if (TipoReporte == 24)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P24);
                }
            }

            if (TipoReporte == 25)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P25);
                }
            }

            if (TipoReporte == 26)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P26);
                }
            }


            if (TipoReporte == 27)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P23);
                }
            }

            if (TipoReporte == 28)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P27);
                }
            }

            if (TipoReporte == 29)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P28);
                }
            }


            if (TipoReporte == 30)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P30);
                }
            }


            if (TipoReporte == 31)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P29);
                }
            }


            if (TipoReporte == 32)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P31);
                }
            }


            if (TipoReporte == 33)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P32);
                }
            }


            //if (TipoReporte == 34) Estas dos no existen 
            //{
            //    var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
            //    for (int i = 0; i < ProvinciasSum.Length; i++)
            //    {
            //        ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.p33);
            //    }
            //}

            //if (TipoReporte == 35)
            //{
            //    var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
            //    for (int i = 0; i < ProvinciasSum.Length; i++)
            //    {
            //        ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.p33);
            //    }
            //}


            return Json(ProvinciasSum);
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {

            var fileName = Path.GetFileName("1poblacion.xlsx");
            var destinationPath = Path.Combine(Server.MapPath("~/Pantillas"), "1poblacion.xlsx");
            return File(destinationPath, "application/vnd.ms-excel", "1poblacion.xlsx");
        }


        public  string RutaReporte(int Tipo)
        {
            var Ruta = "";

            switch (Tipo)
            {
                case 1:
                    Ruta = Server.MapPath("~/Pantillas/1.PLANTILLA METAS PARA APLICACION-población.xlsx");
                    break;
                case 2:
                    Ruta =  Server.MapPath("~/Pantillas/2.PLANTILLA METAS PARA APLICACION-hogares.xlsx");
                    break;
                case 3:
                    Ruta = Server.MapPath("~/Pantillas/3.PLANTILLA METAS PARA APLICACION-comercios.xlsx");
                    break;

                case 4:
                    Ruta = Server.MapPath("~/Pantillas/4.PLANTILLA METAS PARA APLICACION-escuelas.xlsx");
                    break;

                case 5:
                    Ruta = Server.MapPath("~/Pantillas/5.PLANTILLA METAS PARA APLICACION-estudiantes.xlsx");
                    break;

                case 6:
                    Ruta = Server.MapPath("~/Pantillas/6.PLANTILLA METAS PARA APLICACION-docentes y personal admon.xlsx");
                    break;

                case 7:
                    Ruta = Server.MapPath("~/Pantillas/7.PLANTILLA METAS PARA APLICACION-población escuelas.xlsx");
                    break;

                case 8:
                    Ruta = Server.MapPath("~/Pantillas/8.PLANTILLA METAS PARA APLICACION-puntos limpios.xlsx");
                    break;

                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                    Ruta = Server.MapPath("~/Pantillas/"+Convert.ToString(Tipo)+".xlsx");
                    break;

                default:
                    
                    break;
            }

            return Ruta;
        }


        public ActionResult ImprimirReporte(int TipoReporte, string Desdef, string Hastaf)
        {


            var splitDesde = Desdef.Split('/');
            var splitHasta = Hastaf.Split('/');

            DateTime Desde = Convert.ToDateTime(splitDesde[1] + "/" + splitDesde[0] + "/" + splitDesde[2]);
            DateTime Hasta = Convert.ToDateTime(splitHasta[1] + "/" + splitHasta[0] + "/" + splitHasta[2]);





            int[] ProvinciasSum = new int[23];
            string NombreReprte = "";
            var requests = db.Objetivo1.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();


            var excelFile = RutaReporte(TipoReporte);
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Add(excelFile);
            Microsoft.Office.Interop.Excel.Worksheet Hoja = null;


            if (TipoReporte == 1)
            {
                NombreReprte = "MetasPoblacion";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["1.POBLA"];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p7);
                    ProvinciasSum[i] = (int)(ProvinciasSum[i] * 3.3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p8);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p9);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p10);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);

                    //con el maestro de formulario ahora
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P18);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P19);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P22);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P13);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P29);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P32);

                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];

            }

            //Metas de hogares sumatoria de la pretunta 8
            if (TipoReporte == 2)
            {
                NombreReprte = "MetasHogares";
                Hoja =  (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["2.HOGARES "];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p8);
                }


                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            //metas de comercios
            if (TipoReporte == 3)
            {
                NombreReprte = "MetasComercios";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["3.COMERCIOS"];
                

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P16);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];

            }


            //METAS DE ESCUELAS
            if (TipoReporte == 4)
            {
                NombreReprte = "MetasEscuelas";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["4.ESCUELAS "];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p1);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }


            //METAS DE ESTUDIANTES
            if (TipoReporte == 5)
            {
                
                NombreReprte = "MetasEstudiantes";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["5.ESTUDIANTES"];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];

            }


            //METAS DE ESCUELAS
            if (TipoReporte == 6)
            {
                NombreReprte = "MetasDocentesYPersonal";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["6.DOCENTES Y PERSONAL ADMON"];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];

            }


            if (TipoReporte == 7)
            {
                NombreReprte = "MetasPoblacionEscuelas";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["7.POBLACION ESCUELAS "];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p3);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];

            }


            if (TipoReporte == 8)
            {
                NombreReprte = "MetasPuntosLimpios";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["8.PUNTOS LIMPIOS"];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P13);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P14);
                    ProvinciasSum[i] = ProvinciasSum[i] + (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P15);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }


            //Metas docentes sumatoria de la pretunta p1
            if (TipoReporte == 9)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["9"];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p2);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 10)
            {
                NombreReprte = "10";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["10"];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p4);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 11)
            {

                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["11"];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p5);
                }

                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }



            if (TipoReporte == 12)
            {

                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["12"];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p6);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 13)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["13"];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p10);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 14)
            {

                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["14"];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p11);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 15)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 16)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = 0;//comentar hasta que resuelva el tema de los plasticos  (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 17)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = 0;//comentar hasta que resuelva el tema de los plasticos  (int)requests.Where(x => x.MunicipioId == i).Sum(z => z.p12);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 18)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P14);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 19)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P16);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 20)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P20);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 21)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P19);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 22)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P21);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 23)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P22);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 24)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P24);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 25)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P25);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 26)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P26);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 27)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P23);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 28)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P27);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }

            if (TipoReporte == 29)
            {

                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P28);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 30)
            {
                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P30);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 31)
            {

                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P29);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 32)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P31);
                }
                Hoja.Range["F7"].Value2 = ProvinciasSum[1];
                Hoja.Range["F8"].Value2 = ProvinciasSum[11];
                Hoja.Range["F9"].Value2 = ProvinciasSum[19];
                Hoja.Range["F10"].Value2 = ProvinciasSum[20];
                Hoja.Range["F11"].Value2 = ProvinciasSum[2];
                Hoja.Range["F12"].Value2 = ProvinciasSum[12];
                Hoja.Range["F13"].Value2 = ProvinciasSum[13];
                Hoja.Range["F14"].Value2 = ProvinciasSum[14];
                Hoja.Range["F15"].Value2 = ProvinciasSum[3];
                Hoja.Range["F16"].Value2 = ProvinciasSum[21];
                Hoja.Range["F17"].Value2 = ProvinciasSum[15];
                Hoja.Range["F18"].Value2 = ProvinciasSum[16];
                Hoja.Range["F19"].Value2 = ProvinciasSum[4];
                Hoja.Range["F20"].Value2 = ProvinciasSum[5];
                Hoja.Range["F21"].Value2 = ProvinciasSum[6];
                Hoja.Range["F22"].Value2 = ProvinciasSum[22];
                Hoja.Range["F23"].Value2 = ProvinciasSum[17];
                Hoja.Range["F24"].Value2 = ProvinciasSum[7];
                Hoja.Range["F25"].Value2 = ProvinciasSum[18];
                Hoja.Range["F26"].Value2 = ProvinciasSum[10];
                Hoja.Range["F27"].Value2 = ProvinciasSum[8];
                Hoja.Range["F28"].Value2 = ProvinciasSum[9];
            }


            if (TipoReporte == 33)
            {
                NombreReprte = "9";
                Hoja = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets[Convert.ToString(TipoReporte)];

                var FormularioM = db.FormularioM.Where(x => x.Desde >= Desde && x.Desde <= Hasta).ToList();
                for (int i = 0; i < ProvinciasSum.Length; i++)
                {
                    ProvinciasSum[i] = (int)FormularioM.Where(x => x.ProvinciaId == i).Sum(z => z.P32);
                }
                Hoja.Range["E7"].Value2 = ProvinciasSum[1];
                Hoja.Range["E8"].Value2 = ProvinciasSum[11];
                Hoja.Range["E9"].Value2 = ProvinciasSum[19];
                Hoja.Range["E10"].Value2 = ProvinciasSum[20];
                Hoja.Range["E11"].Value2 = ProvinciasSum[2];
                Hoja.Range["E12"].Value2 = ProvinciasSum[12];
                Hoja.Range["E13"].Value2 = ProvinciasSum[13];
                Hoja.Range["E14"].Value2 = ProvinciasSum[14];
                Hoja.Range["E15"].Value2 = ProvinciasSum[3];
                Hoja.Range["E16"].Value2 = ProvinciasSum[21];
                Hoja.Range["E17"].Value2 = ProvinciasSum[15];
                Hoja.Range["E18"].Value2 = ProvinciasSum[16];
                Hoja.Range["E19"].Value2 = ProvinciasSum[4];
                Hoja.Range["E20"].Value2 = ProvinciasSum[5];
                Hoja.Range["E21"].Value2 = ProvinciasSum[6];
                Hoja.Range["E22"].Value2 = ProvinciasSum[22];
                Hoja.Range["E23"].Value2 = ProvinciasSum[17];
                Hoja.Range["E24"].Value2 = ProvinciasSum[7];
                Hoja.Range["E25"].Value2 = ProvinciasSum[18];
                Hoja.Range["E26"].Value2 = ProvinciasSum[10];
                Hoja.Range["E27"].Value2 = ProvinciasSum[8];
                Hoja.Range["E28"].Value2 = ProvinciasSum[9];
            }



            var nuevoguid = System.Guid.NewGuid();
            var rutaarchivo = NombreReprte + nuevoguid + ".xlsx";
            var excelFileSave = Server.MapPath("~/Pantillas/Repositorio");
            var tempFile = System.IO.Path.Combine(excelFileSave, rutaarchivo);
            xlWorkbook.SaveAs(tempFile);
            xlWorkbook.Close(true);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkbook);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);

            var destinationPath = Path.Combine(Server.MapPath("~/Pantillas/Repositorio"), rutaarchivo);
            return File(destinationPath, "application/vnd.ms-excel", rutaarchivo); 
        
        }



       
    }
}