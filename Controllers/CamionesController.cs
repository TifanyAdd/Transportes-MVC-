using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
            using (TransportesEntities context = new TransportesEntities())
            {
                //llenos mi lista directamente usando linQ
                lista_camiones = (from c in context.Camiones select c).ToList();
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
        //GET: NUevo_Camion
        public ActionResult Nuevo_Camion()
        {
            ViewBag.Titulo = "Nuevo Camión";
            //cargar DLL con las opciones del tipo de camión
            cargarDDL();
            return View();
        }
        //POST: Nuevo_Camion
        [HttpPost]
        public ActionResult Nuevo_Camion(Camiones_DTO model, HttpPostedFileBase imagen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransportesEntities contex = new TransportesEntities()) //creo una instancia de un solo uso de mi contexto
                    {
                        //creo un objeto basado en el contexto(Modelo Original)
                        var camion = new Camiones();

                        //asigno todos los valores del modelo de entrada (DTO) el objeto que será insertado en la BD(model original)
                        camion.Matricula = model.Matricula;
                        camion.Tipo_Camion = model.Tipo_Camion;
                        camion.Marca = model.Marca;
                        camion.Modelo = model.Modelo;
                        camion.Capacidad = model.Capacidad;
                        camion.Kilometraje = model.Kilometraje;
                        camion.Disponibilidad = model.Disponibilidad;

                        //valido si existe una IMG o no
                        if (imagen != null && imagen.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(imagen.FileName); //recupero el nombre de mi archivo
                            string pathdir = Server.MapPath("~/Assets/Imagenes/Camiones/"); //mapeo la ruta donde guardare mis imagenes en el servidor
                            if (!Directory.Exists(pathdir)) // si no existe el directorio, lo creo
                            {
                                Directory.CreateDirectory(pathdir);
                            }
                            imagen.SaveAs(pathdir + filename); //guardo la imagen en el servidor
                            camion.UrlFoto = "/Assets/Imagenes/Camiones/" + filename; //guardo la ruta y el nombre del archivo en el camion que voy a insertar

                            //Impacto dobre la BD usando EF
                            contex.Camiones.Add(camion); //agrego un nuevo camion al contexto
                            contex.SaveChanges(); //Impactar la bd (enviar los cambios entre el contexto y la bd)
                            //sweet alert
                            return RedirectToAction("Index"); //finalmente, regreso al listado si es que todo salio bien
                        }
                        else
                        {
                            //Sweet alert
                            cargarDDL();
                            return View(model);
                        }
                    }
                }
                else
                {
                    //En este caso que ocurra alguna exepcion, voy a mostrar un msj con el error, voy a volver a cargar la lista de opciones dek tipo_camion(cargarDDL) para que no marque error el formulario, y devuelve la misma vista con los mismos datos que me han sido enviados
                    //Sweet alert
                    cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //En este caso que ocurra alguna exepcion, voy a mostrar un msj con el error, voy a volver a cargar la lista de opciones dek tipo_camion(cargarDDL) para que no marque error el formulario, y devuelve la misma vista con los mismos datos que me han sido enviados
                //Sweet alert
                cargarDDL();
                return View(model);
            }
        }
        
        //GET: Editar_Camion/{id}

        public ActionResult Editar_Camion(int id)
        {
            if (id > 0) //validar que realmente llega un id
            {
                Camiones_DTO camion = new Camiones_DTO();
                using (TransportesEntities contex = new TransportesEntities()) //creo una instancia de un unico uso de mi contexto
                {
                    //busco aquel camion que tenga el ID
                    //Bajo metodo
                    var camion_aux = contex.Camiones.Where(x => x.ID_Camion == id).FirstOrDefault();
                    camion.ID_Camion = camion_aux.ID_Camion;
                    camion.Matricula = camion_aux.Matricula;
                    camion.Marca = camion_aux.Marca;
                    camion.Modelo = camion_aux.Modelo;
                    camion.Tipo_Camion = camion_aux.Tipo_Camion;
                    camion.Capacidad = camion_aux.Capacidad;
                    camion.Kilometraje = camion_aux.Kilometraje;
                    camion.Disponibilidad = camion_aux.Disponibilidad;
                    camion.UrlFoto = camion_aux.UrlFoto;

                    //Bajo consulta

                        camion = (from c in contex.Camiones
                                  where c.ID_Camion == id
                                  select new Camiones_DTO()
                                  {
                                      ID_Camion = c.ID_Camion,
                                      Matricula = c.Matricula,
                                      Marca = c.Marca,
                                      Modelo = c.Modelo,
                                      Tipo_Camion = c.Tipo_Camion,
                                      Capacidad = c. Capacidad,
                                      Kilometraje = c. Kilometraje,
                                      Disponibilidad = c.Disponibilidad,
                                      UrlFoto = c.UrlFoto
                                  }).FirstOrDefault();
                } //cierre de using (contex)

                if (camion != null)
                {
                    ViewBag.Titulo = "Editar camion" + camion.ID_Camion;
                    cargarDDL();
                    return View(camion);
                }
                else
                {
                    //sweet alert
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //Sweet Alert
                return RedirectToAction("Index");
            }
        }

        //POST: Nuevo_Camion
        [HttpPost]
        public ActionResult Editar_Camion(Camiones_DTO model, HttpPostedFileBase imagen)
        {
            try
            {
                if (ModelState.IsValid) //Verificar si los campos y los tipos de datos corresponden al Modelo (DTO)
                {
                    using (TransportesEntities context = new TransportesEntities()) //creo una isntancia de un único uso de mi Contexto
                    {
                        //creo un objeto basado en el contexto (modelo original)
                        var camion = new Camiones();

                        //asignos los valores del modelos de entrada (DTO) al objeto que se insertará en la BD (Modelo Original)
                        camion.ID_Camion = model.ID_Camion;
                        camion.Matricula = model.Matricula;
                        camion.Marca = model.Marca;
                        camion.Modelo = model.Modelo;
                        camion.Capacidad = model.Capacidad;
                        camion.Tipo_Camion = model.Tipo_Camion;
                        camion.Disponibilidad = model.Disponibilidad;
                        camion.Kilometraje = model.Kilometraje;

                        //valido si exite una IMG
                        if (imagen != null && imagen.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(imagen.FileName);
                            string pathdir = Server.MapPath("~/Assets/Imagenes/Camiones/");
                            //validar si tiene una foto o no
                            if (model.UrlFoto.Length == 0)
                            {
                                //la imagen en la BD es null y hay que darle la imagen
                                if (!Directory.Exists(pathdir))
                                {
                                    Directory.CreateDirectory(pathdir);
                                }

                                imagen.SaveAs(pathdir + filename);
                                camion.UrlFoto = "/Assets/Imagenes/Camiones/" + filename;
                            }
                            else
                            {
                                //validar si es la misma o es nueva
                                if (model.UrlFoto.Contains(filename))
                                {
                                    //El la misma
                                    camion.UrlFoto = "/Assets/Imagenes/Camiones/" + filename;
                                }
                                else
                                {
                                    //es diferente
                                    if (!Directory.Exists(pathdir))
                                    {
                                        Directory.CreateDirectory(pathdir);
                                    }

                                    //Borro la imagen anterios
                                    //valido si existe

                                    try
                                    {
                                        string pathdir_old = Server.MapPath("~" + model.UrlFoto); //busco la imagen que catualmente tiene el camión
                                        if (System.IO.File.Exists(pathdir_old)) //valido si existe dicho archivo
                                        {
                                            //procedo a eliminarlo
                                            System.IO.File.Delete(pathdir_old);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //Sweet Alert
                                    }

                                    imagen.SaveAs(pathdir + filename);
                                    camion.UrlFoto = "/Assets/Imagenes/Camiones/" + filename;
                                }
                            }

                        }
                        else //si no hya nueva imagen, paso la misma
                        {
                            camion.UrlFoto = model.UrlFoto;
                        }

                        //Guardar cambios, validar excepciones, redirigir
                        //actualizar el estado de nuestro elemento
                        //.Entry() registrar la entrada de nueva información al contexto y notificar un cambio de estado usando System.Data.Entity.EntityState.Modified
                        context.Entry(camion).State = System.Data.Entity.EntityState.Modified;
                        //impactamos la BD
                        try
                        {
                            context.SaveChanges();
                        }
                        //agregar using desde 'using System.Data.Entity.Validation;'
                        catch (DbEntityValidationException ex)
                        {
                            string resp = "";
                            //recorro todos los posibles errores de la Entidad Referencial
                            foreach (var error in ex.EntityValidationErrors)
                            {
                                //recorro los detalles de cada error
                                foreach (var validationError in error.ValidationErrors)
                                {
                                    resp += "Error en la Entidad: " + error.Entry.Entity.GetType().Name;
                                    resp += validationError.PropertyName;
                                    resp += validationError.ErrorMessage;
                                }
                            }
                            //Sweet Alert
                        }
                        //Sweet Alert
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //En caso de que ocurra alguna exepción, voy a mostrar un msj con el error, voy a volver a cargar la lista de opciones del tipo_camion (cargarDDL) par que no marque error el formulario, y devulveré la misma vista con los mismos datos que me han sido enviado (vas pa' tras)
                    //Sweet Alert
                    cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //En caso de que ocurra alguna exepción, voy a mostrar un msj con el error, voy a volver a cargar la lista de opciones del tipo_camion (cargarDDL) par que no marque error el formulario, y devulveré la misma vista con los mismos datos que me han sido enviado (vas pa' tras)
                //Sweet Alert
                cargarDDL();
                return View(model);
            }
        }


        #region Auxiliares
        private class Opciones
        {
            public string Numero { get; set; }
            public string Descripcion { get; set; }
        }
        public void cargarDDL()
        {
            List<Opciones> lista_opciones = new List<Opciones>()
            {
                new Opciones(){Numero = "0", Descripcion="Seleccione un opción"},
                new Opciones(){Numero = "1", Descripcion="Volteo"},
                new Opciones(){Numero = "2", Descripcion="Rodillas"},
                new Opciones(){Numero = "3", Descripcion="Transporte"}
            };
            ViewBag.ListaTipos = lista_opciones;
        }
        #endregion
    }
}