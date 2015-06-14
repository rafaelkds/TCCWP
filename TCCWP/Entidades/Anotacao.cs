using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class Anotacao
    {
        [SQLite.PrimaryKey]
        public string Id { get; set; }
        public string IdPedido { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string Texto { get; set; }

        [SQLite.Ignore]
        public string DataFormatada { get { return Data.ToString("dd/MM/yyyy"); } }
        [SQLite.Ignore]
        public string DataUltimaAlteracaoFormatada { get { return DataUltimaAlteracao.ToString("dd/MM/yyyy"); } }
    }
}
