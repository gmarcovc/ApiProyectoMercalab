using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WATickets.Models;
using WATickets.Models.Cliente;


namespace WATickets.Controllers
{
    [Authorize]
    public class RutasController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public HttpResponseMessage GetAll([FromUri] Filtros filtro)
        {
            try
            {
                var Rutas = db.Rutas.ToList();


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, Rutas);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StrackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.JSON = JsonConvert.SerializeObject(ex);
                db.BitacoraErrores.Add(be);
                db.SaveChanges();


                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);

            }

        }
        [Route("api/Rutas/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int id)
        {
            try
            {
                Rutas rutas = db.Rutas.Where(a => a.id == id).FirstOrDefault();


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, rutas);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StrackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.JSON = JsonConvert.SerializeObject(ex);
                db.BitacoraErrores.Add(be);
                db.SaveChanges();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);

            }

        }
        [Route("api/Rutas/Insertar")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Rutas rutas)
        {
            try
            {
                Rutas Ruta = db.Rutas.Where(a => a.id == rutas.id).FirstOrDefault();
                if (Ruta == null)
                {
                    Ruta = new Rutas();
                    Ruta.Nombre = rutas.Nombre;
                    Ruta.Origen = rutas.Origen;
                    Ruta.Costos = rutas.Costos;
                    Ruta.Km = rutas.Km;
                    db.Rutas.Add(Ruta);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Ya existe una ruta con este ID");
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StrackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.JSON = JsonConvert.SerializeObject(ex);
                db.BitacoraErrores.Add(be);
                db.SaveChanges();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        [Route("api/Rutas/Actualizar")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Rutas rutas)
        {
            try
            {
                Rutas Ruta = db.Rutas.Where(a => a.id == rutas.id).FirstOrDefault();
                if (Ruta != null)
                {
                    db.Entry(Ruta).State = System.Data.Entity.EntityState.Modified;
                    Ruta.Nombre = rutas.Nombre;
                    Ruta.Origen = rutas.Origen;
                    Ruta.Costos = rutas.Costos;
                    Ruta.Km = rutas.Km;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe una ruta" +
                        " con este ID");
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StrackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.JSON = JsonConvert.SerializeObject(ex);
                db.BitacoraErrores.Add(be);
                db.SaveChanges();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        [Route("api/Rutas/Eliminar")]
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                Rutas Rutas = db.Rutas.Where(a => a.id == id).FirstOrDefault();
                if (Rutas != null)
                {
                    db.Rutas.Remove(Rutas);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe una bodega con este ID");
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StrackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.JSON = JsonConvert.SerializeObject(ex);
                db.BitacoraErrores.Add(be);
                db.SaveChanges();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}