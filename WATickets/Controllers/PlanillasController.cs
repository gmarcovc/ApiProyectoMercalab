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
    public class PlanillasController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public HttpResponseMessage GetAll([FromUri] Filtros filtro)
        {
            try
            {

                var Planillas = db.Planillas.Where(a => (filtro.Codigo1 > 0 ? a.idHorario == filtro.Codigo1 : true)
         && (filtro.Codigo2 > 0 ? a.idPeriodoPago == filtro.Codigo2 : true)
          && (filtro.Codigo3 > 0 ? a.idUsuario == filtro.Codigo3 : true)
         ).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, Planillas);

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
        [Route("api/Planillas/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int id)
        {
            try
            {
                Planillas planillas = db.Planillas.Where(a => a.id == id).FirstOrDefault();


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, planillas);
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
        [Route("api/Planillas/Insertar")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Planillas planillas)
        {
            try
            {
                Planillas Planillas = db.Planillas.Where(a => a.id == planillas.id).FirstOrDefault();
                if (Planillas == null)
                {
                    Planillas = new Planillas();
                    Planillas.idHorario = planillas.idHorario;
                    Planillas.idPeriodoPago = planillas.idPeriodoPago;
                    Planillas.idUsuario = planillas.idUsuario;
                    Planillas.Monto = planillas.Monto;
                    db.Planillas.Add(Planillas);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Ya existe una planilla con este ID");
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
        [Route("api/Planillas/Actualizar")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Planillas planillas)
        {
            try
            {
                Planillas Planillas = db.Planillas.Where(a => a.id == planillas.id).FirstOrDefault();
                if (Planillas != null)
                {
                    db.Entry(Planillas).State = System.Data.Entity.EntityState.Modified;
                    Planillas.idHorario = planillas.idHorario;
                    Planillas.idPeriodoPago = planillas.idPeriodoPago;
                    Planillas.idUsuario = planillas.idUsuario;
                    Planillas.Monto = planillas.Monto;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe una planilla" +
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
        [Route("api/Planillas/Eliminar")]
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                Planillas Planillas = db.Planillas.Where(a => a.id == id).FirstOrDefault();
                if (Planillas != null)
                {
                    db.Planillas.Remove(Planillas);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe una planilla con este ID");
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