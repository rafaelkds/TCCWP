using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleVendedor : Controle<Vendedor>
    {
        public List<Vendedor> buscar(string busca)
        {
            string query = string.Format("select * from Vendedor order by Nome");
            List<Vendedor> lista = BancoDeDados.Query<Vendedor>(query);
            return lista;
        }

        public Vendedor buscarPorId(int id)
        {
            string query = string.Format("select * from Vendedor where Id = '{0}'", id);
            List<Vendedor> lista = BancoDeDados.Query<Vendedor>(query);
            return lista[0];
        }

        public void gravar(Vendedor objeto)
        {
            throw new NotImplementedException();
        }
    }
}
