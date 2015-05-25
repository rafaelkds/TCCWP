using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    interface Controle<T>
    {
        List<T> buscar();
        void gravar(List<T> lista);
        void deletar(List<T> lista);
    }
}
