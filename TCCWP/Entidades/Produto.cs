using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class Produto
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Estoque { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        [SQLite.Ignore]
        public String ValorFormatado { get { return Valor.ToString("0.00"); } }
        [SQLite.Ignore]
        public String EstoqueFormatado { get { return Estoque.ToString("0.00"); } }
    }
}
