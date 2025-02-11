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
    public class HorariosController : ApiController
    {
        ModelCliente db = new ModelCliente();

        public HttpResponseMessage GetAll([FromUri] Filtros filtro)
        {
            try
            {

                var Horarios = db.Horarios.ToList();


                return Request.CreateResponse(HttpStatusCode.OK, Horarios);

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

        [Route("api/Horarios/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int id)
        {
            try
            {
                Horarios horarios = db.Horarios.Where(a => a.id == id).FirstOrDefault();


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, horarios);
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
        [Route("api/Horarios/Insertar")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Horarios horarios)
        {
            try
            {
                Horarios Horarios = db.Horarios.Where(a => a.id == horarios.id).FirstOrDefault();
                if (Horarios == null)
                {
                    Horarios = new Horarios();
                    Horarios.Nombre = horarios.Nombre;
                    Horarios.HoraEntrada = horarios.HoraEntrada;
                    Horarios.HoraSalida = horarios.HoraSalida;
                    db.Horarios.Add(Horarios);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Ya existe un horario con este ID");
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

        [Route("api/Horarios/Actualizar")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Horarios horarios)
        {
            try
            {
                Horarios Horarios = db.Horarios.Where(a => a.id == horarios.id).FirstOrDefault();
                if (Horarios != null)
                {
                    db.Entry(Horarios).State = System.Data.Entity.EntityState.Modified;
                    Horarios.Nombre = horarios.Nombre;
                    Horarios.HoraEntrada = horarios.HoraEntrada;
                    Horarios.HoraSalida = horarios.HoraSalida;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe un horario" +
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

        [Route("api/Horarios/Eliminar")]
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                Horarios Horarios = db.Horarios.Where(a => a.id == id).FirstOrDefault();
                if (Horarios != null)
                {
                    db.Horarios.Remove(Horarios);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe un horario con este ID");
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