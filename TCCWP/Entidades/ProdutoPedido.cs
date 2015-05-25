using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ProdutoPedido
    {
        [SQLite.Unique]
        public string Id { get; set; }
        public string IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
    }
}
