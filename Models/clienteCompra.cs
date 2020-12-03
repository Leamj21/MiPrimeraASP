using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiPrimeraASP.Models
{
    public class clienteCompra
    {
        public string clienteNombre { get; set; }
        public int compraId { get; set; }
        public string emailCliente { get; set; }
        public Nullable<System.DateTime> fechaCompra { get; set; }
        public Nullable<int> totalCompra { get; set; }

    }
}