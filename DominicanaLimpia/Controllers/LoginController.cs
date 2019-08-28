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