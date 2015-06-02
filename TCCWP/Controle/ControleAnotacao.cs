using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleAnotacao : Controle<Anotacao>
    {
        public List<Anotacao> buscar(string busca)
        {
            throw new NotImplementedException();
        }

        public void gravar(Anotacao objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.Id)) objeto.Id = BancoDeDados.GetIdPedido();
            string values = "("
                + "$$" + objeto.Id + "$$,"
                + "$$" + objeto.Data.ToString("dd/M/yyyy") + "$$,"
                + "$$" + objeto.DataUltimaAlteracao.ToString("dd/M/yyyy") + "$$,"
                + "$$" + objeto.Texto + "$$)";

            string sql = "insert into Anotacao "
                + "(Id, Data, Data_ultima_alteracao, Texto) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.InsertRIT(objeto, log);
        }

        public void deletar(Anotacao objeto)
        {
            throw new NotImplementedException();
        }
    }
}
