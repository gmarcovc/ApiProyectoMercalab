using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATickets.Models
{
    public class PeriodoPagos
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int CantidadDias { get; set; }
    }
}