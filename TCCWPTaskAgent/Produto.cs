using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWPTaskAgent
{
    class Produto
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Estoque { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
    }
}
