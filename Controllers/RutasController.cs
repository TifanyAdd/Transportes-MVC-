using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Transportes_MVC.Models;
using System.Data.Entity;

namespace Transportes_MVC.Controllers
{
    public class RutasController : Controller
    {
        //CREO UNA INSTANCIA DE MI CONTEXTO<<<<<<<<<<<<<<<<<<
        private TransportesEntities context = new TransportesEntities();
        // GET: Rutas
        public ActionResult Index()
        {
            return View();
        }

        //GET: view_LinQ
        public ActionResult View_LinQ()
        {
            //Creo una lista de Objetos DTO
            List<View_Rutas_DTO> lista_view = new List<View_Rutas_DTO>();

            //lleno mis lista a partir de los datos del contexto usando LinQ
            lista_view = (from r in context.Rutas
                          join carg in context.Cargamentos on r.ID_Ruta equals carg.Ruta_ID
                          join cam in context.Camiones on r.Camion_ID equals cam.ID_Camion
                          join ch in context.Choferes on r.Chofer_ID equals ch.ID_Chofer
                          join DirO in context.Direcciones on r.Direccionorigen_ID equals DirO.ID_Direccion
                          join DirD in context.Direcciones on r.Direcciondestino_ID equals DirD.ID_Direccion
                          //selecciono los datos que necesito para modelar la clase DTO
                          select new View_Rutas_DTO()
                          {
                              C_ = r.ID_Ruta,
                              ID_Cargamento = carg.ID_Cargamento,
                              Cargamento = carg.Descripcion,
                              ID_Direccion_Origen = DirO.ID_Direccion,
                              Origen = "Calle: " + DirO.Calle + " # " + DirO.Numero + "Col. " + DirO.Colonia + " C.P." +
                              DirO.CP,
                              Estado_Origen = DirO.Estado,
                              ID_Direccion_Destino = DirO.ID_Direccion,
                              Destino = "Calle: " + DirD.Calle + " # " + DirD.Numero + "Col. " + DirD.Colonia + " C.P." +
                              DirD.CP,
                              Estado_Destino = DirD.Estado,
                              ID_Chofer = ch.ID_Chofer,
                              Chofer = ch.Nombre + "" + ch.Apellido_Paterno + " " + ch.Apellido_Materno,
                              ID_Camion = cam.ID_Camion,
                              Camion = "Marca: " + cam.Marca + "Modelo: " + cam.Modelo + " Matricula: " + cam.Matricula,
                              Salida = r.Fecha_salida,
                              Llegadaestimada = r.Fecha_llegadaestimada,
                          }).ToList();
            ViewBag.Titulo = "Vista con LinQ";
            return View(lista_view);

        }
        
        //GET: EF_Nav
        public ActionResult EF_Nav()
        {
            var rutas = context.Rutas.Include(r => r.Camiones)
                .Include(r => r.Choferes)
                .Include(r => r.Direcciones)
                .Include(r => r.Cargamentos);

            ViewBag.Titulo = "Rutas con EF";
            return View(rutas.ToList());
        }
        // GET: Rutas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rutas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rutas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rutas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rutas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rutas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rutas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
