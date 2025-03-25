using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WATickets.Models;
using WATickets.Models.APIS;
using WATickets.Models.Cliente;

namespace WATickets.Controllers
{
    [Authorize]
    public class ReporteClientesController : ApiController
    {
        ModelCliente db = new ModelCliente();
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {




                var Parametros = db.Parametros.FirstOrDefault();

                var SQL = Parametros.SQLReporteClientes;
                string codigoProductoConComillas = $"'{filtro.CardCode}'";








                return Request.CreateResponse(HttpStatusCode.OK, db.Database.SqlQuery<ReporteClientes>(SQL).ToList());
            }
            catch (Exception ex1)
            {
                ModelCliente db2 = new ModelCliente();
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex1.Message;
                be.StrackTrace = ex1.StackTrace;
                be.Fecha = DateTime.Now;
                be.JSON = JsonConvert.SerializeObject(ex1);
                db2.BitacoraErrores.Add(be);
                db2.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex1);
            }
        }


    }
}