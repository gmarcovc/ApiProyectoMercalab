using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATickets.Models.Cliente
{
    public class Planillas
    {
        public int id { get; set; }
        public int idHorario { get; set; }
        public int idPeriodoPago { get; set; }
        public int idUsuario { get; set; }
        public decimal Monto { get; set; }
    }
}