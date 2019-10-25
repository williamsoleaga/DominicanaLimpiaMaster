using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DominicanaLimpia.Controllers
{
    public class LoginController : Controller
    {

        DominicanalimpiaEntities2 db = new DominicanalimpiaEntities2();

       
        // GET: Login
        public ActionResult Index()
        {

            return View();  
        }


        [HttpGet]
        public JsonResult Entrar(string Usuario, string Clave)
        {
            try
            {
                var resultado = db.Usuarios.Where(x => x.Usuario == Usuario.Trim().ToLower() && x.Clave == Clave.Trim() && x.Estatus.Trim() == "A").ToList();
                
                if (resultado.Count > 0)
                {
                    Session["UsuarioId"] = resultado.Select(x => x.UsuarioId).FirstOrDefault();
                    Session["NombreUsuario"] = resultado.Select(x => x.Nombre_Completo).FirstOrDefault();
                    Session["RodId"] = resultado.Select(x => x.RolId).FirstOrDefault();
                    var Munic = resultado.Select(x => x.MunicipiosId).FirstOrDefault();

                    if (Session["RodId"].ToString() == "2")
                    {
                        Session["Municipios"] = db.Municipios.Where(r => Munic.Contains(r.MunicipioId.ToString())).ToList().Select(x => x.Provincia_Nombre).FirstOrDefault();
                    }

                    if (Session["RodId"].ToString() == "5" || Session["RodId"].ToString() == "4" )
                    {
                            
                         var wepa   = db.Municipios.Where(r => Munic.Contains(r.MunicipioId.ToString())).ToList();

                        foreach (var item in wepa)
                        {
                            Session["Municipios"] = Session["Municipios"] + " " + item.Provincia_Nombre;
                        }
                    }

                    if (Session["RodId"].ToString() == "1")
                    {
                        Session["Municipios"] = "";
                    }


                    if (Convert.ToInt16(Session["RodId"]) == 2)
                    {
                        var IdResponsable = resultado.Select(x => x.ResponsableId).FirstOrDefault();
                        var responsable = db.Usuarios.Where(x => x.UsuarioId == IdResponsable).FirstOrDefault();
                        Session["NombreResponsable"]  = responsable.Nombre_Completo;
                    }

                    return Json(new { EsExitoso = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { EsExitoso = false }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { EsExitoso = false, Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }




    }
}