using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transportes_MVC.Models;

namespace Transportes_MVC.Controllers
{
    public class CamionesController : Controller
    {
        // GET: Camiones
        public ActionResult Index()
        {
            //creo ena lista de camiones del modelo Original
            List<Camiones> lista_camiones = new List<Camiones>();
            //lleno la lista con elementos existentes dentro del contexto(DB) utilizando EF y LinQ
            using(TransportesEntities context = new TransportesEntities())
            {
                //llenos mi lista directamente usando linQ
                lista_camiones =(from c in context.Camiones select c).ToList();
                //// otra forma de hacer lo mismo 
                //lista_camiones = context.Camiones.ToList();
                ////otra forma de hacer lo mismo
                //foreach (Camiones cam in context.Camiones)
                //{
                //    lista_camiones.Add(cam);
                //}
            }
            //ViewBag(forma parte de razor)se caracteriza por hacer uso de una propiedad arbitraria que sirve para pasar informacion desde el controlador a la vista
            ViewBag.Titulo = "Lista de Camiones";
            ViewBag.Subtitulo = "Utilizando ASP.NET MVC";

            //ViewData se caracteriza por hacer uso de un atributo y tiene el mismo funcionamiento que el ViewBag
            ViewData["Titulo2"] = "Segundo Título";

            //TempData se caracteriza por permitir crear variables temporales que existen durante la ejecucion del Runtime de ASP
            //ademas los tempdata me permite compartir información, no solo del controlador a la vista, si no tambien entre otras vistas y otros controladores
            //TempData.Add("Clave", "Valor");

            //retorno la vista con los datos del modelo
            return View(lista_camiones);
        }
    }
}