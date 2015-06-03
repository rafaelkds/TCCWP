using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class Sinc
    {
        public int Identificacao { get; set; }
        public long UltimaSinc { get; set; }
        
        public DateTime getUltimaSinc()
        {
            return new DateTime(UltimaSinc);
        }
    }
}
