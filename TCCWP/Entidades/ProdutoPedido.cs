using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ProdutoPedido
    {
        [SQLite.PrimaryKey]
        public string Id { get; set; }
        public string IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeEntregue { get; set; }


        [SQLite.Ignore]
        public Produto Produto { get; set; }
        [SQLite.Ignore]
        public String ValorFormatado { get { return Valor.ToString("0.00"); } }
        [SQLite.Ignore]
        public String ValorTotalFormatado { get { return (Valor * Quantidade).ToString("0.00"); } }
        [SQLite.Ignore]
        public String QuantidadeFormatada { get { return Quantidade.ToString("0.00"); } }
        [SQLite.Ignore]
        public String QuantidadeEntregueFormatada { get { return QuantidadeEntregue.ToString("0.00"); } }
    }
}
