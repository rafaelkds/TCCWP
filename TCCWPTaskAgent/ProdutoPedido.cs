using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWPTaskAgent
{
    class ProdutoPedido
    {
        [SQLite.PrimaryKey]
        public string Id { get; set; }
        public string IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }


        [SQLite.Ignore]
        public Produto Produto { get; set; }
    }
}
