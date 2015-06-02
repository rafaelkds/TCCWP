using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class Pedido
    {
        [SQLite.PrimaryKey]
        public string Id { get; set; }
        public string Numero { get; set; }
        public string IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataPago { get; set; }
        public string Observacoes { get; set; }


        [SQLite.Ignore]
        public Cliente Cliente { get; set; }
        [SQLite.Ignore]
        public List<ProdutoPedido> Produtos { get; set; }
        [SQLite.Ignore]
        public List<Receber> Receber { get; set; }
    }
}
