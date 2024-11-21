using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using Transportes_MVC.Models;

namespace Transportes_MVC.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CamionesService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CamionesService.svc o CamionesService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CamionesService : ICamionesService
    {
        private readonly TransportesEntities _context;

        public CamionesService()
        {
            _context = new TransportesEntities();
        }
        public string create_Camion(string Matricula, string Tipo_Camion, string Marca, string Modelo, int Capacidad, double Kilometraje, string UrlFoto, bool Disponibilidad)
        {
            //preparo una respuesta
            string respuesta = "";
            try
            {
                Camiones _camion_instanciado = new Camiones()
                {
                    Matricula = Matricula,
                    Tipo_Camion = Tipo_Camion,
                    Marca = Marca,
                    Modelo = Modelo,
                    Capacidad = Capacidad,
                    Kilometraje = Kilometraje,
                    UrlFoto = UrlFoto,
                    Disponibilidad = Disponibilidad
                };
                return respuesta = "Camión agregado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }
        public string delete_Camion(int ID_Camion)
        {
            //preparo una respuesta
            string respuesta = "";
            try
            {
                //buscar el camión(del modelo original) a eliminar
                Camiones _camion = _context.Camiones.Find(ID_Camion);
                Camiones _camion1 = _context.Camiones.Where(x => x.ID_Camion == ID_Camion).FirstOrDefault();
                Camiones _camion2 = _context.Camiones.FirstOrDefault(x => x.ID_Camion == ID_Camion);
                Camiones _camion3 = (from c in _context.Camiones where c.ID_Camion == ID_Camion select c).FirstOrDefault();

                //elimino el obheto del contexto
                _context.Camiones.Remove(_camion);
                //impacto la BD
                _context.SaveChanges();
                //respondo
                return respuesta = "Camión eliminado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public List<Camiones_DTO> list_camiones(int id)
        {
            //creo y lleno una lista de Camiones_DTO utilizando LinQ
            List<Camiones_DTO> lista_resultado = new List<Camiones_DTO>();
            try
            {
                lista_resultado = (from c in _context.Camiones
                                   where (id == 0 || c.ID_Camion == id)
                                   select new Camiones_DTO()
                                   {
                                       ID_Camion = c.ID_Camion,
                                       Matricula = c.Matricula,
                                       Tipo_Camion = c.Tipo_Camion,
                                       Marca = c.Marca,
                                       Modelo = c.Modelo,
                                       Capacidad = c.Capacidad,
                                       Kilometraje = c.Kilometraje,
                                       UrlFoto = c.UrlFoto,
                                       Disponibilidad = c.Disponibilidad
                                   }
                                   ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return lista_resultado;
        }

        public string update_Camion(int ID_Camion, string Matricula, string Tipo_Camion, string Marca, string Modelo, int Capacidad, double Kilometraje, string UrlFoto, bool Disponibilidad)
        {
            //preparo una respuesta
            string respuesta = "";
            try
            {
                Camiones _camion_instanciado = new Camiones()
                {
                    ID_Camion = ID_Camion,
                    Matricula = Matricula,
                    Tipo_Camion = Tipo_Camion,
                    Marca = Marca,
                    Modelo = Modelo,
                    Capacidad = Capacidad,
                    Kilometraje = Kilometraje,
                    UrlFoto = UrlFoto,
                    Disponibilidad = Disponibilidad
                };
                //modifico el estado en el contexto
                _context.Entry(_camion_instanciado).State = System.Data.Entity.EntityState.Modified;
                //impacto la BD
                _context.SaveChanges();
                return respuesta = "Camión actualizado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }
    }
}
