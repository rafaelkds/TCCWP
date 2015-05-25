using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class Pedido
    {
        [SQLite.Unique]
        public string Id { get; set; }
        public string Numero { get; set; }
        public string IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataPago { get; set; }
        public string Observacoes { get; set; }
    }
}
