using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    interface Controle<T>
    {
        List<T> buscar(string busca);
        void gravar(T objeto);
        void deletar(T objeto);
    }
}
