using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATickets.Models.Cliente
{
    public class Horarios
    {
        public int id { get; set; }


        public string Nombre { get; set; }

        public TimeSpan HoraEntrada { get; set; }

        public TimeSpan HoraSalida { get; set; }
    }
}