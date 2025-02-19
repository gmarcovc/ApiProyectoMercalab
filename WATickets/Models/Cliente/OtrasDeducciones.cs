using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATickets.Models.Cliente
{
    public class OtrasDeducciones
    {
        public int id { get; set; }
        public int idPlanilla { get; set; }
        public string Nombre { get; set; }
        public decimal Monto { get; set; }
    }
}