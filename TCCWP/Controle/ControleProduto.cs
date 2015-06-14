using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleProduto : Controle<Produto>
    {
        public List<Produto> buscar(string busca)
        {
            string query = string.Format("select * from Produto where Nome like '{0}%' order by Nome", busca);
            List<Produto> lista = BancoDeDados.Query<Produto>(query);
            return lista;
        }

        public Produto buscarPorId(string id)
        {
            string query = string.Format("select * from Produto where Id = '{0}'", id);
            List<Produto> lista = BancoDeDados.Query<Produto>(query);
            return lista[0];
        }

        public void gravar(Produto objeto)
        {
            
        }
    }
}
