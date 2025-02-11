using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WATickets.Models;
using WATickets.Models.Cliente;

namespace WATickets.Controllers
{
    [Authorize]
    public class PeriodoPagosController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public HttpResponseMessage GetAll()
        {
            try
            {

                var PeriodoPagos = db.PeriodoPagos.ToList();


                return Request.CreateResponse(HttpStatusCode.OK, PeriodoPagos);

            }
            catch (Exception ex)
            {
                BitacoraErrores bt = new BitacoraErrores();
                bt.Descripcion = ex.Message;
                bt.StrackTrace = ex.StackTrace;
                bt.Fecha = DateTime.Now;
                bt.JSON = JsonConvert.SerializeObject(ex);
                db.BitacoraErrores.Add(bt);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/PeriodoPagos/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int id)
        {
            try
            {
                PeriodoPagos periodos = db.PeriodoPagos.Where(a => a.id == id).FirstOrDefault();


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, periodos);
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
        [Route("api/PeriodoPagos/Insertar")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] PeriodoPagos periodos)
        {
            try
            {
                PeriodoPagos PeriodoPagos = db.PeriodoPagos.Where(a => a.id == periodos.id).FirstOrDefault();
                if (PeriodoPagos == null)
                {
                    PeriodoPagos = new PeriodoPagos();
                    PeriodoPagos.id = PeriodoPagos.id;
                    PeriodoPagos.Nombre = periodos.Nombre;
                    PeriodoPagos.CantidadDias = periodos.CantidadDias;
                    db.PeriodoPagos.Add(PeriodoPagos);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Ya existe un puesto con este ID");
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

        [Route("api/PeriodoPagos/Actualizar")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] PeriodoPagos periodos)
        {
            try
            {
                PeriodoPagos PeriodoPagos = db.PeriodoPagos.Where(a => a.id == periodos.id).FirstOrDefault();
                if (PeriodoPagos != null)
                {
                    db.Entry(PeriodoPagos).State = System.Data.Entity.EntityState.Modified;
                    PeriodoPagos.Nombre = periodos.Nombre;
                    PeriodoPagos.CantidadDias = periodos.CantidadDias;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe un puesto" +
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

        [Route("api/PeriodoPagos/Eliminar")]
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                PeriodoPagos PeriodoPagos = db.PeriodoPagos.Where(a => a.id == id).FirstOrDefault();
                if (PeriodoPagos != null)
                {
                    db.PeriodoPagos.Remove(PeriodoPagos);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe un puesto con este ID");
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