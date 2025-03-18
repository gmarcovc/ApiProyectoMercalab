using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WATickets.Models.Cliente
{
    [Table("Rutas")]
    public class Rutas
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Origen { get; set; }
        public decimal Costos { get; set; }
        public decimal Km { get; set; }
    }
}