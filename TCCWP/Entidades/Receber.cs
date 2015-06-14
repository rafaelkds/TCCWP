using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class Receber
    {
        [SQLite.PrimaryKey]
        public string Id { get; set; }
        public string IdPedido { get; set; }
        public int Ordem { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Pagamento { get; set; }

        [SQLite.Ignore]
        public string VencimentoFormatado { get { return Vencimento.ToString("dd/MM/yyyy"); } }
    }
}
