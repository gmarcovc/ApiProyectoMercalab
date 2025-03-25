﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATickets.Models.APIS
{
    public class ReporteVentas
    {
        public int idEncabezado { get; set; }

        public int CodigoCliente { get; set; }

        public string NombreCliente { get; set; }

        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }

        public decimal Cantidad { get; set; }


        public DateTime Fecha { get; set; }
    }
}