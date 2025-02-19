using Newtonsoft.Json;
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
    public class OtrasDeduccionesController : ApiController
    {
        ModelCliente db = new ModelCliente();
        public HttpResponseMessage GetAll([FromUri] Filtros filtro)
        {
            try
            {


                var OtrasDeducciones = db.OtrasDeducciones.Where(a => (filtro.Codigo1 > 0 ? a.idPlanilla == filtro.Codigo1 : true)

                   ).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, OtrasDeducciones);

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

        [Route("api/OtrasDeducciones/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int id)
        {
            try
            {
                OtrasDeducciones OtrasDeducciones = db.OtrasDeducciones.Where(a => a.id == id).FirstOrDefault();


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, OtrasDeducciones);
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
        [Route("api/OtrasDeducciones/Insertar")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] OtrasDeducciones deducciones)
        {
            try
            {
                OtrasDeducciones OtrasDeducciones = db.OtrasDeducciones.Where(a => a.id == deducciones.id).FirstOrDefault();
                if (OtrasDeducciones == null)
                {
                    OtrasDeducciones = new OtrasDeducciones();

                    OtrasDeducciones.Nombre = deducciones.Nombre;
                    OtrasDeducciones.Monto = deducciones.Monto;
                    OtrasDeducciones.idPlanilla = deducciones.idPlanilla;
                    db.OtrasDeducciones.Add(OtrasDeducciones);
                    db.SaveChanges();

                    var Planilla = db.Planillas.Where(a => a.id == OtrasDeducciones.idPlanilla).FirstOrDefault();

                    db.Entry(Planilla).State = System.Data.Entity.EntityState.Modified;
                    Planilla.Monto -= OtrasDeducciones.Monto;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Ya existe una deduccion con este ID");
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