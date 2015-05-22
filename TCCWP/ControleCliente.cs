using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleCliente : Controle<Cliente>
    {
        public List<Cliente> buscar()
        {
            throw new NotImplementedException();
        }

        public void gravar(List<Cliente> lista)
        {
            string values = "";
            foreach(Cliente cliente in lista)
            {
                if (string.IsNullOrWhiteSpace(cliente.Id)) cliente.Id = BancoDeDados.GetIdCliente();
                if (values.Length > 0) values += ", ";
                values += "("
                    + "$$" + cliente.Id + "$$,"
                    + "$$" + cliente.Nome + "$$,"
                    + "$$" + cliente.Cpf + "$$," 
                    + "$$" + cliente.Rua + "$$," 
                    + "$$" + cliente.Numero + "$$," 
                    + "$$" + cliente.Bairro + "$$," 
                    + "$$" + cliente.Cidade + "$$," 
                    + "$$" + cliente.Cep + "$$," 
                    + "$$" + cliente.Complemento + "$$," 
                    + "$$" + cliente.Telefone + "$$," 
                    + "$$" + cliente.Email + "$$)";
            }
            string sql = "insert into Cliente "
                + "(Id, Nome, Cpf, Rua, Numero, Bairro, Cidade, Cep, Complemento, Telefone, Email) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.Insert(lista, log);
        }

        public void deletar(List<Cliente> lista)
        {
            throw new NotImplementedException();
        }
    }
}
