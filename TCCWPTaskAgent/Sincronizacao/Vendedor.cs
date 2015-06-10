using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWPTaskAgent.Sincronizacao
{
    class Vendedor
    {
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
