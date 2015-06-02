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
        public DateTime Data { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string Texto { get; set; }
    }
}
